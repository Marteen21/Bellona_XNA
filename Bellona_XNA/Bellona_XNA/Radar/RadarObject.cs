using Bellona_XNA.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Radar {
    class RadarObject {
        public static float RadarZoom = 4;
        private Vector2 relativeRadarPosToPlayer;
        private float radarRot;
        protected Texture2D texture;
        private Vector2 absoluteRadarPos;

        public Vector2 AbsoluteRadarPos {
            get {
                return absoluteRadarPos;
            }

            set {
                absoluteRadarPos = value;
            }
        }

        public Vector2 RelativeRadarPosToPlayer {
            get {
                return relativeRadarPosToPlayer;
            }

            set {
                relativeRadarPosToPlayer = value;
            }
        }

        public float RadarRot {
            get {
                return radarRot;
            }

            set {
                radarRot = value;
            }
        }

        public RadarObject(Vector2 rPos, float rRot, Texture2D txtr) {
            this.RelativeRadarPosToPlayer = rPos;
            this.RadarRot = rRot;
            this.texture = txtr;
        }
        public void SetAbsoluteRadarPos(int viewportwidth, int viewportheight) {
            this.AbsoluteRadarPos = this.RelativeRadarPosToPlayer + new Vector2(viewportwidth / 2, viewportheight / 2);
        }
        public virtual void DrawObject(ExtendedSpriteBatch myBatch) {
            SetAbsoluteRadarPos(myBatch.GraphicsDevice.Viewport.Width, myBatch.GraphicsDevice.Viewport.Height);
            if (this.texture != null) {
                myBatch.Draw(this.texture, this.AbsoluteRadarPos, null, Color.Brown, RadarRot, new Vector2(this.texture.Width / 2, this.texture.Height / 2), 0.08f, SpriteEffects.None, 0f);
            }
        }
    }
}
