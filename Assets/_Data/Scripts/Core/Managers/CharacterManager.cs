using UnityEngine;

namespace EndlessTycoon.Core
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        [SerializeField] private Character customer;
        [SerializeField] private Transform customerParent;

        [SerializeField] private Character staff;

        public Character CreateCustomer(Vector3 pos)
        {
            Character character = Instantiate(customer, pos, Quaternion.identity, customerParent);
            return character;
        }

        public Character CreateStaff(Vector3 pos, Transform tr)
        {
            Character character = Instantiate(staff, pos, Quaternion.identity, tr);
            return character;
        }

    }
}


