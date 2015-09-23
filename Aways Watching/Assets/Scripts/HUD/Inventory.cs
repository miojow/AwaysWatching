using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<GameObject> Slots = new List<GameObject>();
    public List<Item> Items = new List<Item>();
    public GameObject slots;
    ItemDatabase database;
    public GameObject options;
    ItemOptions itemOptions;
    float rectX;
    float rectY;
    float startx = -122;
    float starty = -20;


    public void ShowOptions(Item item)
    {
        options.SetActive(true);
        itemOptions.item = item;
    }
    public void CloseOptions()
    {
        options.SetActive(false);
    }

	void Start () {
        rectX = gameObject.GetComponent<RectTransform>().rect.width;
        rectY = gameObject.GetComponent<RectTransform>().rect.height;
        startx =  rectX - 334;
        starty = rectY - 680;
        int slotAmount = 0;
        itemOptions = options.GetComponent<ItemOptions>();
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
                startx = startx + 60;
                if (p == 4)
                {
                    startx = -90;
                    starty = starty - 60;

                }
                slotAmount++;

            }
        }
        
	}

    public void addItem(int id)
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            if(database.items[i].itemID == id)
            {
                Item item = database.items[i];
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
                Debug.Log(Items.Count);
                break;
            }
        }
    }

    public void removeItem(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Equals(item.itemName))
            {
                Items.RemoveAt(i);
                Items.Add(new Item());//Adicionando um item Vazio
                break;
            }
        }
    }

}



