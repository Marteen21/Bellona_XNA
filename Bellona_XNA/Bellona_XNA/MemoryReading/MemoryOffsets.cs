using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    class MemoryOffsets {
        public static readonly uint ObjectManagerPointer = 0x943318;        //From MainModule Baseaddress
        public static readonly uint ObjectManagerOffset = 0x2218;           //From ObjectManagerPointer
        public static readonly uint ObjectManagerFirstObjectAddress = 0xAC;
        public static readonly uint ObjectManagerNextObjectAddress = 0x3C;
        public static readonly uint ObjectManagerLocalGUID = 0x30;
        public static readonly uint ObjectManagerSpellID = 0x1A4;
        public static readonly uint ObjectManagerLocalDescriptorArray = 0xC;
        public static readonly uint ObjectManagerLocalMovementArray = 0x100;

        public static readonly uint DescriptorArrayClass8 = 0x7d;

        public static readonly uint MovementArrayIsMoving8 = 0x38;

        public static readonly uint ObjectManagerSpellPosX = 0x110;
        public static readonly uint ObjectManagerSpellPosY = 0x114;
        public static readonly uint ObjectManagerSpellPosZ = 0x118;

        public static readonly uint ObjectManagerUnitPosX = 0xBF0;
        public static readonly uint ObjectManagerUnitPosY = 0xBF4;
        public static readonly uint ObjectManagerUnitPosZ = 0xBF8;
        public static readonly uint ObjectManagerUnitRotation = 0xBFC;


        public static readonly uint GlobalInfoPlayerGUID = 0x943340;
        public static readonly uint GlobalInfoTargetGUID = 0x86E960;

        public static readonly uint ClickToMoveTargetXOffset = 0x968a18;
        public static readonly uint ClickToMoveTargetYOffset = 0x968a1C;
        public static readonly uint ClickToMoveTargetZOffset = 0x968a20;

        public static readonly uint ClickToMoveStarter = 0x9689BC;

        public enum Descriptors : uint
        {
            Class8 = 0x7d,
            Level = 0x74,
            Health = 0x44,
            MaxHealth = 0x5C,
            Power = 0x48,
            MaxPower = 0x60,
            TargetGUID = 0x40,
            ShapeShift = 0x1C7,
            Faction = 0xB4,
            Strength = 0x26C,
            Agility = 0x270,
            Stamina = 0x274,
            Intellect = 0x278,
            Spirit = 0x27C,
            Armor = 0x280
        }

    }
}
