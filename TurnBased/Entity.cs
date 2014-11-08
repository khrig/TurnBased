using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class Entity {
        private int selectedWeapon = 0;
        private int energy = 0;
        public string Name { get; private set; }
        public Vector2 Position { get; set; }
        public bool PlayerControlled { get; set; }

        public Entity(string name, Vector2 position) {
            this.Name = name;
            this.Position = position;
        }

        public void Init() {
            energy = 100; // or whatever this units energy is
        }

        public void SetWeapon(int p) {
            // Equipment.SetWeapon()
            Console.WriteLine(Name + " selected weapon");
            selectedWeapon = 1;
        }

        public void Shoot(string p) {
            Console.WriteLine(Name + " shooting");
            energy -= 100; // energy -= Equipment.SelectedWeaponEnergyCost()

            // eventQueue.Push("shoot", "targetCoords x,y");
        }

        public void Move(Vector2 vector2) {
            energy -= 100;
            Position = vector2;
        }

        public bool IsEnergyDepleted() {
            return energy <= 0;
        }
    }
}
