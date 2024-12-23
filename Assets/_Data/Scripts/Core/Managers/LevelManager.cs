using EndlessTycoon.LevelGrids;
using EndlessTycoon.TaskSystems;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class LevelManager : Singleton<LevelManager>
    {

        public Transform startPos;
        public Transform endPos;

        [field: SerializeField] public Stall Stall { get; private set; }
        [field: SerializeField] public int CustomerCount { get; private set; } = 1;

        [NaughtyAttributes.Button]
        private void Initialize()
        {
            Stall.AddStaff();

            for (int i = 0; i < 5; i++)
            {
                CreateCustomer();

                GotoStall();
            }
        }

        [NaughtyAttributes.Button]
        public void IncreaseCustomer()
        {
            CustomerCount++;
        }

        [NaughtyAttributes.Button]
        public void IncreaseStaff()
        {
            Stall.AddStaff();
        }

        [NaughtyAttributes.Button]
        public void AddCounterSlot()
        {
            Stall.GetCounter().AddSlot();
        }

        private void CreateCustomer()
        {
            Vector3 pos = LevelGrid.Instance.GetCorectWorldPosition(startPos.position);
            Character customer = CharacterManager.Instance.CreateCustomer(pos);
            customer.GetComponent<CharacterVisual>().SetVisualCustomer();
            TaskManager.Instance.SetupCustomerTask(customer);
        }

        public void NewCustomerComing()
        {
            CreateCustomer();
            IncreaseCustomer();
            GotoStall();
        }

        [NaughtyAttributes.Button]
        public void GotoStall()
        {
            TaskManager.Instance.TaskSystemCustomer.EnqueueTask(() =>
            {
                if (CustomerCount > 0)
                {
                    StallSlot slot = Stall.GetRandomStallSlot();

                    if (slot != null)
                    {
                        CustomerCount--;
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
    }
}


