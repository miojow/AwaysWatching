using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int KeyID;
    string KeyName;
    public Inventory inventory;
    TextPanel textPanel;
    void Awake()
    {
        textPanel = GameObject.FindGameObjectWithTag("TextPanel").GetComponent<TextPanel>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Player")
        {
            Other.GetComponent<Controller>().TriggerName = "Door";
            if (Other.GetComponent<Controller>().Key == KeyID)
            {
                ItemDatabase database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

                foreach (Item i in database.items)
                {
                    if (i.itemID == KeyID)
                    {
                        KeyName = i.itemName;
                        inventory.removeItem(i);
                        break;
                    }
                }
                string Message = "Você abriu a porta com a chave " + KeyName;
                Other.GetComponent<Controller>().TriggerName = "";
                textPanel.WriteText(Message);
                Destroy(this.gameObject);
            }
            else if (Other.GetComponent<Controller>().Key > 0 && Other.GetComponent<Controller>().Key != KeyID)
            {
                string Message = "Nada acontece.";
                textPanel.WriteText(Message);
            }
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "Player")
        {
            Other.GetComponent<Controller>().TriggerName = "";
        }
    }


}
