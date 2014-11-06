using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public class AiState : State {
		public AiState(WorldModel model) : base(model) {
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
