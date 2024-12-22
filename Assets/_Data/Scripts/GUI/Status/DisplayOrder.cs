using UnityEngine;

public class DisplayOrder : MonoBehaviour
{
    [SerializeField] private GameObject orderItem;

    public void ToggleOrderItem(bool flag)
    {
        orderItem.SetActive(flag);
    }
}
