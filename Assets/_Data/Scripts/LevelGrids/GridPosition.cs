using System;
using UnityEngine;

namespace EndlessTycoon.LevelGrids
{
    public class GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition gridPostition &&
                   x == gridPostition.x &&
                   y == gridPostition.y;
        }

        public bool Equals(GridPosition other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return !(a == b);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x + b.x, a.y + b.y);
        }

        public static GridPosition operator -(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x - b.x, a.y - b.y);
        }
    }
}