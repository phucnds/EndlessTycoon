using EndlessTycoon.Core;
using UnityEngine;

namespace EndlessTycoon.TaskSystems
{
    public class TaskManager : Singleton<TaskManager>
    {
        public TaskSystem<CustomerTask> TaskSystemCustomer { get; private set; }
        public TaskSystem<StallTask> TaskSystemStaff { get; private set; }

        private void Start()
        {
            TaskSystemCustomer = new TaskSystem<CustomerTask>();
            TaskSystemStaff = new TaskSystem<StallTask>();
        }

        public void SetupCustomerTask(Character character)
        {
            CustomerBehaviour characterTask = character.GetComponent<CustomerBehaviour>();
            characterTask.Setup(TaskSystemCustomer);
        }

        public void SetupStaffTask(Character character)
        {
            StaffBehaviour characterTask = character.GetComponent<StaffBehaviour>();
            characterTask.Setup(TaskSystemStaff);
        }


    }
}


