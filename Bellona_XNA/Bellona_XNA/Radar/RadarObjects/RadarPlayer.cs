using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bellona_XNA.Draw;
using Microsoft.Xna.Framework.Graphics;

namespace Bellona_XNA.Radar {
    class RadarPlayer : RadarObject {
        private bool selected;
        public static Texture2D PlayerTexture;
        protected Color drawColor = Color.AntiqueWhite;
        public bool Selected {
            get {
                return selected;
            }

            set {
                selected = value;
            }
        }

        public RadarPlayer(Vector2 rPos, float rRot, Color myColor) : base(rPos, rRot, RadarPlayer.PlayerTexture) {
            this.drawColor = myColor;
        }
        public override void DrawObject(ExtendedSpriteBatch myBatch) {
            SetAbsoluteRadarPos(myBatch.GraphicsDevice.Viewport.Width, myBatch.GraphicsDevice.Viewport.Height);
            if (this.texture != null) {
                myBatch.Draw(this.texture, this.AbsoluteRadarPos, null, this.drawColor, RadarRot, new Vector2(this.texture.Width / 2, this.texture.Height / 2), 0.08f, SpriteEffects.None, 0f);
                if (selected) {
                    myBatch.DrawRectangle(new Rectangle((int)this.AbsoluteRadarPos.X- 15, (int)this.AbsoluteRadarPos.Y - 15, 30, 30), Color.Chartreuse);
                }                
            }
        }

    }
}
