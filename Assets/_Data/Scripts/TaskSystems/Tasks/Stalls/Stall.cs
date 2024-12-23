using System.Collections.Generic;
using EndlessTycoon.Core;
using EndlessTycoon.LevelGrids;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class Stall : MonoBehaviour
    {
        [SerializeField] private Transform staffParent;
        [SerializeField] private Transform slotParent;
        [SerializeField] private Counter counter;

        private List<StallSlot> listStallSlot = new List<StallSlot>();

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (Transform tr in slotParent)
            {
                StallSlot stallSlot = tr.GetComponent<StallSlot>();
                listStallSlot.Add(stallSlot);
                stallSlot.SetStall(this);
            }
        }

        public List<StallSlot> GetListStallSlot()
        {
            return listStallSlot;
        }

        public StallSlot GetRandomStallSlot()
        {
            List<StallSlot> stallSlotEmpty = new List<StallSlot>();

            foreach (StallSlot slot in listStallSlot)
            {
                if (slot.CustomerPosition.IsEmpty())
                {
                    stallSlotEmpty.Add(slot);
                }
            }

            if (stallSlotEmpty.Count <= 0) return null;

            return stallSlotEmpty[Random.Range(0, stallSlotEmpty.Count)];
        }

        public void AddStaff()
        {
            Vector3 pos = LevelGrid.Instance.GetCorectWorldPosition(staffParent.position);
            Character staff = CharacterManager.Instance.CreateStaff(pos, staffParent);
            staff.GetComponent<CharacterVisual>().SetVisualStaff(staffParent.childCount - 1);
            TaskManager.Instance.SetupStaffTask(staff);
        }

        public Counter GetCounter()
        {
            return counter;
        }
    }
}
