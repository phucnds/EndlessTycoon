using EndlessTycoon.LevelGrids;
using EndlessTycoon.TaskSystems;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class LevelManager : Singleton<LevelManager>
    {

        public Transform startPos;
        public Transform endPos;

        [SerializeField] private Stall stall;
        [SerializeField] private int customerCount = 1;

        [NaughtyAttributes.Button]
        private void Initialize()
        {
            stall.AddStaff();

            for (int i = 0; i < 5; i++)
            {
                CreateCustomer();

                GotoStall();
            }
        }

        private void CreateCustomer()
        {
            Vector3 pos = LevelGrid.Instance.GetCorectWorldPosition(startPos.position);
            Character customer = CharacterManager.Instance.CreateCustomer(pos);
            TaskManager.Instance.SetupCustomerTask(customer);
        }

        [NaughtyAttributes.Button]
        public void GotoStall()
        {
            TaskManager.Instance.TaskSystemCustomer.EnqueueTask(() =>
            {
                if (customerCount > 0)
                {
                    StallSlot slot = stall.GetRandomStallSlot();

                    if (slot != null)
                    {
                        customerCount--;
                        slot.CustomerPosition.SetHasCharacterIncoming(true);
                        CustomerTask task = new CustomerTask.Order
                        {
                            stallSlotPos = slot.CustomerPosition.GetPosition(),
                            order = (character) =>
                            {
                                slot.CustomerPosition.SetCharacter(character);
                                Debug.Log("set character");
                            }
                        };

                        return task;
                    }
                }
                return null;
            });
        }

        /*
         private void TakeOrder(Character customer)
         {

             TaskManager.Instance.TaskSystemStaff.EnqueueTask(() =>
             {
                 if (stall.stallSlot.StaffIsEmpty())
                 {
                     Debug.Log("Staff null");

                     stall.stallSlot.SetHasCustomerIncoming(true);

                     StallTask task = new StallTask.TakeOrder
                     {
                         stallSlotPos = stall.stallSlot.GetStaffSlotPos().position,
                         takeOrder = (staff) =>
                         {
                             staff.GetComponent<DisplayProgress>().StartProgression(1f, () =>
                             {
                                 customer.GetComponent<DisplayOrder>().ToggleOrderItem(true);
                                 Deliver(customer);
                             });
                         }
                     };

                     return task;
                 }

                 return null;
             });
         }

         /*
        private void Deliver(Character customer)
        {
            TaskManager.Instance.TaskSystemStaff.EnqueueTask(() =>
            {
                if (stall.counter.StaffIsEmpty())
                {
                    Debug.Log("Staff null");

                    stall.stallSlot.SetHasCustomerIncoming(true);

                    StallTask task = new StallTask.Deliver
                    {
                        counterPos = stall.counter.GetCounterPos().position,
                        stallSlotPos = stall.stallSlot.GetStaffSlotPos().position,
                        takePayment = () =>
                        {
                            customer.isWaiting = false;
                            customer.GetComponent<DisplayOrder>().ToggleOrderItem(false);
                            Debug.Log("get money");
                            customer.GetComponent<WaitingAction>().isWaiting = false;
                            GotoStall();
                        }
                    };

                    return task;
                }

                return null;
            });
        }
        */

    }
}


