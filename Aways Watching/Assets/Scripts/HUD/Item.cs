using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string itemName;
    public int itemID;
    public string itemDesc;
    public Sprite itemIcon;
    public GameObject itemModel;
    public int itemPower;
    public ItemType itemType;
    public enum ItemType
    {
        Weapon,
        Consumable,
        Document,
        Key
    }
    public Item(string name, int id, string desc, int power, ItemType type )
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemPower = power;
        itemType = type;
        itemIcon = Resources.Load<Sprite>("Sprite" + name);
        itemModel = Resources.Load<GameObject>("Prefab"+name);
    }
    public Item()
    {

    }
}
