using Bellona_XNA.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Maps {
    class WoWMap {
        private string name;
        Texture2D img;
        Vector2 posTopLeft;
        Vector2 posBottomRight;
        Vector2 origin;
        float angle;
        Vector2 imgSize = new Vector2(600, 600);

        public Texture2D Img {
            get {
                return img;
            }

            set {
                img = value;
            }
        }

        public float Angle {
            get {
                return angle;
            }

            set {
                angle = value;
            }
        }

        public WoWMap(Game myGame, string textureName, Vector2 orgn, float angl) {
            this.name = textureName;
            this.imgSize = new Vector2(600, 600);
            this.Img = myGame.Content.Load<Texture2D>(textureName);
            this.origin = orgn;
            this.Angle = angl;
        }
        public Vector2 whereToDraw(Vector3 PlayerPos) {
            return CoordinateChanger.WorldToRelative(this.origin,PlayerPos)+ new Vector2(300,300);
        }

        public static WoWMap Arena_BE(Game mygame) {
            string nm = "circle_of_blood";
            Vector2 orgn = new Vector2(2840.6f, 5925.5f);
            return new WoWMap(mygame, nm, orgn, 50*(float)Math.PI/180);
        }
    }
}
