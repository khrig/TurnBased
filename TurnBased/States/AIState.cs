using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public class AiState : State {
        private readonly List<Entity> availableEntities = new List<Entity>();

		public AiState(WorldModel model) : base(model) {
            availableEntities = new List<Entity>() {
                new Entity("robot", new Vector2(15, 14)),
                new Entity("robot", new Vector2(15, 15))
            };

            WorldModel.Entities.AddRange(availableEntities);
		}
	
        public override void Act(string action) {

        }

        public override void Init() {
            // Set order of movement
        }

        public override void Update(float deltaTime) {
            Console.WriteLine("updating computer entities");
            // Move and shoot and shit

            // when done:
            this.StateManager.Pop();
            this.StateManager.Push("player");
        }
    }
}
