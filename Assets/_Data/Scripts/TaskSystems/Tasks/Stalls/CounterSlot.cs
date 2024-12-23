using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class CounterSlot : MonoBehaviour
    {
        [SerializeField] private Transform slotTransform;
        public SlotPosition StaffPosition { get; private set; }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            StaffPosition = new SlotPosition(slotTransform);
        }
    }

}
