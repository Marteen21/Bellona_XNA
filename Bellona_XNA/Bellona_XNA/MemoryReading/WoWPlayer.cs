using Bellona_XNA.MemoryReading.Helpers;
using Bellona_XNA.Misc;
using Bellona_XNA.Radar;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    public class WoWPlayer : WoWObject {
        private Vector3 position;
        private float rotation;
        private WoWClass unitClass;
        private RadarPlayer rPlayer;
        private MovementFlags movingInfo = new MovementFlags(0);
        public Vector2 MovementTarget = new Vector2();
        private float healthpercent;
        private float powerpercent;
        public string WindowTitle;

        internal RadarPlayer RPlayer {
            get {
                return rPlayer;
            }

            set {
                rPlayer = value;
            }
        }

        public Vector3 Position {
            get {
                return position;
            }

            set {
                position = value;
            }
        }

        public float Rotation {
            get {
                return rotation;
            }

            set {
                rotation = value;
            }
        }

        internal MovementFlags MovingInfo {
            get {
                return movingInfo;
            }

            set {
                movingInfo = value;
            }
        }

        public float Healthpercent { get => healthpercent; set => healthpercent = value; }
        public float Powerpercent { get => powerpercent; set => powerpercent = value; }

        public void SetPositions(WoWPlayer other) {
            this.position = other.position;
            this.rotation = other.rotation;
            this.rPlayer.RelativeRadarPosToPlayer = new Vector2(
                -RadarObject.RadarZoom * (other.position.Y - Game1.mainPlayer.Position.Y),
                -RadarObject.RadarZoom * (other.position.X - Game1.mainPlayer.Position.X));
            this.rPlayer.RadarRot = -(other.rotation + (float)Math.PI / 2);
            this.MovingInfo = other.MovingInfo;
        }
        public WoWPlayer(WoWObject wo, Vector3 pos, float rot, WoWClass uClass, byte moveflag) : base(wo) {
            this.Position = pos;
            this.Rotation = rot;
            this.unitClass = uClass;
            this.MovingInfo = new MovementFlags(moveflag);
            this.RPlayer = new RadarPlayer(new Vector2(
                -RadarObject.RadarZoom * (pos.Y - Game1.mainPlayer.Position.Y),
                -RadarObject.RadarZoom * (pos.X - Game1.mainPlayer.Position.X)), -(rot + (float)Math.PI / 2), ColorHelper.GetColorFromWoWClass(uClass));
        }
        public WoWPlayer(ulong guid) {
            this.Guid = guid;
        }
        public WoWPlayer(WoWPlayer other) {
            this.Position = other.position;
            this.rPlayer = other.rPlayer;
            this.UnitType = other.UnitType;
            this.Guid = other.Guid;
            this.BaseAddress = other.BaseAddress;
        }
        public bool RefreshFromList(List<WoWPlayer> allPlayers, string WindowTitle) {
            foreach (WoWPlayer wu in allPlayers) {
                if (wu.Guid == this.Guid) {
                    this.Position = wu.Position;
                    this.Rotation = wu.Rotation;
                    this.BaseAddress = wu.BaseAddress;
                    wu.WindowTitle = WindowTitle;
                    return true;
                }
            }
            return false;
        }
        public static bool SetWindowTitleForPlayer(ref List<WoWPlayer> allplayer, ulong playerguid, string playerWindowTitle) {
            try {
                allplayer.Find(x => x.Guid == playerguid).WindowTitle = playerWindowTitle;
                return true;
            }
            catch (NullReferenceException) {
                return false;
            }

        }
        public void SetWindowTitle(string WindowTitle) {
            this.WindowTitle = WindowTitle;
        }

        public void Refresh(WoWConnection wc) {
            try {
                Vector3 myUnitPos = new Vector3(
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosX),
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosY),
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosZ));
                position = myUnitPos;

                UIntPtr descriptorArrayAddress = (UIntPtr)(wc.Connection.ReadUInt(this.BaseAddress + MemoryOffsets.ObjectManagerLocalDescriptorArray));
                uint Health = wc.Connection.ReadUInt((uint)descriptorArrayAddress + (uint)MemoryOffsets.Descriptors.Health);
                uint MaxHealth = wc.Connection.ReadUInt((uint)descriptorArrayAddress + (uint)MemoryOffsets.Descriptors.MaxHealth);
                uint Power = wc.Connection.ReadUInt((uint)descriptorArrayAddress + (uint)MemoryOffsets.Descriptors.Power);
                uint MaxPower = wc.Connection.ReadUInt((uint)descriptorArrayAddress + (uint)MemoryOffsets.Descriptors.MaxPower);

                healthpercent = (float)Health / (float)MaxHealth;
                powerpercent = (float)Power/ (float)MaxPower;
            }
            catch {

            }
        }
    }



}