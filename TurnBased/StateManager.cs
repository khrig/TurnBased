using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.States;

namespace TurnBased {
	// Move to new file
	public class WorldController {
		private StateManager stateManager;
		private WorldModel model;
        private Queue<string> actions = new Queue<string>();
		
		public WorldController() {
			model = new WorldModel(new Grid(GetBackground()), new List<Entity>(), null);
		
			stateManager = new StateManager();
			stateManager.Add("player", new PlayerState(model));
            stateManager.Add("computer", new AIState(model));
            stateManager.Push("player");
		}
		
		public void Update(GameTime gameTime) {
			HandleActions();
		
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            stateManager.Update(deltaTime);
		}
		
		public void AddAction(string action) {
			actions.Enqueue(action);
		}
		
		public bool End() {
			return stateManager.IsEmpty();
		}
		
		public WorldModel GetWorldModel() {
			throw new NotImplementedException("world model needs to be updated in the states or it needs to read the states");
			return model;
		}
		
		private void HandleActions() {
			while (actions.Count != 0) {
                string action = actions.Dequeue();
                if (action == "pause")
                    stateManager.Push("pause");
                else if (action == "menu")
                    stateManager.Push("menu");
                else if(model.Valid(action)) {
					stateManager.Act(actions);
				}
			}
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


    public class StateManager {
        private Stack<State> states = new Stack<State>();
        private Dictionary<string, State> availableStates = new Dictionary<string, State>();

        internal void Act(string action) {
			states.Peek().Act(action);
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
    }
}
