using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class Renderer {
        private Texture2D texturePlayer;
        private Texture2D textureAI;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public Renderer(ContentManager content) {
            textures.Add("Rambo", TextureManager.CreateTexture(GraphicsDevice, 20, 20, Color.Green));
            textures.Add("Terminator", TextureManager.CreateTexture(GraphicsDevice, 20, 20, Color.Green));
            textures.Add("red", TextureManager.CreateTexture(GraphicsDevice, 20, 20, Color.Red));
            textures.Add("bkg", content.Load<Texture2D>("spaceship32x32"));
			
            fonts.Add("normal", content.Load<SpriteFont>("monolight12"));
        }

        // character info for UI
        // - current weapon/spell
        // - current HP
        // - current action points
        // Other SELECTABLE characters (like characters that has skipped or has no energy left is not selectable)

        // for all characters 
        // - current HP (maybe?)
        // - sprite
        // - position

        // Also needs to be able to draw UI for other things
        // - paused
        // - menu
        // - inventory?

        public void Draw(SpriteBatch spriteBatch, WorldModel model) {
            DrawWorld(spriteBatch, model);
			DrawUI(spriteBatch, model);
        }
		
		private void DrawWorld(SpriteBatch spriteBatch, WorldModel model) {
			model.DrawBackground(spriteBatch, textures, fonts);
            foreach(DrawState drawState in states) {
                drawState.Draw(spriteBatch, textures, fonts);
            }
		}
		
		private void DrawUI(SpriteBatch spriteBatch, WorldModel model) {
		}
    }
}
