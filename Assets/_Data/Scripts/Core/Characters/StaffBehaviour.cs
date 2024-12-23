using System;
using EndlessTycoon.Core;
using EndlessTycoon.LevelGrids;
using EndlessTycoon.TaskSystems;
using UnityEngine;

public class StaffBehaviour : MonoBehaviour
{
    private enum State
    {
        WaitingForNextTask,
        ExecutingTask,
    }

    private MoveAction moveAction;

    private TaskSystem<StallTask> taskSystem;

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

    public void Setup(TaskSystem<StallTask> taskSystem)
    {
        state = State.WaitingForNextTask;
        this.taskSystem = taskSystem;

    }

    private void RequestNextTask()
    {
        Debug.Log("RequestNextTask");
        StallTask task = taskSystem.RequestNextTask();
        if (task == null)
        {
            state = State.WaitingForNextTask;
        }
        else
        {
            state = State.ExecutingTask;

            if (task is StallTask.TakeOrder)
            {
                ExecuteTask_Order(task as StallTask.TakeOrder);
                return;
            }
            else if (task is StallTask.Deliver)
            {
                ExecuteTask_Deliver(task as StallTask.Deliver);
                return;
            }
        }
    }

    private void ExecuteTask_Order(StallTask.TakeOrder takeOrderTask)
    {
        Debug.Log("ExecuteTask_Order");

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(takeOrderTask.stallSlotPos);
        moveAction.TakeAction(gridPosition, () =>
        {
            GetComponent<DisplayProgress>().StartProgression(1f, () =>
            {
                takeOrderTask.takeOrder(this.GetComponent<Character>());
                Debug.Log("deliver");
                state = State.WaitingForNextTask;
            });
        });
    }

    private void ExecuteTask_Deliver(StallTask.Deliver deliverTask)
    {
        Debug.Log("ExecuteTask_Deliver");

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(deliverTask.counterPos);
        moveAction.TakeAction(gridPosition, () =>
        {
            deliverTask.reachedCounterSlot();
            GetComponent<CharacterAnimator>().StartDoing();
            GetComponent<DisplayProgress>().StartProgression(3f, () =>
            {
                GetComponent<CharacterAnimator>().StoptDoing();
                deliverTask.doneProduce();
                GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(deliverTask.stallSlotPos);
                moveAction.TakeAction(gridPosition, () =>
                {
                    deliverTask.takePayment();
                    state = State.WaitingForNextTask;
                });

            });

        });
    }
}
