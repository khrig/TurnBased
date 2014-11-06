using System.Collections.Generic;
using TurnBased.States;

namespace TurnBased {
    public class WorldController {
        private readonly StateManager stateManager;
        private readonly WorldModel model;
        private readonly Queue<string> actions = new Queue<string>();
		
        public WorldController() {
            model = new WorldModel(new Grid(GetBackground()));
		
            stateManager = new StateManager();
            stateManager.Add("player", new PlayerState(model));
            stateManager.Add("computer", new AiState(model));
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
                    stateManager.Act(action);
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
}