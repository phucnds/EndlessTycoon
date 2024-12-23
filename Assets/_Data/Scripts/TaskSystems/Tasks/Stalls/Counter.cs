using System.Collections.Generic;
using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private Transform counterSlotParent;

        private List<CounterSlot> listCounterSlot = new List<CounterSlot>();

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (Transform tr in counterSlotParent)
            {
                CounterSlot slot = tr.GetComponent<CounterSlot>();
                listCounterSlot.Add(slot);
            }
        }

        public CounterSlot GetEmptySlot()
        {
            foreach (CounterSlot slot in listCounterSlot)
            {
                if (slot.StaffPosition.IsEmpty())
                {
                    return slot;
                }
            }

            return null;
        }

        public List<CounterSlot> GetList()
        {
            List<CounterSlot> lst = new List<CounterSlot>();

            foreach (CounterSlot slot in listCounterSlot)
            {
                if (slot.gameObject.activeSelf)
                {
                    lst.Add(slot);
                }
            }
            return lst;
        }

        public void AddSlot()
        {
            foreach (CounterSlot slot in listCounterSlot)
            {
                if (!slot.gameObject.activeSelf)
                {
                    slot.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

}