using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class Grid {
        private readonly string[,] grid;

        public Grid(string[,] grid) {
            this.grid = grid;
        }

        public void Clear(int x, int y) {
            grid[x, y] = string.Empty;
        }

        public string Tile(int x, int y) {
            return grid[x, y];
        }

        public List<Point> GetNearestPositions(int x, int y, int searchLength) {
            List<Point> nearestPositions = new List<Point>();
            for (int i = 1; i < searchLength + 1; i++) {
                throw new NotImplementedException();
            }
            return nearestPositions;
        }

        public List<Vector2> GetNearestPositions(int x, int y) {
            List<Vector2> nearestPositions = new List<Vector2>();
            int minX = Math.Max(x - 1, grid.GetLowerBound(1));
            int maxX = Math.Min(x + 1, grid.GetUpperBound(1));
            int minY = Math.Max(y - 1, grid.GetLowerBound(0));
            int maxY = Math.Min(y + 1, grid.GetUpperBound(0));
            for (int ix = minX; ix <= maxX; ix++) {
                for (int iy = minY; iy <= maxY; iy++) {
                    if (IsValid(ix, iy)) {
                        nearestPositions.Add(new Vector2(ix, iy));
                    }
                }
            }
            return nearestPositions;
        }

        public void ForeachTile(Action<int, int, string> action) {
            for (int y = 0; y < grid.GetUpperBound(0); y++) {
                for (int x = 0; x < grid.GetUpperBound(1); x++) {
                    action(x, y, grid[y, x]);
                }
            }
        }

        public void ForeachColumn(Action<int> action) {
            for (int x = 0; x < grid.GetUpperBound(1); x++) {
                action(x);
            }
        }

        public void ForeachRow(Action<int> action) {
            for (int y = 0; y < grid.GetUpperBound(0); y++) {
                action(y);
            }
        }

        public bool IsValid(int x, int y) {
            return x >= 0 && x <= grid.GetUpperBound(1) && y >= 0 && y <= grid.GetUpperBound(0) && !IsBlockedTile(x, y);
        }

        // TODO: fix this brutally shitty code
        private bool IsBlockedTile(int x, int y) {
            return Tile(x, y) != "bkg";
        }
    }
}
