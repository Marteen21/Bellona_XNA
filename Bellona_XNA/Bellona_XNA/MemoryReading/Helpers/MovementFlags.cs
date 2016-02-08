using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading.Helpers {
    class MovementFlags {
        private bool isMoving;
        private bool isMovingForward;
        private bool isMovingBackward;
        private bool isMovingStraightLeft;
        private bool isMovingStraightRight;
        private bool isTurningLeft;
        private bool isTurningRight;
        public bool myleft = false;
        public bool myright = false;
        public bool myforward = false;

        public bool IsMoving {
            get {
                return isMoving;
            }

            set {
                isMoving = value;
            }
        }

        public bool IsMovingForward {
            get {
                return isMovingForward;
            }

            set {
                isMovingForward = value;
            }
        }

        public bool IsMovingBackward {
            get {
                return isMovingBackward;
            }

            set {
                isMovingBackward = value;
            }
        }

        public bool IsMovingStraightLeft {
            get {
                return isMovingStraightLeft;
            }

            set {
                isMovingStraightLeft = value;
            }
        }

        public bool IsMovingStraightRight {
            get {
                return isMovingStraightRight;
            }

            set {
                isMovingStraightRight = value;
            }
        }

        public bool IsTurningLeft {
            get {
                return isTurningLeft;
            }

            set {
                isTurningLeft = value;
            }
        }

        public bool IsTurningRight {
            get {
                return isTurningRight;
            }

            set {
                isTurningRight = value;
            }
        }

        public MovementFlags(byte flagByte) {
            IsMoving = flagByte != 0;
            IsMovingForward = (flagByte & 0x01) != 0;       //2^0=1
            IsMovingBackward = (flagByte & 0x02) != 0;      //2^1=2
            IsMovingStraightLeft = (flagByte & 0x04) != 0;  //2^2=4
            IsMovingStraightRight = (flagByte & 0x08) != 0; //2^3=8
            IsTurningLeft = (flagByte & 0x10) != 0;         //2^4=16
            IsTurningRight = (flagByte & 0x20) != 0;        //2^4=32
        }
    }
}
