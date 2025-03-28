using UnityEngine;

namespace EndlessTycoon.LevelGrids
{

    public class PathNode
    {
        private int gCost;
        private int hCost;
        private int fCost;

        private GridPosition gridPostition;
        private PathNode cameFromPathNode;

        private bool isWalkable = true;


        public PathNode(GridPosition gridPostition)
        {
            this.gridPostition = gridPostition;
        }

        public override string ToString()
        {
            string unitString = "";
            return gridPostition.ToString() + "\n" + unitString;
        }



        public int GetGCost()
        {
            return gCost;
        }

        public int GetHCost()
        {
            return hCost;
        }

        public int GetFCost()
        {
            return fCost;
        }

        public void SetGCost(int gCost)
        {
            this.gCost = gCost;
        }

        public void SetHCost(int hCost)
        {
            this.hCost = hCost;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public void ResetCameFromPathNode()
        {
            cameFromPathNode = null;
        }

        public void SetCameFromPathNode(PathNode pathNode)
        {
            cameFromPathNode = pathNode;
        }

        public PathNode GetCameFromPathNode()
        {
            return cameFromPathNode;
        }

        public GridPosition GetGridPosition()
        {
            return gridPostition;
        }

        public bool IsWalkable()
        {
            return isWalkable;
        }

        public void SetIsWalkable(bool isWalkable)
        {
            this.isWalkable = isWalkable;
        }
    }

}

