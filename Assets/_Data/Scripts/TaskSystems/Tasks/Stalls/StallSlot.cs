using EndlessTycoon.Core;
using UnityEngine;

public class StallSlot : MonoBehaviour
{
    private void Start()
    {
        SetCustomer(null);
        SetStaff(null);

    }

    [SerializeField] private Transform orderPos;
    [SerializeField] private Character customer;

    private bool hasCustomerIncoming;

    public void SetCustomer(Character character)
    {
        customer = character;
        hasCustomerIncoming = false;

        if (customer != null)
        {
            Debug.Log("hasCustomer");
        }
    }

    public void SetHasCustomerIncoming(bool hasCustomerIncoming)
    {
        this.hasCustomerIncoming = hasCustomerIncoming;
    }

    public bool CustomerIsEmpty()
    {
        return customer == null && !hasCustomerIncoming;

    }

    public Transform GetOrderPos()
    {
        return orderPos;
    }

    // ================================================================================================

    [SerializeField] private Transform stallStaffSlotPos;
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

    public Transform GetStaffSlotPos()
    {
        return stallStaffSlotPos;
    }
}
