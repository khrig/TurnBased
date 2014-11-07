using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TurnBased.Interfaces {
    public interface IWorldViewSettings {
        int TileSizeX { get; set; }
        int TileSizeY { get; set; }
        int TileCenter { get; set; }
        int WindowWidth { get; set; }
        int WindowHeight { get; set; }
        Rectangle GridBounds  { get; set; }
    }
}
