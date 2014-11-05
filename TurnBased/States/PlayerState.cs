using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.States {
    public class PlayerState : State {
        private List<Entity> availableEntities = new List<Entity>();
        private Queue<Entity> entityTurnOrder = new Queue<Entity>();
		
        public PlayerState(WorldModel model) : base(model) {
            availableEntities = new List<Entity>() {
                    new Entity("Rambo", new Vector2(50, 50)),
                    new Entity("Terminator", new Vector2(100, 100))
                };
        }

        public override void Init() {
            foreach (Entity entity in availableEntities) {
                entity.Init();
                entityTurnOrder.Enqueue(entity);
				model.Entities.Add(entity);
            }
        }

        public override void Act(string action) {
            Console.WriteLine("Acting");
            if (action == "changeCharacter") {
                PutCharacterFirstInQueue();
                return;
            }

            Entity current = entityTurnOrder.Peek();
            if (action.StartsWith("move"))
                current.Move(GetCharacterMove(action));
            if (action == "weapon")
                current.SetWeapon(1);
            if (action == "shoot")
                current.Shoot("x,y");
            if (action == "skip" || current.IsEnergyDepleted()) {
                entityTurnOrder.Dequeue();
				SetCurrentEntity();
			}
            if (entityTurnOrder.Count == 0) {
                // We are done with this turn
                // Set next state to computer state
				model.CurrentEntity = null;
                this.StateManager.Pop();
                this.StateManager.Push("computer");
            }
        }

        private Vector2 GetCharacterMove(string action) {
            string[] arr = action.Split(';')[1].Split(',');
            return Vector2(int.Parse(arr[0]), int.Parse(arr[1]));
        }

        private void PutCharacterFirstInQueue() {
            // Change entity turn order
            // Should find the character and then put it first in queue
            Entity e = entityTurnOrder.Dequeue();
            entityTurnOrder.Enqueue(e);
			SetCurrentEntity();
        }
		
		private void SetCurrentEntity() {
			if(entityTurnOrder.Count > 0)
				model.CurrentEntity = entityTurnOrder.Peek();
		}

        public override void Update(float deltaTime) {
            // unkown
        }
	}
}
