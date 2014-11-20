using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TurnBased.Actions {
    public class MoveAction : EntityAction {
        public MoveAction() {
            Name = "move";
        }

        public Vector2 Position { get; set; }
        public override void React(Entity entity) {
            entity.Move(Position);
        }
    }
}
