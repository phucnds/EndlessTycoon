using UnityEngine;

namespace EndlessTycoon.LevelGrids
{
    public class LevelGrid : Singleton<LevelGrid>
    {
        [Header("Setting")]
        [SerializeField] private int width = 57;
        [SerializeField] private int height = 50;
        [SerializeField] private float cellSize = 3f;

        private GridSystem<GridObject> gridSystem;

        protected override void Awake()
        {
            base.Awake();

            gridSystem = new GridSystem<GridObject>(width, height, cellSize, (GridSystem<GridObject> g, GridPosition gridPostition) => new GridObject(g, gridPostition));
        }

        private void Start()
        {
            Pathfinding.Instance.Setup(width, height, cellSize);
        }

        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

        public bool IsValidGridPosition(GridPosition gridPostition) => gridSystem.IsValidGridPosition(gridPostition);

        public int GetWidth() => gridSystem.GetWidth();

        public int GetHeight() => gridSystem.GetHeight();

        public float CellSize() => gridSystem.GetCellSize();

    }
}


