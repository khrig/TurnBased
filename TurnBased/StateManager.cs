using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.States;

namespace TurnBased {
    public class StateManager {
        private readonly Stack<State> states = new Stack<State>();
        private readonly Dictionary<string, State> availableStates = new Dictionary<string, State>();

        public void Act(string action) {
            states.Peek().Act(action);
        }

        public void Add(string stateId, State state) {
            state.StateManager = this;
            availableStates.Add(stateId, state);
        }

        public void Push(string stateId) {
            State state = availableStates[stateId];
            state.Init();
            states.Push(state);
        }

        public void Pop() {
            states.Pop();
        }

        public void Update(float deltaTime) {
            states.Peek().Update(deltaTime);
        }

        public bool IsEmpty() {
            return states.Count == 0;
        }
    }
}
