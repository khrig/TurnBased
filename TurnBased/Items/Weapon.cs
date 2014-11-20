using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.Items {
    public class Weapon {
        public Weapon(int damage) {
            Damage = damage;
        }

        public int Damage { get; set; }
    }
}
