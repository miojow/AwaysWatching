using UnityEngine;
using System.Collections;

public class WeaponCollision : MonoBehaviour {

    public Item weapon;
    Controller controller;
	// Use this for initialization
	void Start () {
        weapon = gameObject.GetComponent<Item>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();   
	}
	
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Monster")
        {
            if (!controller.canAttack) // Só colide se o jogador estiver atacando
            {
                Monster monster = col.gameObject.GetComponentInParent<Monster>();
                Debug.Log("COLIDIU COM O MONSTRO");
                monster.Life -= weapon.itemPower;
                monster.stun = true;
            }
        }

    }
}
