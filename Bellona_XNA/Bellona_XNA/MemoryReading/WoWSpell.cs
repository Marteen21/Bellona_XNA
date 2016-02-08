using Bellona_XNA.Radar;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    public class WoWSpell : WoWObject {
        private Vector3 position;
        private uint spellID;
        private RadarSpell rSpell;

        internal RadarSpell RSpell {
            get {
                return rSpell;
            }

            set {
                rSpell = value;
            }
        }

        public WoWSpell(WoWObject wo, Vector3 pos, uint sID) : base(wo) {
            this.position = pos;
            this.spellID = sID;
            this.RSpell = new RadarSpell(new Vector2(
                -RadarObject.RadarZoom * (pos.Y - Game1.mainPlayer.Position.Y),
                -RadarObject.RadarZoom * (pos.X - Game1.mainPlayer.Position.X)), 0f);

        }
        public static void GetAllSpells(out List<WoWSpell> allspells, WoWConnection wc) {
            allspells = new List<WoWSpell>();
            WoWObject TempObject = new WoWObject(wc.FirstObjectAddress, wc);
            while ((uint)TempObject.BaseAddress != 0 && TempObject.Guid != 0) {
                if (TempObject.UnitType == WoWUnitType.Spell) {
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
        }
    }
}
