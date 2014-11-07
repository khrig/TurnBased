using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurnBased.Interfaces;

namespace TurnBased {
    public class WorldViewSettings : IWorldViewSettings {
        public int TileSizeX { get; set; }
        public int TileSizeY { get; set; }
        public int TileCenter { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public Rectangle GridBounds { get; set; }
    }
}
