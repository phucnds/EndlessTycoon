using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class SlotPosition
    {
        private Transform slotTransform;
        private Character character;
        protected StallSlot stallSlot;

        protected bool hasCharacterIncoming;

        public SlotPosition(Transform slotTransform, StallSlot stallSlot)
        {
            this.slotTransform = slotTransform;
            this.stallSlot = stallSlot;
            SetCharacter(null);
        }

        public virtual void SetCharacter(Character character)
        {
            this.character = character;
            hasCharacterIncoming = false;
        }

        public void SetHasCharacterIncoming(bool hasCharacterIncoming)
        {
            this.hasCharacterIncoming = hasCharacterIncoming;
        }

        public bool IsEmpty()
        {
            return character == null && !hasCharacterIncoming;

        }

        public Vector3 GetPosition()
        {
            return slotTransform.position;
        }
    }


    public class CustomerSlotPosition : SlotPosition
    {
        public CustomerSlotPosition(Transform slotTransform, StallSlot stallSlot) : base(slotTransform, stallSlot)
        {
        }

        public override void SetCharacter(Character character)
        {
            base.SetCharacter(character);

            if (character != null)
            {
                StallTask task = new StallTask.TakeOrder
                {
                    stallSlotPos = stallSlot.StaffPosition.GetPosition(),
                    takeOrder = (staff) =>
                    {
                        staff.GetComponent<DisplayProgress>().StartProgression(1f, () =>
                        {
                            character.GetComponent<DisplayOrder>().ToggleOrderItem(true);
                            Debug.Log("deliver");
                        });
                    }
                };

                TaskManager.Instance.TaskSystemStaff.AddTask(task);
            }
        }

    }

    public class StaffSlotPosition : SlotPosition
    {
        public StaffSlotPosition(Transform slotTransform, StallSlot stallSlot) : base(slotTransform, stallSlot)
        {
        }

        public override void SetCharacter(Character character)
        {
            base.SetCharacter(character);

            if (character != null)
            {
                // StallTask task = new StallTask.Deliver {
                    
                // }
            }
        }
    }
}

