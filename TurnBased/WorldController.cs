using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TurnBased.Actions;
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

            model.RemoveDestroyedEntities();
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
                else if (model.IsEnemyTargeted(action)) {
                    stateManager.Act(CreateShootAction(action));
                }
                else if(model.Valid(action)) {
                    stateManager.Act(CreateMoveAction(action));
                }
            }
        }

        private EntityAction CreateMoveAction(string action) {
            return new MoveAction() { Position = model.GetPosition(action) };
        }

        private EntityAction CreateShootAction(string action) {
            return new ShootAction() { TargetEntity = model.GetEntityAt(action) };
        }
        
        private string[,] GetBackground() {
            int height = 16, width = 20;
		string[,] grid = new string[height,width];
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++) {
				grid[i,j] = "bkg";
			}
		}
        }
    }
}
