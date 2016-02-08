using Bellona_XNA.MemoryReading;
using Bellona_XNA.Radar;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Control {
    class CoordinateChanger {
        public static Vector2 AbsoluteToRelative (Vector2 abs,Vector2 viewPort) {
            return abs - (viewPort/2);
        }
        public static Vector2 RelativeToWorld (Vector2 relative, Vector3 playerPos) {
            return ((relative / RadarObject.RadarZoom) + new Vector2(playerPos.X, playerPos.Y));
        }
    }
    class Angles {
        public static double Calculateangle(WoWPlayer Target, WoWPlayer Player) {
            float vx = Target.Position.X - Player.Position.X;
            float vy = Target.Position.Y - Player.Position.Y;
            return Math.Sign(vy) * Math.Acos((vx * 1 + vy * 0) / (Math.Sqrt(vx * vx + vy * vy)));
        }
        public static double Calculateangle(Vector2 TargetPos, Vector3 PlayerPos) {
            float vx = TargetPos.X - PlayerPos.X;
            float vy = TargetPos.Y - PlayerPos.Y;
            return Math.Sign(vy) * Math.Acos((vx * 1 + vy * 0) / (Math.Sqrt(vx * vx + vy * vy)));
        }
        public static double AngleDiff(double rad2, double rad1) {
            double result = (rad1 - rad2);
            double temp2 = (rad1 - rad2 + 2 * Math.PI);
            if (Math.Abs(temp2) == Math.Min(Math.Abs(result), Math.Abs(temp2))) {
                result = temp2;
            }
            double temp3 = (rad1 - rad2 - 2 * Math.PI);
            if (Math.Abs(temp3) == Math.Min(Math.Abs(result), Math.Abs(temp3))) {
                result = temp3;
            }
            return result;
        }
        public static double Todegree(double rad) {
            return 180 * rad / Math.PI;
        }
    }
}
