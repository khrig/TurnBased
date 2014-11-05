using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public abstract class State {
		protected WorldModel worldModel;
		
		public State() {}
		public State(WorldModel model) {
			worldModel = model;
		}
	
        public abstract void Init();
        public abstract void Act(string action);
        public abstract void Update(float deltaTime);
        public StateManager StateManager { get; set; }
    }
}
