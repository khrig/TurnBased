using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.States;

namespace TurnBased.Actions {
    public abstract class EntityAction {
        public string Name { get; protected set; }
        public abstract void React(Entity entity);
    }
}
