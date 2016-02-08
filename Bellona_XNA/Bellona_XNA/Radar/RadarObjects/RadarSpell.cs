using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bellona_XNA.Draw;

namespace Bellona_XNA.Radar {
    class RadarSpell : RadarObject {
        public static Texture2D SpellTexture;
        private float radius;   //yard
        public RadarSpell(Vector2 rPos, float rRot) : base (rPos,rRot,RadarSpell.SpellTexture) {
            radius = 15;
        }
        public override void DrawObject(ExtendedSpriteBatch myBatch) {
            SetAbsoluteRadarPos(myBatch.GraphicsDevice.Viewport.Width, myBatch.GraphicsDevice.Viewport.Height);
            if (this.texture != null) {
                myBatch.Draw(this.texture, this.AbsoluteRadarPos, null, Color.DarkRed, RadarRot, new Vector2(this.texture.Width / 2, this.texture.Height / 2), 0.005f*radius*RadarObject.RadarZoom, SpriteEffects.None, 0f);
            }
        }

    }
}
