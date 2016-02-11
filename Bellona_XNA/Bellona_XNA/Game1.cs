using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Bellona_XNA.Draw;
using Bellona_XNA.Control;
using Bellona_XNA.Radar;
using Bellona_XNA.MemoryReading;
using Bellona_XNA.Control.WoWControl;
using Bellona_XNA.WinForms;

namespace Bellona_XNA {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        ExtendedSpriteBatch spriteBatch;
        private IntPtr drawSurface;

        RadarControl radarctrl;

        List<WoWUnit> allUnits = new List<WoWUnit>();
        List<WoWPlayer> allPlayers = new List<WoWPlayer>();
        List<WoWSpell> allSpells = new List<WoWSpell>();


        WoWConnection mainwow;
        public static WoWPlayer mainPlayer;
        public static bool GameEnabled;

        TimeSpan previousRefreshTime = TimeSpan.Zero;
        public Game1(IntPtr drawSurface) {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 600;
            Content.RootDirectory = "Content";
            this.drawSurface = drawSurface;
            Mouse.WindowHandle = drawSurface;
            graphics.PreparingDeviceSettings +=
            new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged +=
            new EventHandler(Game1_VisibleChanged);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e) {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
            drawSurface;
        }

        private void Game1_VisibleChanged(object sender, EventArgs e) {
            if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
        }

        protected override void Initialize() {
            base.Initialize();
        }
        public bool ConnectToMainWoW(string windowTitle) {
            GameEnabled = false;
            radarctrl = new RadarControl();
            mainwow = new WoWConnection(windowTitle);
            if (!mainwow.TryToConnect()) {
                throw new Exception();
            }
            mainPlayer = new WoWPlayer(mainwow.Connection.ReadUInt64((uint)mainwow.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoPlayerGUID));
            GameEnabled = true;
            return true;
        }


        #region ResourceLoading
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);
            RadarUnit.UnitTexture = Content.Load<Texture2D>("arrow_without_border");
            RadarPlayer.PlayerTexture = Content.Load<Texture2D>("arrow");
            RadarSpell.SpellTexture = Content.Load<Texture2D>("donut");

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        } 
        #endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape)) {
                this.Exit();
            }
            if (GameEnabled) {
                radarctrl.CommandRefresh(ref allPlayers, this);
                Refresh(gameTime.TotalGameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.FillRectangle(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 5, 5), Color.Red);

            if (GameEnabled) {
                foreach (WoWUnit rp in allUnits) {
                    rp.RUnit.DrawObject(spriteBatch);
                }
                foreach (WoWPlayer rp in allPlayers) {
                    rp.RPlayer.DrawObject(spriteBatch);
                }
                foreach (WoWSpell rs in allSpells) {
                    rs.RSpell.DrawObject(spriteBatch);
                }
                radarctrl.DrawSelectBox(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void Refresh(TimeSpan totalgameTime) {
            if ((totalgameTime - previousRefreshTime) > TimeSpan.FromMilliseconds(100)) {
                previousRefreshTime = totalgameTime;
                mainwow.TryToRefreshObjectManager();
                WoWObject.GetAllObjects(ref allUnits, ref allSpells, ref allPlayers, mainwow);
                mainPlayer.RefreshFromList(allPlayers, mainPlayer.WindowTitle);
                
                foreach (WoWPlayer wp in allPlayers) {
                    if (wp.WindowTitle != null && Vector2.Distance(wp.MovementTarget, new Vector2(wp.Position.X, wp.Position.Y)) < 80) {
                        if (MoveController.RotateTowards(wp, wp.MovementTarget, (double)0.06 * Math.PI, true)) {
                            MoveController.WalkingTowards(wp, wp.MovementTarget, 5);
                        }
                    }
                }
            }
        }
    }
}
