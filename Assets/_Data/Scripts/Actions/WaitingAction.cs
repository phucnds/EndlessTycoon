using System;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class WaitingAction : BaseAction
    {
        public bool isWaiting;

        private void Update()
        {
            if (!isActive) return;

            if (!isWaiting)
            {
                ActionComplete();
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            isWaiting = true;
            ActionStart(onActionComplete);
        }

        public override string GetActionName()
        {
            return "Waiting";
        }
    }
}


