using System;
using EndlessTycoon.Core;
using EndlessTycoon.TaskSystems;
using UnityEngine;

public class CustomerTask : BaseTask
{
    public class MoveToPostition : CustomerTask
    {
        public Vector3 targetPosition;
    }

    public class Order : CustomerTask
    {
        public Vector3 stallSlotPos;
        public Action<Character> order;
    }

    public class LeaveTown : CustomerTask
    {
        public Vector3 targetPosition;
    }

}
