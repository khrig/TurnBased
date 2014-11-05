using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public abstract class State {
        public abstract void Init();
        public abstract void Act(string action);
        public abstract void Update(float deltaTime);
        public StateManager StateManager { get; set; }
        public abstract DrawState GetDrawState();
    }
}
