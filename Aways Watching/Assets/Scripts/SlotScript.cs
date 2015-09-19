using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotScript : MonoBehaviour, IPointerDownHandler{

    public Item item;
    Image itemImage;
    public int slotNumber;
    Inventory inventory;

	void Start () 
    {
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}

    void Update()
    {
        if (inventory.Items[slotNumber].itemName != null)
        {
            item = inventory.Items[slotNumber];
            itemImage.enabled = true;
            itemImage.sprite = inventory.Items[slotNumber].itemIcon;
        }
        else
        {
            itemImage.enabled = false;
        }

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (inventory.Items[slotNumber].itemName != null)
        {
            inventory.ShowOptions(inventory.Items[slotNumber]);
        }
        else if (inventory.Items[slotNumber].itemName == null)
        {
            inventory.CloseOptions();
        }

    }

    //public void OnPointerEnter(PointerEventData data)
    //{
    //    if (inventory.Slots)
    //    {
    //        inventory.ShowTooltip(inventory.Slots[slotNumber].GetComponent<RectTransform>().localPosition, inventory.Items[slotNumber]);
    //    }
    //}

    //public void OnPointerExit(PointerEventData data)
    //{
    //    inventory.CloseToolTip();
    //}
}

