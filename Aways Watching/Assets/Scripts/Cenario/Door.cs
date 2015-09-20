using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int KeyID;
    string KeyName;
    TextPanel t;
    void Awake()
    {
         t= GameObject.FindGameObjectWithTag("TextPanel").GetComponent<TextPanel>();
    }

    void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Player")
        {
            if (Other.GetComponent<Controller>().Key == KeyID)
            {
                
                ItemDatabase database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

                foreach (Item i in database.items){
                    if(i.itemID == KeyID)
                    {
                        KeyName = i.itemName;
                        break;
                    }
                }
                string Message = "Você abriu a porta com a chave "+KeyName;
                t.WriteText(Message);
                Destroy(this.gameObject);
            }
        }
    }


}
