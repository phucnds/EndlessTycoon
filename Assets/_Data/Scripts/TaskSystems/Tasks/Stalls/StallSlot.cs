using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class StallSlot : MonoBehaviour
    {
        [SerializeField] private Transform customerPos;
        [SerializeField] private Transform staffPos;

        public CustomerSlotPosition CustomerPosition { get; private set; }
        public SlotPosition StaffPosition { get; private set; }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            CustomerPosition = new CustomerSlotPosition(customerPos, this);
            StaffPosition = new SlotPosition(staffPos, this);
        }
    }
}


