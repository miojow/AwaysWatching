using UnityEngine;
using System.Collections;

public class ItemOptions : MonoBehaviour {
    public Item item;
    public GameObject inventory;
    public enum Options
    {
        Use,
        Examine,
        Drop,
        Exit

    }


	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
	}
	


	// Update is called once per frame
	void Update () 
    {
        Debug.Log(inventory.activeSelf);
	}
}
