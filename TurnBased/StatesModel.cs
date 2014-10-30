using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class StatesModel {
        public Stack<DrawState> States { get; set; }
        private readonly Grid background;
        private int tileSizeX = 64, tileSizeY = 64;

        public StatesModel(Grid background) {
            States = new Stack<DrawState>();
            this.background = background;
        }

        public void DrawBackground(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures, Dictionary<string, SpriteFont> fonts) {
            background.ForeachTile((x, y, sprite) => {
                DrawTile(spriteBatch, new Vector2(x * (int)tileSizeX, y * (int)tileSizeY), textures[sprite], 2);
            });
        }

        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite) {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        private void DrawTile(SpriteBatch spriteBatch, Vector2 position, Texture2D sprite, float scale) {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
