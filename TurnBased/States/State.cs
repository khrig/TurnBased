using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public abstract class State {
        protected WorldModel WorldModel { get; set; }
        public StateManager StateManager { get; set; }
		
		protected State() {}
		protected State(WorldModel model) {
			WorldModel = model;
		}
	
        public abstract void Init();
        public abstract void Act(string action);
        public abstract void Update(float deltaTime);
    }
}
