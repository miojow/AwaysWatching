using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {

    private bool Paused = false;
    public bool close = false;
    GameObject inventory;
    Inventory inv;
    InfoPanel info;
	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        info = GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<InfoPanel>();
        inv = inventory.GetComponent<Inventory>();
        inventory.SetActive(false);
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.I) && !Paused)
        {
            inventory.SetActive(true);
            Paused = true;
            Time.timeScale = 0.0f;
        }

        else if ((Input.GetKeyDown(KeyCode.I) && Paused) || close)
        {
            info.ClearText();
            inv.CloseOptions();
            inventory.SetActive(false);
            Paused = false;
            Time.timeScale = 1.0f;
            close = false;
        }
	}
}
