using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class EntityDrawState : DrawState {
        public List<KeyValuePair<string, Vector2>> entities;

        public EntityDrawState(List<KeyValuePair<string, Vector2>> entities) {
            this.entities = entities;
        }

        public override void Draw(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures) {
            foreach (var entity in entities) {
                spriteBatch.Draw(textures[entity.Key], entity.Value, Color.Green);
            }
        }
    }
}
