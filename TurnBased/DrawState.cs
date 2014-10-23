using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public abstract class DrawState {
        public abstract void Draw(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures, Dictionary<string, SpriteFont> fonts);
    }
}
