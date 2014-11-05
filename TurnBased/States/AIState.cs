using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public class AIState : State {
        // when done, set next state

        // draw disabled UI // Loader on UI
        public override void Act(string action) {

        }

        public override void Init() {
            // Set order of movement
        }

        public override void Update(float deltaTime) {
            Console.WriteLine("updating computer entities");
            // Move and shoot and shit

            this.StateManager.Pop();
            this.StateManager.Push("player");
        }

        public override DrawState GetDrawState() {
            return new EntityDrawState(new List<KeyValuePair<string,Microsoft.Xna.Framework.Vector2>>(), string.Empty);
        }
    }
}
