using System.Collections.Generic;
using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private Transform counterSlotParent;
        [SerializeField] private CounterStat counterStat;

        private int level;
        private float speed = 5f;

        private List<CounterSlot> listCounterSlot = new List<CounterSlot>();

        private void Start()
        {
            Setup();
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetIncome()
        {
            return counterStat.Data[level].Income;
        }
        public int GetCost()
        {
            return counterStat.Data[level].Cost;
        }

        public void UpgradeSpeed()
        {
            speed = 2.5f;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void LevelUp()
        {
            level++;
            level = Mathf.Clamp(level, 0, 24);

            if (level == 10)
            {
                AddSlot();
            }
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