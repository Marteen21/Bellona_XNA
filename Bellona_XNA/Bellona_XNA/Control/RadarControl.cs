using Bellona_XNA.Draw;
using Bellona_XNA.MemoryReading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bellona_XNA.Control {
    class RadarControl {
        private Vector2 startPoint = new Vector2();
        private Rectangle selectionBox = new Rectangle();
        private bool selecting;
        private bool needsToDraw;
        public RadarControl() {

        }
        public void SelectionBoxRefresh() {
            MouseState mymouse = Mouse.GetState();
            if (mymouse.LeftButton == ButtonState.Pressed) {
                if (selecting) {
                    selectionBox.Width = (int)Math.Abs(mymouse.X - startPoint.X);
                    selectionBox.Height = (int)Math.Abs(mymouse.Y - startPoint.Y);
                    //Set the start point to the top left, which is the minimum X and minimum Y
                    //Choose between the start point and the current mouse position
                    selectionBox.X = (int)Math.Min(startPoint.X, mymouse.X);
                    selectionBox.Y = (int)Math.Min(startPoint.Y, mymouse.Y);
                    needsToDraw = true;
                }
                else {
                    selecting = true;
                    startPoint.X = mymouse.X;
                    startPoint.Y = mymouse.Y;
                    selectionBox.X = mymouse.X;
                    selectionBox.Y = mymouse.Y;
                }
            }
            else {
                needsToDraw = false;
                selecting = false;
            }
        }
        public void CommandRefresh(ref List<WoWPlayer> allplayers, Game mygame) {
            MouseState mymouse = Mouse.GetState();
            if (mymouse.LeftButton == ButtonState.Pressed && mygame.IsActive) {
                if (selecting) {
                    selectionBox.Width = (int)Math.Abs(mymouse.X - startPoint.X);
                    selectionBox.Height = (int)Math.Abs(mymouse.Y - startPoint.Y);
                    //Set the start point to the top left, which is the minimum X and minimum Y
                    //Choose between the start point and the current mouse position
                    selectionBox.X = (int)Math.Min(startPoint.X, mymouse.X);
                    selectionBox.Y = (int)Math.Min(startPoint.Y, mymouse.Y);
                    needsToDraw = true;
                }
                else {
                    selecting = true;
                    startPoint.X = mymouse.X;
                    startPoint.Y = mymouse.Y;
                    selectionBox.X = mymouse.X;
                    selectionBox.Y = mymouse.Y;
                }
            }
            else {
                if (needsToDraw) {
                    SetSelectedPlayers(ref allplayers);
                }
                needsToDraw = false;
                selecting = false;
            }
            if (mymouse.RightButton == ButtonState.Pressed && mygame.IsActive) {
                Vector2 mousevector = new Vector2(mymouse.X, mymouse.Y);
                Console.WriteLine(CoordinateChanger.AbsoluteToRelative(mousevector, new Vector2(720, 720)));
                foreach (WoWPlayer wp in allplayers) {
                    if (wp.RPlayer.Selected) {
                        wp.MovementTarget=CoordinateChanger.RelativeToWorld(CoordinateChanger.AbsoluteToRelative(mousevector, new Vector2(720, 720)), Game1.mainPlayer.Position);
                    }
                }

            }
        }
        public void DrawSelectBox(ExtendedSpriteBatch mybatch) {
            if (needsToDraw) {
                mybatch.DrawRectangle(selectionBox, Color.White);
            }
        }
        public void SetSelectedPlayers(ref List<WoWPlayer> allplayers) {
            foreach (WoWPlayer wp in allplayers) {
                if (wp.RPlayer.AbsoluteRadarPos.X > selectionBox.X &&
                    wp.RPlayer.AbsoluteRadarPos.X < selectionBox.X + selectionBox.Width &&
                    wp.RPlayer.AbsoluteRadarPos.Y > selectionBox.Y &&
                    wp.RPlayer.AbsoluteRadarPos.Y < selectionBox.Y + selectionBox.Height) {
                    wp.RPlayer.Selected = true;
                }
                else {
                    wp.RPlayer.Selected = false;
                }
            }
        }
    }
}
