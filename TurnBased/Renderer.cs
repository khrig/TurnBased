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

        public Renderer(Texture2D texturePlayer, Texture2D textureAI, SpriteFont font) {
            textures.Add("Rambo", texturePlayer);
            textures.Add("Terminator", texturePlayer);
            textures.Add("red", textureAI);
            
            fonts.Add("normal", font);
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

        public void DrawStateModel(SpriteBatch spriteBatch, StateManager stateManager) {
            var model = stateManager.GetModel();
            while (model.States.Count != 0) {
                DrawState drawState = model.States.Pop();
                drawState.Draw(spriteBatch, textures, fonts);
            }
        }
    }
}
