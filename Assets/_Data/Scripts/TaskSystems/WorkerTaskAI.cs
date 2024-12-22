using EndlessTycoon.Core;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class WorkerTaskAI : MonoBehaviour
    {
        private enum State
        {
            WaitingForNextTask,
            ExecutingTask,
        }

        private MoveAction moveAction;
        private TaskSystem<TaskHandler.Task> taskSystem;

        private State state;
        private float waitingTimer;
        private float waitingTimerMax = .2f;

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
        }

        private void Update()
        {
            HandleState();
        }

        private void HandleState()
        {
            if (taskSystem == null) return;

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

        public void Setup(TaskSystem<TaskHandler.Task> taskSystem)
        {
            state = State.WaitingForNextTask;
            this.taskSystem = taskSystem;
        }

        private void RequestNextTask()
        {
            Debug.Log("RequestNextTask");
            TaskHandler.Task task = taskSystem.RequestNextTask();
            if (task == null)
            {
                state = State.WaitingForNextTask;
            }
            else
            {
                state = State.ExecutingTask;

                if (task is TaskHandler.Task.MoveToPostition)
                {
                    ExecuteTask_MoveToPostition(task as TaskHandler.Task.MoveToPostition);
                    return;
                }

                if (task is TaskHandler.Task.Victory)
                {
                    ExecuteTask_Victory(task as TaskHandler.Task.Victory);
                    return;
                }

                if (task is TaskHandler.Task.Gathering)
                {
                    ExecuteTask_Gathering(task as TaskHandler.Task.Gathering);
                    return;
                }

                if (task is TaskHandler.Task.TakeCubeToCubeSlot)
                {
                    ExecuteTask_TakeCubeToCubeSlot(task as TaskHandler.Task.TakeCubeToCubeSlot);
                    return;
                }
            }
        }

        private void ExecuteTask_TakeCubeToCubeSlot(TaskHandler.Task.TakeCubeToCubeSlot takeCubeToCubeSlot)
        {
            Debug.Log("ExecuteTask_TakeCubeToCubeSlot");

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(takeCubeToCubeSlot.cubePosition);


            moveAction.TakeAction(gridPosition, () =>
            {
                takeCubeToCubeSlot.carryCube(this);
                GridPosition gridPositionSlot = LevelGrid.Instance.GetGridPosition(takeCubeToCubeSlot.cubeSlotPosition);
                moveAction.TakeAction(gridPositionSlot, () =>
                {
                    takeCubeToCubeSlot.dropCube();
                    state = State.WaitingForNextTask;
                });
            });
        }

        private void ExecuteTask_Victory(TaskHandler.Task.Victory victoryTask)
        {
            Debug.Log("ExecuteTask_Victory");

            // victoryAction.TakeAction(null, () =>
            // {
            //     state = State.WaitingForNextTask;
            // });
        }

        private void ExecuteTask_MoveToPostition(TaskHandler.Task.MoveToPostition moveToPostitionTask)
        {
            Debug.Log("ExecuteTask_MoveToPostition");

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(moveToPostitionTask.targetPosition);
            moveAction.TakeAction(gridPosition, () =>
            {
                state = State.WaitingForNextTask;
            });
        }

        private void ExecuteTask_Gathering(TaskHandler.Task.Gathering gatherTask)
        {
            Debug.Log("ExecuteTask_Gathering");

            // GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(gatherTask.targetPosition);
            // moveAction.TakeAction(gridPosition, () =>
            // {
            //     gatherAction.TakeAction(null, () =>
            //     {
            //         gatherTask.gather();
            //         state = State.WaitingForNextTask;
            //     });
            // });
        }
    }
}