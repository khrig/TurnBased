using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased.Actions {
    public class ShootAction : EntityAction {
        public ShootAction() {
            Name = "shoot";
        }

        public Entity TargetEntity { get; set; }
        public override void React(Entity entity) {
            entity.Shoot(TargetEntity);
        }
    }
}
