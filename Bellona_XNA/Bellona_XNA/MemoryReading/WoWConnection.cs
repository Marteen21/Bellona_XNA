using Magic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    public class WoWConnection {
        private string windowTitle;
        private BlackMagic connection;
        private bool connected = false;
        private uint objectManagerAddress;
        private uint firstObjectAddress;

        public BlackMagic Connection {
            get {
                return connection;
            }

            set {
                connection = value;
            }
        }

        public uint FirstObjectAddress {
            get {
                return firstObjectAddress;
            }

            set {
                firstObjectAddress = value;
            }
        }

        public string WindowTitle {
            get {
                return windowTitle;
            }

            set {
                windowTitle = value;
            }
        }

        public WoWConnection(string wTitle) {
            this.WindowTitle = wTitle;
        }
        public bool TryToConnect() {
            try {
                this.Connection = new BlackMagic();
                this.Connection.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(this.WindowTitle));
                if (TryToRefreshObjectManager()) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }
        public bool TryToRefreshObjectManager() {
            try {
                this.objectManagerAddress = this.Connection.ReadUInt(this.Connection.ReadUInt((uint)this.Connection.MainModule.BaseAddress + MemoryOffsets.ObjectManagerPointer) + MemoryOffsets.ObjectManagerOffset); ;
                this.FirstObjectAddress = this.Connection.ReadUInt(this.objectManagerAddress + MemoryOffsets.ObjectManagerFirstObjectAddress);
                return true;
            }
            catch {
                return false;
            }
        }
        public ulong GetPlayerGUID() {
            return this.Connection.ReadUInt64((uint)this.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoPlayerGUID);
        }
    }
}
