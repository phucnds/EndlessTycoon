using System;
using System.Collections.Generic;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected bool isActive = false;
        protected Action onActionComplete;


        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        public void ActionStart(Action onActionComplete)
        {
            isActive = true;
            this.onActionComplete = onActionComplete;
        }

        public void ActionComplete()
        {
            isActive = false;
            onActionComplete();
        }

        public abstract string GetActionName();

        public List<GridPosition> GetValidActionGridPositionList()
        {
            return null;
        }
    }
}
