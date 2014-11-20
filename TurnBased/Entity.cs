using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.Items;

namespace TurnBased {
    public class Entity {
        private int selectedWeapon = 0;
        private int energy = 0;
        private int health = 100;
        public string Name { get; private set; }
        public Vector2 Position { get; set; }
        public bool PlayerControlled { get; set; }

        private Weapon currentWeapon { get; set; }

        public int ActionPoints {
            get {
                return 4;
            }
        }

        public int MoveLength { get { return ActionPoints * 10; } }

        public Entity(string name, Vector2 position) {
            this.Name = name;
            this.Position = position;
            currentWeapon = new Weapon(100);
        }

        public void Init() {
            energy = 100; // or whatever this units energy is
        }

        public void SetWeapon(int p) {
            // Equipment.SetWeapon()
            Console.WriteLine(Name + " selected weapon");
            selectedWeapon = 1;
        }

        public void Shoot(Entity targetEntity) {
            Console.WriteLine(Name + " shooting");
            energy -= 100; // energy -= Equipment.SelectedWeaponEnergyCost()
            targetEntity.DecraseHealth(currentWeapon.Damage);
            // eventQueue.Push("shoot", "targetCoords x,y");
        }

        public void Move(Vector2 vector2) {
            energy -= 100;
            Position = vector2;
        }

        public bool IsEnergyDepleted() {
            return energy <= 0;
        }

        private void DecraseHealth(int damage) {
            health -= damage;
        }

        public bool IsDestroyed() {
            return health <= 0;
        }
    }
}
