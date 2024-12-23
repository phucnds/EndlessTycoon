using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class SlotPosition
    {
        private Transform slotTransform;
        private Character character;

        protected bool hasCharacterIncoming;

        public SlotPosition(Transform slotTransform)
        {
            this.slotTransform = slotTransform;
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
        private StallSlot stallSlot;

        public CustomerSlotPosition(Transform slotTransform, StallSlot stallSlot) : base(slotTransform)
        {
            this.stallSlot = stallSlot;
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
                        character.GetComponent<DisplayOrder>().ToggleOrderItem(true);
                        Deliver(staff, character);
                    }
                };

                TaskManager.Instance.TaskSystemStaff.AddTask(task);
            }
        }

        private void Deliver(Character staff, Character customer)
        {
            TaskManager.Instance.TaskSystemStaff.EnqueueTask(() =>
            {
                foreach (CounterSlot slot in stallSlot.GetStall().GetCounter().GetList())
                {
                    if (slot.StaffPosition.IsEmpty())
                    {
                        slot.StaffPosition.SetHasCharacterIncoming(true);

                        StallTask task = new StallTask.Deliver
                        {
                            counterPos = slot.StaffPosition.GetPosition(),
                            stallSlotPos = stallSlot.StaffPosition.GetPosition(),
                            reachedCounterSlot = () => { slot.StaffPosition.SetCharacter(staff); },
                            doneProduce = () => { slot.StaffPosition.SetCharacter(null); },
                            takePayment = () =>
                            {
                                customer.GetComponent<DisplayOrder>().ToggleOrderItem(false);
                                customer.GetComponent<WaitingAction>().isWaiting = false;
                                SetCharacter(null);
                                LevelManager.Instance.NewCustomerComing();

                                int money = stallSlot.GetStall().GetCounter().GetIncome();
                                CurrencyManager.Instance.AddCurrency(money);
                                EffectManager.Instance.SetText(customer.transform, money);
                                SoundManager.Instance.PlaySFXCollectCoin();
                            }
                        };

                        return task;
                    }
                }

                return null;
            });
        }

    }

    public class StaffSlotPosition : SlotPosition
    {
        private StallSlot stallSlot;

        public StaffSlotPosition(Transform slotTransform, StallSlot stallSlot) : base(slotTransform)
        {
            this.stallSlot = stallSlot;
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

