using System;
using UnityEngine;

namespace EndlessTycoon.LevelGrids
{
    public class GridSystem<TGridObject>
    {
        private int width;
        private int height;
        private float cellSize;
        private TGridObject[,] gridObjectArray;

        public GridSystem(int width, int height, float cellSize, Func<GridSystem<TGridObject>, GridPosition, TGridObject> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridObjectArray = new TGridObject[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridPosition gridPosition = new GridPosition(x, y);
                    gridObjectArray[x, y] = createGridObject(this, gridPosition);
                }
            }
        }

        public Vector3 GetWorldPosition(GridPosition gridPostition)
        {
            Vector3 Pos = new Vector3(gridPostition.x, gridPostition.y) * cellSize;
            return Pos;
        }

        public GridPosition GetGridPosition(Vector3 worldposition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldposition.x / cellSize),
                Mathf.RoundToInt(worldposition.y / cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridPosition gridPostition = new GridPosition(x, y);

                    Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPostition), Quaternion.identity);
                    // GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                    // gridDebugObject.SetGridObject(GetGridObject(gridPostition));
                }
            }
        }

        public TGridObject GetGridObject(GridPosition gridPostition)
        {
            return gridObjectArray[gridPostition.x, gridPostition.y];
        }

        public bool IsValidGridPosition(GridPosition gridPostition)
        {
            return gridPostition.x >= 0 &&
                   gridPostition.y >= 0 &&
                   gridPostition.x < width &&
                   gridPostition.y < height;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public float GetCellSize()
        {
            return cellSize;
        }
    }
}


