using System;
using EndlessTycoon.Core;
using EndlessTycoon.LevelGrids;
using EndlessTycoon.TaskSystems;
using UnityEngine;

public class CharacterTask : MonoBehaviour
{
    private enum State
    {
        WaitingForNextTask,
        ExecutingTask,
    }

    private MoveAction moveAction;

    private WaitingAction waitingAction;

    private TaskSystem<CustomerTask> taskSystem;

    private State state;
    private float waitingTimer;
    private float waitingTimerMax = .2f;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        waitingAction = GetComponent<WaitingAction>();
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

    public void Setup(TaskSystem<CustomerTask> taskSystem)
    {
        state = State.WaitingForNextTask;
        this.taskSystem = taskSystem;

    }

    private void RequestNextTask()
    {
        Debug.Log("RequestNextTask");
        CustomerTask task = taskSystem.RequestNextTask();
        if (task == null)
        {
            state = State.WaitingForNextTask;
        }
        else
        {
            state = State.ExecutingTask;

            if (task is CustomerTask.LeaveTown)
            {
                ExecuteTask_LeaveTown(task as CustomerTask.LeaveTown);
                return;
            }
            else if (task is CustomerTask.Order)
            {
                ExecuteTask_Order(task as CustomerTask.Order);
                return;
            }
        }
    }

    private void ExecuteTask_Order(CustomerTask.Order order)
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(order.stallSlotPos);
        moveAction.TakeAction(gridPosition, () =>
        {
            order.order(this.GetComponent<Character>());
            waitingAction.TakeAction(null, () =>
            {
                GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(LevelManager.Instance.endPos.position);
                moveAction.TakeAction(gridPosition, () =>
                {
                    Destroy(gameObject);
                });
            });
        });
    }

    private void ExecuteTask_LeaveTown(CustomerTask.LeaveTown moveToPostition)
    {
        Debug.Log("ExecuteTask_LeaveTown");

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(moveToPostition.targetPosition);
        moveAction.TakeAction(gridPosition, () =>
        {
            Destroy(gameObject);
            // state = State.WaitingForNextTask;
        });
    }
}
