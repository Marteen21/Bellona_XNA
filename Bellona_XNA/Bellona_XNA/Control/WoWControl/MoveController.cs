using Bellona_XNA.MemoryReading;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Control.WoWControl {
    class MoveController {
        #region MovementControlling
        public static bool RotateTowards(WoWPlayer PlayerUnit, Vector2 TargetUnitPos, double RotationThreshhold, bool DisableForward) {
            double mydiff = Angles.AngleDiff(Angles.Calculateangle(TargetUnitPos, PlayerUnit.Position), PlayerUnit.Rotation);
            if ((Math.Abs(mydiff) < RotationThreshhold)) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft, ref PlayerUnit.MovingInfo.myleft, PlayerUnit.WindowTitle);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight, ref PlayerUnit.MovingInfo.myright, PlayerUnit.WindowTitle);
                return true;
            }
            else if (mydiff < 0) {
                if (DisableForward) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref PlayerUnit.MovingInfo.myforward, PlayerUnit.WindowTitle);
                }
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_LEFT, !PlayerUnit.MovingInfo.IsTurningLeft, ref PlayerUnit.MovingInfo.myleft, PlayerUnit.WindowTitle);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight, ref PlayerUnit.MovingInfo.myright, PlayerUnit.WindowTitle);
            }
            else if (mydiff > 0) {
                if (DisableForward) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref PlayerUnit.MovingInfo.myforward, PlayerUnit.WindowTitle);
                }
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_RIGHT, !PlayerUnit.MovingInfo.IsTurningRight, ref PlayerUnit.MovingInfo.myright, PlayerUnit.WindowTitle);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft, ref PlayerUnit.MovingInfo.myleft, PlayerUnit.WindowTitle);
            }
            return false;
        }

        //protected bool RotateTowards(WoWUnit PlayerUnit, WoWUnit TargetUnit, double RotationThreshhold, bool DisableForward) {
        //    return RotateTowards(PlayerUnit, TargetUnit.Position, RotationThreshhold, DisableForward);
        //}
        public static bool WalkingTowards(WoWPlayer PlayerUnit, Vector2 mTargetPosition, double mPositionThreshold) {
            if (Vector2.Distance(new Vector2(PlayerUnit.Position.X, PlayerUnit.Position.Y), mTargetPosition) < mPositionThreshold) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref PlayerUnit.MovingInfo.myforward, PlayerUnit.WindowTitle);
                return true;
            }
            else {
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_UP, !PlayerUnit.MovingInfo.IsMovingForward, ref PlayerUnit.MovingInfo.myforward, PlayerUnit.WindowTitle);
                return false;
            }
        }
        //protected bool WalkingTowards(WoWUnit mPlayerUnit, WoWUnit mTargetUnit, double mPositionThreshhold) {
        //    return WalkingTowards(mPlayerUnit, mTargetUnit.Position, mPositionThreshhold);
        //}
        protected void Halt(WoWPlayer PlayerUnit) {
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref PlayerUnit.MovingInfo.myforward, PlayerUnit.WindowTitle);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft, ref PlayerUnit.MovingInfo.myleft, PlayerUnit.WindowTitle);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight, ref PlayerUnit.MovingInfo.myright, PlayerUnit.WindowTitle);
        }
        #endregion
    }
}
