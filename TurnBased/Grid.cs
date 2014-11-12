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

        public List<Vector2> GetNearestPositions(int x, int y, int length, int maxCost) {
            List<Vector2> nearestPositions = new List<Vector2>();
            int startPosX = Math.Max(x - length, grid.GetLowerBound(1));
            int endPosX = Math.Min(x + length, grid.GetUpperBound(1));
            int startPosY = Math.Max(y - length, grid.GetLowerBound(0));
            int endPosY = Math.Min(y + length, grid.GetUpperBound(0));
            for (int rowNum=startPosX; rowNum<=endPosX; rowNum++) {
                for (int colNum=startPosY; colNum<=endPosY; colNum++) {
                    if(EuclideanDistance(x, y, rowNum, colNum) <= maxCost && IsValid(rowNum, colNum))
                        nearestPositions.Add(new Vector2(rowNum, colNum));
                }
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

        private int EuclideanDistance(int x, int y, int rowNum, int colNum) {
            double xd = x - rowNum;
            double yd = y - colNum;
            double sqrt = Math.Sqrt((xd*xd) + (yd*yd));
            return (int)(sqrt * 10);
        }
    }
}
