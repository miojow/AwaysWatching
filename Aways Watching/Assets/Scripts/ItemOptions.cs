using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemOptions : MonoBehaviour {
    public Item item;
    public Inventory inventory;
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
                weapon.transform.position = new Vector3(controller.Hand.transform.position.x - 0.212f, controller.Hand.transform.position.y - 0.184f, controller.Hand.transform.position.z + 0.002f);
                weapon.transform.rotation = controller.Hand.transform.rotation;
                weapon.transform.rotation = Quaternion.Euler(new Vector3(controller.Hand.transform.rotation.x + 26484f, controller.Hand.transform.rotation.y + 258.399f, controller.Hand.transform.rotation.z + 60.21963f));
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
                GameObject ite = item.itemModel;
                Collider[] cols = ite.GetComponents<Collider>();
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].enabled == false)
                    {
                        cols[i].enabled = true;
                    }
                }
                Instantiate(ite, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
                inventory.removeItem(item);
            }
            #endregion
        }
        #endregion

    }
}
