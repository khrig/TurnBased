using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TurnBased {
    // Goals:
    // Walk and shoot

    public static class StateDriven
    {
        internal static void RunGame()
        {
            StateManager stateManager = new StateManager();
            stateManager.Add("player", new PlayerState());
            stateManager.Add("computer", new ComputerState());

            stateManager.Push("player");

            while (true)
            {
                DetectUIInput();
                stateManager.Act(actions);
                stateManager.Update();
                Draw();
                Thread.Sleep(1000);
            }
        }

        private static void DetectUIInput()
        {
            // Could as well be get input from ui with the mouse clicked
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        // Space is pressed, which is like mouse is clicked on gun/spell
                        // equals set selected weapon
                        actions.Enqueue("weapon");
                        break;
                    case ConsoleKey.S:
                        // which is like target is clicked on
                        // equals shoot with selected weapon on target (if a weapon is selected, if a player only has one weapon/spell, fire that)
                        actions.Enqueue("shoot");
                        break;
                    case ConsoleKey.G:
                        // which is like move to target if possible
                        actions.Enqueue("move");
                        break;
                    case ConsoleKey.T:
                        actions.Enqueue("changeCharacter");
                        break;
                    default:
                        break;
                }
            }
        }

        private static Queue<string> actions = new Queue<string>();

        //-----------------------------
        //
        // WARNING - Only handles current state, no fallthrough
        //
        //-----------------------------
        private class StateManager {
            private Stack<State> states = new Stack<State>();
            private Dictionary<string, State> availableStates = new Dictionary<string, State>();

            internal void Act(Queue<string> actions) {
                while (actions.Count != 0) {
                    string action = actions.Dequeue();
                    if(action == "pause")
                        Push("pause");
                    else if(action == "menu")
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

            internal void Update() {
                states.Peek().Update();
            }
        }

        private class PlayerState : State {
            // when done, set next state
            // draw UI depending on the current players actions

            public override void Init() {
                availableEntities = new List<Entity>() {
                    new Entity("Rambo"),
                    new Entity("Terminator")
                };

                foreach (Entity entity in availableEntities) {
                    entity.Init();
                    entityTurnOrder.Enqueue(entity);
                }
            }

            private List<Entity> availableEntities = new List<Entity>();
            private Queue<Entity> entityTurnOrder = new Queue<Entity>();

            public override void Act(string action) {
                Console.WriteLine("Acting");
                if (action == "changeCharacter") {
                    PutCharacterFirstInQueue();
                    return;
                }

                Entity current = entityTurnOrder.Peek();
                if (action == "weapon")
                    current.SetWeapon(1);
                if (action == "shoot")
                    current.Shoot("x,y");
                if (action == "skip" || current.IsEnergyDepleted())
                    entityTurnOrder.Dequeue();

                if (entityTurnOrder.Count == 0) {
                    // We are done with this turn
                    // Set next state to computer state
                    this.StateManager.Pop();
                    this.StateManager.Push("computer");
                }
            }

            private void PutCharacterFirstInQueue() {
                // Change entity turn order
                // Should find the character and then put it first in queue
                Entity e = entityTurnOrder.Dequeue();
                entityTurnOrder.Enqueue(e);
            }

            public override void Update() {
                // unkown
            }
        }

        private class Entity {
            private int selectedWeapon = 0;
            private int energy = 0;
            private string name = "";

            public Entity(string name) {
                this.name = name;
            }

            internal void Init() {
                energy = 100; // or whatever this units energy is
            }

            internal void SetWeapon(int p) {
                // Equipment.SetWeapon()
                Console.WriteLine(name + " selected weapon");
                selectedWeapon = 1;
            }

            internal void Shoot(string p) {
                Console.WriteLine(name + " shooting");
                energy -= 100; // energy -= Equipment.SelectedWeaponEnergyCost()

                // eventQueue.Push("shoot", "targetCoords x,y");
            }

            internal bool IsEnergyDepleted() {
                return energy <= 0;
            }
        }

        private class ComputerState : State {
            // when done, set next state

            // draw disabled UI // Loader on UI
            public override void Act(string action) {
                
            }

            public override void Init() {
                // Set order of movement
            }

            public override void Update() {
                Console.WriteLine("updating computer entities");
                // Move and shoot and shit

                this.StateManager.Pop();
                this.StateManager.Push("player");
            }
        }
        /*
        private class InventoryState : State {
            // Show Menu only
        }

        private class MenuState : State {
            // Show Menu only
        }

        private class PauseState : State {
            // Pause updates 
            // Show menu
        }*/

        private abstract class State {
            public abstract void Init();
            public abstract void Act(string action);
            public abstract void Update();
            public StateManager StateManager { get; set; }
        }

        private static void SendUiInputToState()
        {
            // Can be menu button is pressed
            // or pause button is pressed

            // Needs to set the action on the current selected unit
            // e.g. set current weapon
            // shoot at target
            // move to target
        }

        private static void Draw()
        {
            // character info for UI
            // - current weapon/spell
            // - current HP
            // - current action points
            // Other SELECTABLE characters (like characters that has skipped or has no energy left is not selectable)
            
            // for all characters 
            // - current HP (maybe?)
            // - sprite
            // - position

            // Also needs to be able to draw UI for other things
            // - paused
            // - menu
            // - inventory?
        }
    }
}
