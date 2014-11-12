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
                return IsValidOnGrid(characterMove) && AnyMatchingPosition(GetNearestValidPositions(), characterMove);
		    }
			return true;
		}

        public IEnumerable<Vector2> GetNearestValidPositions() {
            Vector2 position = CurrentEntity.Position;
            return Background.GetNearestPositions((int)position.X, (int)position.Y, CurrentEntity.MoveLength / 10, CurrentEntity.MoveLength).Where(p => !SquareOccupied(p));
        }

        private bool IsValidOnGrid(Vector2 characterMove) {
            return Background.IsValid((int)characterMove.X, (int)characterMove.Y);
        }

        private bool SquareOccupied(Vector2 position) {
            return AnyMatchingPosition(Entities.Select(e => e.Position), position);
        }

        private bool AnyMatchingPosition(IEnumerable<Vector2> enumerable, Vector2 position) {
            return enumerable.Any(p => p.X == position.X && p.Y == position.Y);
        }

        private Vector2 GetCharacterMove(string action) {
            string[] arr = action.Split(';')[1].Split(',');
            return new Vector2(int.Parse(arr[0]), int.Parse(arr[1]));
        }
    }
}
