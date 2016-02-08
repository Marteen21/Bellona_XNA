using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    [Flags]
    public enum WoWUnitType : uint {
        Player = 0,
        WorldObject = 1,
        NPC = 3,
        PermanentPet = 4,
        Spell = 5,
        Unknown = 6,
    }
    public class WoWObject {
        private uint baseAddress;
        private ulong guid;
        private WoWUnitType unitType;
        public uint BaseAddress {
            get {
                return baseAddress;
            }

            set {
                baseAddress = value;
            }
        }

        public ulong Guid {
            get {
                return guid;
            }

            set {
                guid = value;
            }
        }

        public WoWUnitType UnitType {
            get {
                return unitType;
            }

            set {
                unitType = value;
            }
        }

        public WoWObject(uint bAddress, WoWConnection wowConnection) {
            this.BaseAddress = bAddress;
            this.Guid = wowConnection.Connection.ReadUInt64(this.BaseAddress + MemoryOffsets.ObjectManagerLocalGUID);
            ulong temp = this.guid >> 52;
            switch ((this.guid >> 52) & 0xF0F) {
                case 0x000:
                    this.UnitType = WoWUnitType.Player;
                    break;
                case 0xF00:
                    this.UnitType = WoWUnitType.Spell;
                    break;
                case 0xF03:
                    this.UnitType = WoWUnitType.NPC;
                    break;
                case 0xF01:
                    this.UnitType = WoWUnitType.WorldObject;
                    break;
                case 0xF04:
                    this.UnitType = WoWUnitType.PermanentPet;
                    break;
                default:
                    this.UnitType = WoWUnitType.Unknown;
                    break;
            }
        }
        public WoWObject(WoWObject other) {
            this.BaseAddress = other.BaseAddress;
            this.Guid = other.Guid;
            this.UnitType = other.UnitType;
        }
        public WoWObject() {

        }
        public static void GetAllObjects(ref List<WoWUnit> allunits, ref List<WoWSpell> allspells, ref List<WoWPlayer> allplayers, WoWConnection wc) {
            allunits = new List<WoWUnit>();
            allspells = new List<WoWSpell>();
            List<WoWPlayer> tempplayers = new List<WoWPlayer>();
            WoWObject TempObject = new WoWObject(wc.FirstObjectAddress, wc);
            while ((uint)TempObject.BaseAddress != 0 && TempObject.Guid != 0) {
                if (TempObject.UnitType == WoWUnitType.NPC) {
                    Vector3 myUnitPos = new Vector3(
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosX),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosY),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosZ));
                    WoWClass myClass = (WoWClass)wc.Connection.ReadByte(wc.Connection.ReadUInt((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerLocalDescriptorArray) + 0x10 + MemoryOffsets.DescriptorArrayClass8);
                    float myUnitRot = wc.Connection.ReadFloat((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitRotation);
                    if (myUnitRot > Math.PI) {
                        myUnitRot = -(2 * (float)(Math.PI) - myUnitRot);
                    }
                    allunits.Add(new WoWUnit(TempObject, myUnitPos, myUnitRot, myClass));
                }
                else if (TempObject.UnitType == WoWUnitType.Player) {
                    Vector3 myPlayerPos = new Vector3(
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosX),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosY),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosZ));
                    WoWClass myClass = (WoWClass)wc.Connection.ReadByte(wc.Connection.ReadUInt((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerLocalDescriptorArray) + 0x10 + MemoryOffsets.DescriptorArrayClass8);
                    float myPlayerRot = wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitRotation);
                    uint myMovementArrayAddress = wc.Connection.ReadUInt(TempObject.BaseAddress + MemoryOffsets.ObjectManagerLocalMovementArray);

                    byte movebyte = wc.Connection.ReadByte(myMovementArrayAddress + MemoryOffsets.MovementArrayIsMoving8);
                    if (myPlayerRot > Math.PI) {
                        myPlayerRot = -(2 * (float)(Math.PI) - myPlayerRot);
                    }
                    tempplayers.Add(new WoWPlayer(TempObject, myPlayerPos, myPlayerRot, myClass, movebyte));
                }
                else if (TempObject.UnitType == WoWUnitType.Spell) {
                    Vector3 mySpellPos = new Vector3(
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerSpellPosX),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerSpellPosY),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerSpellPosZ));
                    allspells.Add(new WoWSpell(TempObject, mySpellPos, wc.Connection.ReadUInt(TempObject.BaseAddress + MemoryOffsets.ObjectManagerSpellID)));
                }
                try {
                    TempObject = new WoWObject(wc.Connection.ReadUInt((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerNextObjectAddress), wc);
                }
                catch {
                    break;
                }
            }
            foreach (WoWPlayer y in tempplayers) {
                try {
                    allplayers.First(x => x.guid == y.guid).SetPositions(y);
                }
                catch (InvalidOperationException) {
                    allplayers.Add(y);
                }
            }
        }
    }
}


