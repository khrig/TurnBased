using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class EntityDrawState : DrawState {
        private readonly List<KeyValuePair<string, Vector2>> entities;
        private readonly string selectedEntity;

        public EntityDrawState(List<KeyValuePair<string, Vector2>> entities, string selectedEntity) {
            this.entities = entities;
            this.selectedEntity = selectedEntity;
        }

        public override void Draw(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures, Dictionary<string, SpriteFont> fonts) {
            int startx = 20;
            for (int i = 0; i < entities.Count; i++) {
                spriteBatch.Draw(textures[entities[i].Key], entities[i].Value, Color.Green);
                // Draw ui for showing selectable characters
                // Should probably be done by UI class or something
                spriteBatch.DrawString(fonts["normal"], entities[i].Key, new Vector2(startx + i*100, 750),
                    entities[i].Key == selectedEntity ? Color.Yellow : Color.Black); 
            }
        }
    }
}
