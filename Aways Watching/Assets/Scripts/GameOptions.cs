using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {

    private bool Paused = false;
    GameObject inventory;
    Inventory inv;
	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inv = inventory.GetComponent<Inventory>();
        inventory.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.I) && !Paused)
        {
            Debug.Log("TESTE");
            inventory.SetActive(true);
            Paused = true;
            Time.timeScale = 0.0f;
        }

        else if (Input.GetKeyDown(KeyCode.I) && Paused)
        {
            inv.CloseOptions();
            inventory.SetActive(false);
            Paused = false;
            Time.timeScale = 1.0f;
        }
	}
}
