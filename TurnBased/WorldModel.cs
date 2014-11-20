using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

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
		    if (action.StartsWith("click")) {
		        Vector2 characterMove = GetPosition(action);
                return IsValidOnGrid(characterMove) && AnyMatchingPosition(GetNearestValidPositions(), characterMove);
		    }
			return true;
		}

        public bool IsEnemyTargeted(string action) {
            if (action.StartsWith("click")) {
                Vector2 characterMove = GetPosition(action);
                return SquareOccupied(characterMove);
            }
            return false;
        }

        public IEnumerable<Vector2> GetNearestValidPositions() {
            Vector2 position = CurrentEntity.Position;
            return Background.GetNearestPositions((int)position.X, (int)position.Y, CurrentEntity.MoveLength / 10, CurrentEntity.MoveLength).Where(p => !SquareOccupied(p));
        }

        public Entity GetEntityAt(string action) {
            Vector2 position = GetPosition(action);
            return Entities.Find(p => p.Position.X == position.X && p.Position.Y == position.Y);
        }

        public Vector2 GetPosition(string action) {
            string[] arr = action.Split(';')[1].Split(',');
            return new Vector2(int.Parse(arr[0]), int.Parse(arr[1]));
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

        public void RemoveDestroyedEntities() {
            Entities.RemoveAll(e => e.IsDestroyed());
        }
    }
}
