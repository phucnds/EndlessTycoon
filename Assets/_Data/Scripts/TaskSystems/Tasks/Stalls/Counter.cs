using EndlessTycoon.Core;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Transform counterPos;
    [SerializeField] private Character staff;

    private bool hasStaffIncoming;

    public void SetStaff(Character character)
    {
        staff = character;
        hasStaffIncoming = false;

        if (staff != null)
        {
            Debug.Log("hasStaff");

        }
    }

    public void SetHasStaffIncoming(bool hasStaffIncoming)
    {
        this.hasStaffIncoming = hasStaffIncoming;
    }

    public bool StaffIsEmpty()
    {
        return staff == null && !hasStaffIncoming;

    }

    public Transform GetCounterPos()
    {
        return counterPos;
    }
}
