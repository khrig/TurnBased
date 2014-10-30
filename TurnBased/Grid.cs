using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class Grid {
        public const int EMPTY_TILE = 0;
        public const int WALL_TILE = 1;

        private readonly string[,] grid;
        private readonly Random random = new Random();

        public Grid(string[,] grid) {
            this.grid = grid;
        }

        public void Clear(int x, int y) {
            grid[x, y] = string.Empty;
        }

        public string Tile(int x, int y) {
            return grid[x, y];
        }

        public void ForeachTile(Action<int, int, string> action) {
            for (int y = 0; y <= grid.GetUpperBound(1); y++) {
                for (int x = 0; x <= grid.GetUpperBound(0); x++) {
                    action(x, y, grid[x, y]);
                }
            }
        }
    }
}
