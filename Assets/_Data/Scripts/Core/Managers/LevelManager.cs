using EndlessTycoon.LevelGrids;
using EndlessTycoon.TaskSystems;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private Transform startPos;
        public Transform endPos;
        [SerializeField] private Stall stall;


        [NaughtyAttributes.Button]
        private void Initialize()
        {
            // CreateCustomer();
            CreateStaff();
        }

        private void CreateCustomer()
        {
            Vector3 pos = LevelGrid.Instance.GetCorectWorldPosition(startPos.position);
            Character customer = CharacterManager.Instance.CreateCustomer(pos);
            TaskManager.Instance.SetupCustomerTask(customer);
        }

        private void CreateStaff()
        {
            Vector3 pos = LevelGrid.Instance.GetCorectWorldPosition(stall.staffParent.position);
            Character staff = CharacterManager.Instance.CreateStaff(pos, stall.staffParent);
            TaskManager.Instance.SetupStaffTask(staff);

            Character staff1 = CharacterManager.Instance.CreateStaff(pos, stall.staffParent);
            TaskManager.Instance.SetupStaffTask(staff1);
        }



        [NaughtyAttributes.Button]
        public void GotoStall()
        {
            CreateCustomer();
            CustomerTask task = new CustomerTask.Order
            {
                stallSlotPos = stall.stallSlot.GetOrderPos().position,
                order = (character) =>
                {
                    TakeOrder(character);
                    character.goneStall = true;
                }
            };

            TaskManager.Instance.TaskSystemCustomer.AddTask(task);
        }

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

    }
}


