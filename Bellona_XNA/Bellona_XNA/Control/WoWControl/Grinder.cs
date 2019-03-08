using Bellona_XNA.MemoryReading;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Control.WoWControl
{
    class Grinder {
        enum State {
            Combat,
            Killing,
            Rejuvenate,
            Reposition,
            Loot,
            Flee
        }

        State lastState;
        State mode;
        WoWConnection wowConnection;
        WoWPlayer player;
        WoWUnit lastTarget;
        WoWUnit target;
        WoWUnit pet;
        ulong tick = 0;
        DateTime previousRefreshTime = DateTime.MinValue;
        List<Vector3> checkpoints;
        uint channeledSpell = 0;
        uint castedSpell = 0;
        int atCP;
        public bool isInCombat;
        public bool hasEnoughResourceToFight;

        public Grinder(WoWConnection mainWow, WoWPlayer mainPlayer, List<Vector3> cs) {
            this.wowConnection = mainWow;
            this.player = mainPlayer;
            this.checkpoints = cs;
            this.atCP = 0;
        }

        private bool IsAtTarget(Vector3 target, float threshold) {
            return Vector3.Distance(player.Position, target) < threshold;
        }

        private void MoveToCommand(Vector3 target) {
            wowConnection.Connection.WriteFloat((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.ClickToMoveTargetXOffset, target.X);
            wowConnection.Connection.WriteFloat((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.ClickToMoveTargetYOffset, target.Y);
            wowConnection.Connection.WriteFloat((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.ClickToMoveTargetZOffset, target.Z);

            wowConnection.Connection.WriteUInt((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.ClickToMoveStarter, 4);
        }

        private void TryToAttack() {

        }

        private void Halt() {
            MoveToCommand(player.Position);
        }

        public void Update() {
            if ((DateTime.UtcNow - previousRefreshTime) > TimeSpan.FromMilliseconds(50)) {
                previousRefreshTime = DateTime.UtcNow;
                tick++;
                Refresh();
                SetGrinderState();
                if (lastState == State.Combat && mode == State.Killing)
                    SendKey.Send(ConstController.WindowsVirtualKey.K_W);
                switch (mode) {
                    case State.Killing:
                        KillTarget();
                        return;
                    case State.Combat:
                        if (target != null)
                            MoveToCommand(target.Position);
                        else
                            SendKey.Send(ConstController.WindowsVirtualKey.VK_TAB);
                        return;
                    case State.Rejuvenate:
                        Rejuvenate();
                        break;
                    case State.Reposition:
                        SearchForTarget();
                        if (IsAtTarget(checkpoints[atCP],5)) {
                            atCP = (atCP+1) >= checkpoints.Count ? 0 : atCP+1;
                        }
                        MoveToCommand(checkpoints[atCP]);
                        break;
                    case State.Loot:
                        MoveToCommand(lastTarget.Position);
                        if (IsAtTarget(lastTarget.Position, 1)) {
                            SendKey.Send(ConstController.WindowsVirtualKey.K_L);
                            lastTarget = null;
                        }
                        break;
                    default:
                        break;

                }
            } else return;
        }

        private void Rejuvenate() {
            Halt();
            SendKey.Send(ConstController.WindowsVirtualKey.K_9);
            if (pet != null && pet.HealthPercent < 0.7 && tick%50 == 0)
                SendKey.Send(ConstController.WindowsVirtualKey.K_8);
        }

        private void SetGrinderState() {
            lastState = mode;
            if (isInCombat || target != null || pet.TargetGUID != 0) {
                if (CanInteractWithTarget())
                    mode = State.Killing;
                else
                    mode = State.Combat;
            } else if (player.Healthpercent < 0.9 || player.Powerpercent < 0.9 || (pet == null || pet.HealthPercent < 0.8))
                mode = State.Rejuvenate;
            else
                mode = State.Reposition;
        }

        private void SearchForTarget() {
            if (target == null)
                SendKey.Send(ConstController.WindowsVirtualKey.VK_TAB);
        }

        public void Refresh() {
            wowConnection.TryToRefreshObjectManager();
            WoWObject.GetAllObjects(ref Game1.allUnits, ref Game1.allSpells, ref Game1.allPlayers, wowConnection);
            player.Refresh(wowConnection);
            byte combatByte = wowConnection.Connection.ReadByte(wowConnection.Connection.ReadUInt(player.BaseAddress + MemoryOffsets.ObjectManagerLocalCombatInfoArray) + MemoryOffsets.UnitIsInCombat);
            channeledSpell = wowConnection.Connection.ReadByte((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoSpellBeingChanelled);
            castedSpell = wowConnection.Connection.ReadByte((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoSpellBeingCasted);
            isInCombat = (combatByte & 0x4) != 0;
            RefreshTarget();
            RefreshPet();
        }

        private void RefreshTarget() {
            lastTarget = target;
            UInt64 targetGuid = wowConnection.Connection.ReadUInt64((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoTargetGUID);
            if (targetGuid != 0) {
                target = new WoWUnit(targetGuid);
                target.RefreshFromList(Game1.allUnits);
                target.Refresh(wowConnection);
            } else
                target = null;
        }

        private void RefreshPet() {
            UInt64 petGUID = wowConnection.Connection.ReadUInt64((uint)wowConnection.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoPetGUID);
            if (petGUID != 0) {
                pet = new WoWUnit(petGUID);
                pet.RefreshFromList(Game1.allUnits);
                pet.Refresh(wowConnection);
            } else
                pet = null;
        }

        public bool CanInteractWithTarget() {
            if (target != null) {
                double distance = Vector3.Distance(player.Position, target.Position);
                if (distance > 20)
                    return false;
                double diff = Angles.AngleDiff(Angles.Calculateangle(target.Position, player.Position), player.Rotation);
                if (Math.Abs(diff) < 3*Math.PI/4)
                    return true;
                else
                    return false;
            }
            return false;
        }
        


        private double DiffBetweenAngles (double angle1, double angle2) {
            double diff = Math.Abs(angle1 - angle2);
            if (diff > Math.PI) {
                return angle1 > angle2 ? Math.Abs(angle1 - (angle2 + Math.PI * 2)) : Math.Abs((angle1 + Math.PI * 2) - angle2);
            } else
                return diff;
        }

        public void KillTarget() {

            if (target != null) {
                if (castedSpell == 0) {
                    if (target.HealthPercent < 0.3) 
                        SendKey.Send(ConstController.WindowsVirtualKey.K_V);
                    else
                        SendKey.Send(ConstController.WindowsVirtualKey.K_1);
                }
            } else {
                SendKey.Send(ConstController.WindowsVirtualKey.VK_TAB);
            }
        }
    }
}
