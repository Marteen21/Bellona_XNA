using Bellona_XNA.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Radar {
    class RadarUnit : RadarObject {
        public static Texture2D UnitTexture;
        protected Color drawColor = Color.Black;
        
        public RadarUnit(Vector2 rPos, float rRot, Color myColor) : base (rPos, rRot, RadarUnit.UnitTexture) {
            this.drawColor = myColor;
        }
        public override void DrawObject(ExtendedSpriteBatch myBatch) {
            SetAbsoluteRadarPos(myBatch.GraphicsDevice.Viewport.Width, myBatch.GraphicsDevice.Viewport.Height);
            if (this.texture != null) {
                myBatch.Draw(this.texture, AbsoluteRadarPos, null, this.drawColor, RadarRot, new Vector2(this.texture.Width / 2, this.texture.Height / 2), 0.08f, SpriteEffects.None, 0f);
            }
        }
    }
}
