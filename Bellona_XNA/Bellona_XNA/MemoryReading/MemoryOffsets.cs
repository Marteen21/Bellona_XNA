using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    class MemoryOffsets {
        public static readonly uint ObjectManagerPointer = 0x9BE7E0;        //From MainModule Baseaddress
        public static readonly uint ObjectManagerOffset = 0x463C;           //From ObjectManagerPointer
        public static readonly uint ObjectManagerFirstObjectAddress = 0xC0;
        public static readonly uint ObjectManagerNextObjectAddress = 0x3C;
        public static readonly uint ObjectManagerLocalGUID = 0x30;
        public static readonly uint ObjectManagerSpellID = 0x1A4;
        public static readonly uint ObjectManagerLocalDescriptorArray = 0xC;
        public static readonly uint ObjectManagerLocalMovementArray = 0x100;

        public static readonly uint DescriptorArrayClass8 = 0x55;

        public static readonly uint MovementArrayIsMoving8 = 0x38;

        public static readonly uint ObjectManagerSpellPosX = 0x110;
        public static readonly uint ObjectManagerSpellPosY = 0x114;
        public static readonly uint ObjectManagerSpellPosZ = 0x118;

        public static readonly uint ObjectManagerUnitPosX = 0x790;
        public static readonly uint ObjectManagerUnitPosY = 0x794;
        public static readonly uint ObjectManagerUnitPosZ = 0x798;
        public static readonly uint ObjectManagerUnitRotation = 0x7A0;


        public static readonly uint GlobalInfoPlayerGUID = 0x9BE818;

    }
}
