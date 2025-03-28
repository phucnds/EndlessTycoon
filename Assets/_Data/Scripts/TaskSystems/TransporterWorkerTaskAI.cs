using EndlessTycoon.Core;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class TransporterWorkerTaskAI : MonoBehaviour
    {
        private enum State
        {
            WaitingForNextTask,
            ExecutingTask,
        }

        private MoveAction moveAction;
        
        private TaskSystem<TaskHandler.TransporterTask> taskSystem;

        private State state;
        private float waitingTimer;
        private float waitingTimerMax = .2f;

        private void Start()
        {
            moveAction = GetComponent<MoveAction>();

        }

        private void Update()
        {
            switch (state)
            {
                case State.WaitingForNextTask:
                    waitingTimer -= Time.deltaTime;
                    if (waitingTimer <= 0)
                    {
                        waitingTimer = waitingTimerMax;
                        RequestNextTask();
                    }
                    break;

                case State.ExecutingTask:
                    break;
            }
        }

        public void Setup(TaskSystem<TaskHandler.TransporterTask> taskSystem)
        {
            state = State.WaitingForNextTask;
            this.taskSystem = taskSystem;
        }

        private void RequestNextTask()
        {
            Debug.Log("RequestNextTask");
            TaskHandler.TransporterTask task = taskSystem.RequestNextTask();
            if (task == null)
            {
                state = State.WaitingForNextTask;
            }
            else
            {
                state = State.ExecutingTask;

                if (task is TaskHandler.TransporterTask.TakeCubeFromSlotToPosition)
                {
                    ExecuteTask_TakeCubeFromSlotToPosition(task as TaskHandler.TransporterTask.TakeCubeFromSlotToPosition);
                    return;
                }

            }
        }

        private void ExecuteTask_TakeCubeFromSlotToPosition(TaskHandler.TransporterTask.TakeCubeFromSlotToPosition takeCubeFromCubeSlot)
        {
            Debug.Log("ExecuteTask_TakeCubeFromSlotToPosition");

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(takeCubeFromCubeSlot.cubeSlotPosition);
            moveAction.TakeAction(gridPosition, () =>
            {
                takeCubeFromCubeSlot.carryCube(this);
                GridPosition gridPositionSlot = LevelGrid.Instance.GetGridPosition(takeCubeFromCubeSlot.targetPosition);
                moveAction.TakeAction(gridPositionSlot, () =>
                {
                    takeCubeFromCubeSlot.dropCube();
                    state = State.WaitingForNextTask;
                });
            });
        }
    }
}
