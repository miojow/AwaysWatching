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



    public void Actions()
    {
        #region Consumables
        if (item.itemType == Item.ItemType.Consumable)
        {
            if (opt == Options.Use)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Life = item.itemPower;
            }
            else if (opt == Options.Exit)
            {
                inventory.CloseOptions();
            }
        }

        #endregion

        #region WEapons
        else if (item.itemType == Item.ItemType.Weapon)
        {
            if (opt == Options.Exit)
            {
                inventory.CloseOptions();
            }
        }
        #endregion

    }
}
