
using System;
using System.Collections.Generic;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class MoveAction : BaseAction
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float stoppingDistance = .1f;
        [SerializeField] private int maxMoveDistance = 2;

        private int currentPositionIndex;
        private List<Vector3> positionList = new List<Vector3>();

        public event Action OnStartMoving;
        public event Action OnStopMoving;

        private void Update()
        {
            if (!isActive) return;

            Vector3 targetPosition = positionList[currentPositionIndex];
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
            else
            {
                currentPositionIndex++;

                if (currentPositionIndex >= positionList.Count)
                {
                    OnStopMoving?.Invoke();
                    ActionComplete();
                }
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            List<GridPosition> pathGridPositionList = Pathfinding.Instance.FindPath(GetGridPosition(), gridPosition, out int pathLength);

            currentPositionIndex = 0;
            positionList = new List<Vector3>();

            foreach (GridPosition pathGridPosition in pathGridPositionList)
            {
                positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
            }

            OnStartMoving?.Invoke();
            ActionStart(onActionComplete);
        }

        public override string GetActionName()
        {
            return "Move";
        }

        private GridPosition GetGridPosition()
        {
            return LevelGrid.Instance.GetGridPosition(transform.position);
        }
    }
}