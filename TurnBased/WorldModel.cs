using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class WorldModel {
        public Grid Background { get; set; }
		public List<Entity> Entities { get; set; }
		public Entity CurrentEntity { get; set; }

        public WorldModel(Grid background) {
            Background = background;
			Entities = new List<Entity>();
			CurrentEntity = null;
        }
		
		public bool Valid(string action) {
		    if (action.StartsWith("move")) {
		        Vector2 characterMove = GetCharacterMove(action);
		        return !Entities.Any(e => e.Position.X == characterMove.X && e.Position.Y == characterMove.Y);
		    }
			return true;
		}

        private Vector2 GetCharacterMove(string action) {
            string[] arr = action.Split(';')[1].Split(',');
            return Vector2(int.Parse(arr[0]), int.Parse(arr[1]));
        }
    }
}
