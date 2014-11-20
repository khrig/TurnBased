using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.Actions;

namespace TurnBased.States {
    public class PlayerState : State {
        private readonly List<Entity> availableEntities = new List<Entity>();
        private readonly Queue<Entity> entityTurnOrder = new Queue<Entity>();
		
        public PlayerState(WorldModel model) : base(model) {
            availableEntities = new List<Entity>() {
                new Entity("rambo", new Vector2(1, 1)) { PlayerControlled = true },
                new Entity("terminator", new Vector2(0, 1)) { PlayerControlled = true }
            };

            WorldModel.Entities.AddRange(availableEntities);
        }

        public override void Init() {
            foreach (Entity entity in availableEntities) {
                entity.Init();
                entityTurnOrder.Enqueue(entity);
            }
            SetCurrentEntity();
        }

        public override void Act(EntityAction action) {
            if (action.Name == "changeCharacter") {
                PutNextCharacterFirstInQueue();                
            }
			else {
				Entity currentEntity = entityTurnOrder.Peek();
                action.React(currentEntity);

                /*
				if (action.StartsWith("move"))
					current.Move(GetCharacterMove(action));
				if (action == "weapon")
					current.SetWeapon(1);
				if (action.StartsWith("shoot"))
					current.Shoot("x,y");
				if (action == "skip" || current.IsEnergyDepleted()) {
					entityTurnOrder.Dequeue();
				}
                 * */
                if (currentEntity.IsEnergyDepleted())
                    entityTurnOrder.Dequeue();
				if (entityTurnOrder.Count == 0) {
					// We are done with this turn
					// Set next state to computer state
					this.StateManager.Pop();
					this.StateManager.Push("computer");
				}
			}
			
			SetCurrentEntity();
        }

        private Vector2 GetCharacterMove(string action) {
            string[] arr = action.Split(';')[1].Split(',');
            return new Vector2(int.Parse(arr[0]), int.Parse(arr[1]));
        }

        private void PutNextCharacterFirstInQueue() {
            // Change entity turn order
            // Should find the character and then put it first in queue
            Entity e = entityTurnOrder.Dequeue();
            entityTurnOrder.Enqueue(e);
        }
		
		private void SetCurrentEntity() {
			if(entityTurnOrder.Count > 0)
                WorldModel.CurrentEntity = entityTurnOrder.Peek();
			else
                WorldModel.CurrentEntity = null;
		}

        public override void Update(float deltaTime) {
            // unkown
        }
	}
}
