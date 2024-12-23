using System;
using EndlessTycoon.Core;
using EndlessTycoon.TaskSystems;
using UnityEngine;

public class StallTask : BaseTask
{
    public class MoveToPostition : StallTask
    {
        public Vector3 targetPosition;
    }

    public class TakeOrder : StallTask
    {
        public Vector3 stallSlotPos;
        public Action<Character> takeOrder;
    }

    public class Deliver : StallTask
    {
        public Vector3 counterPos;
        public Vector3 stallSlotPos;
        public Action reachedCounterSlot;
        public Action doneProduce;
        public Action takePayment;
    }
}