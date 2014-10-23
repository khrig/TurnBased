using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class StatesModel {
        public Stack<DrawState> States { get; set; }

        public StatesModel() {
            States = new Stack<DrawState>();
        }
    }
}
