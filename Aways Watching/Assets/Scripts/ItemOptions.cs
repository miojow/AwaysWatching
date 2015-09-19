using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemOptions : MonoBehaviour {
    public Item item;
    public Inventory inventory;
    public ItemDatabase database;
    public enum Options
    {
        Use,
        Info,
        Release,
        Exit
    }
    public Options opt;
	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
	}

    public void CmdExit()
    {
        opt = Options.Exit;
    }

    public void CmdUse()
    {
        opt = Options.Use;
    }

    public void CmdRelease()
    {
        opt = Options.Release;
    }

    public void CmdInfo()
    {
        opt = Options.Info;
    }



    public void Actions()
    {
        #region Consumables
        if (item.itemType == Item.ItemType.Consumable)
        {
            if (opt == Options.Use)
            {
                int life = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Life;
                if (life < 100)
                {
                    if ((life + item.itemPower) <= 100)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Life = item.itemPower;
                        Destroy(item.gameObject);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Life = 100;
                        Destroy(item.gameObject);
                    }
                }
                else
                {
                    Debug.Log("Vida cheia");
                }
            }
            else if (opt == Options.Exit)
            {
                inventory.CloseOptions();
            }
            else if(opt == Options.Release)
            {
                GameObject ite = item.itemModel;
                Instantiate(ite, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
                inventory.removeItem(item);
            }
        }

        #endregion

        #region Weapons
        else if (item.itemType == Item.ItemType.Weapon)
        {
            #region Exit
            if (opt == Options.Exit)
            {
                inventory.CloseOptions();
            }
            #endregion
            #region Use
            if (opt == Options.Use)
            {
                Controller controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
                GameObject weapon = Instantiate(item.itemModel, GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Hand.transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Hand.transform.rotation) as GameObject;
                weapon.transform.parent = controller.Hand.transform;
                weapon.transform.position = controller.Hand.transform.position;
                Collider[] cols = weapon.GetComponents<Collider>();
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].isTrigger == true)
                    {
                        cols[i].enabled = false;
                    }
                }
                Destroy(weapon.GetComponent<Rigidbody>());
                inventory.removeItem(item);
            }
            #endregion
            #region Release
            else if (opt == Options.Release)
            {
                for (int i = 0; i < database.items.Count; i++)
                {
                    if (database.items[i].itemID == item.itemID)
                    {
                        GameObject ite = database.items[i].itemModel;
                        Instantiate(ite, GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().DropItem.transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
                        inventory.removeItem(item);
                        break;
                    }
                }
               
            }
            #endregion
        }
        #endregion

    }
}
