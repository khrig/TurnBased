using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.States;

namespace TurnBased {
    public class StateManager {
        private Stack<State> states = new Stack<State>();
        private Dictionary<string, State> availableStates = new Dictionary<string, State>();

        internal void Act(Queue<string> actions) {
            while (actions.Count != 0) {
                string action = actions.Dequeue();
                if (action == "pause")
                    Push("pause");
                else if (action == "menu")
                    Push("menu");
                else
                    states.Peek().Act(action);
            }
        }

        internal void Add(string stateId, State state) {
            state.StateManager = this;
            availableStates.Add(stateId, state);
        }

        internal void Push(string stateId) {
            State state = availableStates[stateId];
            state.Init();
            states.Push(state);
        }

        internal void Pop() {
            states.Pop();
        }

        internal void Update(float deltaTime) {
            states.Peek().Update(deltaTime);
        }

        internal WorldModel GetModel() {
            WorldModel worldModel = new WorldModel(new Grid(GetBackground()));
            foreach (State state in states) {
                worldModel.States.Add(state.GetDrawState());
            }
            return worldModel;
        }

        private string[,] GetBackground() {
            return new string[,] {
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" },
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" },
                { "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg", "bkg" }, 
            };
        }
    }
}
