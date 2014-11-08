using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.Interfaces;

namespace TurnBased {
    public class Renderer {
        private readonly IWorldViewSettings worldViewSettings;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public Renderer(IWorldViewSettings worldViewSettings, ContentManager content, GraphicsDevice graphicsDevice) {
            this.worldViewSettings = worldViewSettings;

            textures.Add("Rambo", content.Load<Texture2D>("marine1-t"));
            textures.Add("Terminator", content.Load<Texture2D>("marine3-t"));
            textures.Add("black", TextureManager.CreateTexture(graphicsDevice, 1, 1, Color.Black));
            //textures.Add("bkg", content.Load<Texture2D>("spaceship32x32"));
            textures.Add("sand", content.Load<Texture2D>("sanddirt"));
            textures.Add("sand32", content.Load<Texture2D>("sanddirt32"));
            textures.Add("bkg", content.Load<Texture2D>("sanddirt32"));
			
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
            DrawBackground(spriteBatch, model);
            DrawGridLines(spriteBatch, model);
            DrawEntities(spriteBatch, model);
		}

        private void DrawBackground(SpriteBatch spriteBatch, WorldModel model) {
            model.Background.ForeachTile((x, y, sprite) => DrawTile(spriteBatch, ConvertToViewPosition(x, y), textures[sprite], 2));
        }

        public void DrawGridLines(SpriteBatch spriteBatch, WorldModel model) {
            model.Background.ForeachColumn((x) => {
                Rectangle rectangle = new Rectangle((int)(x * worldViewSettings.TileSizeX), 0, 1, worldViewSettings.GridBounds.Height);
                spriteBatch.Draw(textures["black"], rectangle, Color.Red);
            });
            model.Background.ForeachRow((y) => {
                Rectangle rectangle = new Rectangle(0, (int)(y * worldViewSettings.TileSizeY), worldViewSettings.GridBounds.Width, 1);
                spriteBatch.Draw(textures["black"], rectangle, Color.Red);
            });
        }

        private void DrawEntities(SpriteBatch spriteBatch, WorldModel model) {

            foreach (Entity entity in model.Entities) {
                // brutally shitty code
                if (entity.Name == "Rambo" || entity.Name == "Terminator") {
                    Rectangle sourcerect = new Rectangle(24, 64, 24, 32);
                    DrawTile(spriteBatch, ConvertToGridCenterPosition(entity.Position.X * worldViewSettings.TileSizeX, entity.Position.Y * worldViewSettings.TileSizeY, textures[entity.Name], sourcerect.Width, sourcerect.Height), textures[entity.Name], sourcerect);
                }
                else
                    DrawTile(spriteBatch, ConvertToGridCenterPosition(entity.Position.X * worldViewSettings.TileSizeX, entity.Position.Y * worldViewSettings.TileSizeY, textures[entity.Name], textures[entity.Name].Width, textures[entity.Name].Height), textures[entity.Name]);
            }
        }
		
		private void DrawUI(SpriteBatch spriteBatch, WorldModel model) {
			int startx = 40;
            for (int i = 0; i < model.Entities.Count; i++) {
                // Draw ui for showing selectable characters
				var color = model.CurrentEntity != null && model.Entities[i].Name == model.CurrentEntity.Name ? Color.Yellow : Color.White;
                spriteBatch.DrawString(fonts["normal"], model.Entities[i].Name, new Vector2(startx + i * 100, worldViewSettings.WindowHeight - 70), color); 
            }
		}

        private Vector2 ConvertToGridCenterPosition(float x, float y, Texture2D texture, int textureWidth, int textureHeight) {
            int middleXMinusHalfSpriteWidth = worldViewSettings.TileCenter - (textureWidth / 2);
            int middleYMinusHalfSpriteHeight = worldViewSettings.TileCenter - (textureHeight / 2);
            return new Vector2(x - (x % worldViewSettings.TileSizeX) + middleXMinusHalfSpriteWidth, y - (y % worldViewSettings.TileSizeY) + middleYMinusHalfSpriteHeight);
		}

        private Vector2 ConvertToViewPosition(int x, int y) {
            return new Vector2(worldViewSettings.GridBounds.Location.X + (x * worldViewSettings.TileSizeX), worldViewSettings.GridBounds.Location.Y + (y * worldViewSettings.TileSizeY));
        }
    
        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite) {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite, float scale) {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D texture, Rectangle frameRect) {
            spriteBatch.Draw(texture, position, frameRect, Color.White);
        }
    }
}
