using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class WorldModel {
        public Grid Background { get; set; }
		public List<Entity> Entities { get; set; }
		public Entity CurrentEntity { get; set; }

        public WorldModel(Grid background, List<Entity> entities, Entity currentEntity) {
            Background = background;
			Entities = entities;
			CurrentEntity = currentEntity;
        }
    }
}
