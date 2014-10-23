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
            foreach (var entity in entities) {
                spriteBatch.Draw(textures[entity.Key], entity.Value, Color.Green);

                // Draw ui for selecting characters

                // Should probably be done by UI class or something

                if(entity.Key == selectedEntity)
                    spriteBatch.DrawString(fonts["normal"], entity.Key, new Vector2(20, 750), Color.Yellow); 
                else 
                    spriteBatch.DrawString(fonts["normal"], entity.Key, new Vector2(120, 750), Color.Black); 
            }
        }
    }
}
