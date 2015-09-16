using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<GameObject> Slots = new List<GameObject>();
    public List<Item> Items = new List<Item>();
    public GameObject slots;
    ItemDatabase database;
    float rectX;
    float rectY;
    float startx = -122;
    float starty = -20;
    public GameObject toolTip;

    public void ShowTooltip(Vector3 toolPosition,Item item)
    {
        toolTip.SetActive(true);
        toolTip.GetComponent<RectTransform>().localPosition = new Vector3(toolPosition.x + 500, toolPosition.y, toolPosition.z);

    }
    public void CloseToolTip()
    {
        toolTip.SetActive(false);
    }
	void Start () {
        rectX = gameObject.GetComponent<RectTransform>().rect.width;
        rectY = gameObject.GetComponent<RectTransform>().rect.height;
        startx =  rectX - 180;
        starty = rectY - 530;
        int slotAmount = 0;
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        for (int i = 1; i < 5; i++)
        {
            for (int p = 1; p < 5; p++)
            {
                GameObject slot = Instantiate(slots) as GameObject;
                slot.GetComponent<SlotScript>().slotNumber = slotAmount;
                Slots.Add(slot);
                Items.Add(new Item());
                slot.transform.parent = this.gameObject.transform;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(startx, starty, 0);
                slot.name = "slot" + i + "." + p;
                startx = startx + 100;
                if (p == 4)
                {
                    startx = -180;
                    starty = starty - 100;

                }
                slotAmount++;

            }
        }

        addItem(01);
        addItem(02);
	}

    void addItem(int id)
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            if(database.items[i].itemID == id)
            {
                Item item = database.items[i];
                Debug.Log(item.itemName);
                addItemAtEmptySlot(item);
                break;
            }
        }
    }

    void addItemAtEmptySlot(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == null)
            {
                Items[i] = item;
                break;
            }
        }
    }
	

}
