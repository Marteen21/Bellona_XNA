using Bellona_XNA.Misc;
using Bellona_XNA.Radar;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    [Flags]
    public enum WoWClass : uint {
        None = 0,
        Warrior = 1,
        Paladin = 2,
        Hunter = 3,
        Rogue = 4,
        Priest = 5,
        DeathKnight = 6,
        Shaman = 7,
        Mage = 8,
        Warlock = 9,
        Druid = 11,
    }
    public class WoWUnit : WoWObject {
        //private WoWClass;
        private Vector3 position;
        private float rotation;
        private WoWClass unitClass;
        private RadarUnit rUnit;

        public Vector3 Position {
            get {
                return position;
            }

            set {
                position = value;
            }
        }

        internal RadarUnit RUnit {
            get {
                return rUnit;
            }

            set {
                rUnit = value;
            }
        }

        public WoWUnit(WoWObject wo, Vector3 pos,float rot, WoWClass uClass) : base(wo) {
            this.position = pos;
            this.unitClass = uClass;
            this.RUnit = new RadarUnit(new Vector2(
                -RadarObject.RadarZoom * (pos.Y - Game1.mainPlayer.Position.Y),
                -RadarObject.RadarZoom * (pos.X - Game1.mainPlayer.Position.X)), -(rot+(float)Math.PI/2), ColorHelper.GetColorFromWoWClass(uClass));

        }
        public WoWUnit(ulong guid) {
            this.Guid = guid;
        }
        public bool RefreshFromList(List<WoWUnit> allUnits) {
            foreach (WoWUnit wu in allUnits) {
                if (wu.Guid == this.Guid) {
                    this.position = wu.position;
                    this.rotation = wu.rotation;
                    this.BaseAddress = wu.BaseAddress;
                    return true;
                }
            }
            return false;
        }


        public void Refresh(WoWConnection wc) {
            try {
                Vector3 myUnitPos = new Vector3(
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosX),
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosY),
                            wc.Connection.ReadFloat(BaseAddress + MemoryOffsets.ObjectManagerUnitPosZ));
                position = myUnitPos;
            }
            catch {

            }
        }


        public static void GetAllUnits(out List<WoWUnit> allunits, WoWConnection wc) {
            allunits = new List<WoWUnit>();
            WoWObject TempObject = new WoWObject(wc.FirstObjectAddress, wc);
            while ((uint)TempObject.BaseAddress != 0 && TempObject.Guid != 0) {
                if (TempObject.UnitType == WoWUnitType.Player || TempObject.UnitType == WoWUnitType.NPC) {
                    Vector3 myUnitPos = new Vector3(
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosX),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosY),
                        wc.Connection.ReadFloat(TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitPosZ));
                    WoWClass myClass = (WoWClass)wc.Connection.ReadByte(wc.Connection.ReadUInt((uint)TempObject.BaseAddress+MemoryOffsets.ObjectManagerLocalDescriptorArray)+MemoryOffsets.DescriptorArrayClass8);
                    float myUnitRot = wc.Connection.ReadFloat((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerUnitRotation);
                    if (myUnitRot > Math.PI) {
                        myUnitRot = -(2 * (float)(Math.PI) - myUnitRot);
                    }
                    allunits.Add(new WoWUnit(TempObject, myUnitPos, myUnitRot, myClass));
                }
                try {
                    TempObject = new WoWObject(wc.Connection.ReadUInt((uint)TempObject.BaseAddress + MemoryOffsets.ObjectManagerNextObjectAddress), wc);
                }
                catch {
                    break;
                }

            }
        }



    }
}
