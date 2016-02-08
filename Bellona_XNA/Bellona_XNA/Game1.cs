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

namespace Bellona_XNA {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        ExtendedSpriteBatch spriteBatch;

        RadarControl radarctrl;

        List<WoWUnit> allUnits = new List<WoWUnit>();
        List<WoWPlayer> allPlayers = new List<WoWPlayer>();
        List<WoWSpell> allSpells = new List<WoWSpell>();

        WoWConnection mainwow;
        public static WoWPlayer mainPlayer;

        TimeSpan previousRefreshTime = TimeSpan.Zero;
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            radarctrl = new RadarControl();
            mainwow = new WoWConnection("World of Warcraft");
            if (!mainwow.TryToConnect()) {
                throw new Exception();
            }
            mainPlayer = new WoWPlayer(mainwow.Connection.ReadUInt64((uint)mainwow.Connection.MainModule.BaseAddress + MemoryOffsets.GlobalInfoPlayerGUID));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);
            RadarUnit.UnitTexture = Content.Load<Texture2D>("arrow_without_border");
            RadarPlayer.PlayerTexture = Content.Load<Texture2D>("arrow");
            RadarSpell.SpellTexture = Content.Load<Texture2D>("donut");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

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
            radarctrl.CommandRefresh(ref allPlayers, this);
            Refresh(gameTime.TotalGameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.FillRectangle(new Rectangle(Mouse.GetState().X,Mouse.GetState().Y, 5, 5), Color.Red);


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

            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void Refresh(TimeSpan totalgameTime) {
            if ((totalgameTime - previousRefreshTime) > TimeSpan.FromMilliseconds(100)) {
                previousRefreshTime = totalgameTime;
                mainwow.TryToRefreshObjectManager();
                WoWObject.GetAllObjects(ref allUnits, ref allSpells, ref allPlayers, mainwow);
                mainPlayer.RefreshFromList(allPlayers, "World of Warcraft");
                foreach(WoWPlayer wp in allPlayers) {
                    if(wp.WindowTitle != null && Vector2.Distance(wp.MovementTarget, new Vector2(wp.Position.X,wp.Position.Y)) < 50) {
                        if (MoveController.RotateTowards(wp, wp.MovementTarget, (double)0.06*Math.PI, true)) {
                            MoveController.WalkingTowards(wp, wp.MovementTarget, 5);
                        }
                    }
                }
            }
        }
    }
    
}
