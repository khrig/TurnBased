using System;
using TurnBased.Actions;

namespace TurnBased.States {
    public abstract class State {
        protected WorldModel WorldModel { get; set; }
        public StateManager StateManager { get; set; }
		
		protected State() {}
		protected State(WorldModel model) {
			WorldModel = model;
		}
	
        public abstract void Init();
        public abstract void Act(EntityAction action);
        public abstract void Update(float deltaTime);
    }
}
