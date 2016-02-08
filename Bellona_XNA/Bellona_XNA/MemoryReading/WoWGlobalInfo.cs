using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.MemoryReading {
    class WoWGlobalInfo {
        private ulong mainPlayerGUID;
        private List<ulong> playerGUIDs = new List<ulong>();
        public bool TryToRefreshGlobalInfo(WoWConnection wowConnection) {
            try {
                this.mainPlayerGUID = wowConnection.Connection.ReadUInt((uint)wowConnection.Connection.MainModule.BaseAddress+MemoryOffsets.GlobalInfoPlayerGUID);
                if (!playerGUIDs.Contains(this.mainPlayerGUID)) {
                    playerGUIDs.Add(this.mainPlayerGUID);
                }
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
