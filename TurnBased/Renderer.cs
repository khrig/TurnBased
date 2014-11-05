using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class Renderer {
        private int tileSizeX = 64, tileSizeY = 64;
		private int tileCenter = 32; 
		private int windowWidth, windowHeight;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public Renderer(int windowWidth, int windowHeight, ContentManager content) {
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
		
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
			model.Background.ForeachTile((x, y, sprite) => {
                DrawTile(spriteBatch, new Vector2(x * tileSizeX, y * tileSizeY), textures[sprite], 2);
            });
			
			foreach(Entity entity in model.Entities) {
				DrawTile(spriteBatch, ConvertToGridCenterPosition(entity.Position.X, entity.Position.Y), textures[entity.Name]);
			}
		}
		
		private void DrawUI(SpriteBatch spriteBatch, WorldModel model) {
			int startx = 40;
            for (int i = 0; i < entities.Count; i++) {
                // Draw ui for showing selectable characters
				var color = model.CurrentEntity.Name != null && model.Entities[i].Name == model.CurrentEntity.Name ? Color.Yellow : Color.White;
                spriteBatch.DrawString(fonts["normal"], model.Entities[i].Name, new Vector2(startx + i*100, windowHeight - 70), color); 
            }
		}

        private Vector2 ConvertToGridCenterPosition(int x, int y, Texture2D texture) {
            int middleXMinusHalfSpriteWidth = tileCenter - (texture.White/2), middleYMinusHalfSpriteHeight = tileCenter - (texture.Height/2);
            return new Vector2(x - (x % tileSizeX) + middleXMinusHalfSpriteWidth, y - (y % tileSizeY) + middleYMinusHalfSpriteHeight);
        }
		
        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite) {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite, float scale) {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
