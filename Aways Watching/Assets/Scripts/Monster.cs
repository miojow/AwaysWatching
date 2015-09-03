using UnityEngine;
using System.Collections;

 [System.Serializable]
public class Monster : MonoBehaviour {
     public enum Type { Agressive, Passive };
     public Type MonsterType;
     public float Distance;
     public float Speed;
     public int Life;
     public float Power;
     public float lookDist = 10;
     public float rotateSpeed = 5;
     private GameObject Player;
     private Controller controller;
     private Rigidbody rigdBody;
     public bool follow = false;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigdBody = gameObject.GetComponent<Rigidbody>();
        rigdBody.isKinematic = true;
        switch(MonsterType)
        {
            case Type.Agressive:
                Distance = 10;
                Speed = 1;
                Life = 30;
                Power = 10;
                break;
            case Type.Passive:
                Distance = 1f;
                Speed = 0.5f;
                Life = 50;
                Power = 30;

                break;
        }

	}
	

    void Update()
    {
        if (this.gameObject.name == "Monster")
        {
            Debug.Log(Vector3.Distance(transform.position, Player.transform.position));
        }
        if (Vector3.Distance(transform.position, Player.transform.position) <= lookDist)
        {
            //Quaternion rotation = Quaternion.LookRotation(Player.transform.position - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            transform.LookAt(Player.transform.position);
            RaycastHit hit;
            //Se o jogador estiver ao alcance
            if (Vector3.Distance(transform.position, Player.transform.position) <= Distance)
            {
                rigdBody.isKinematic = false;
                //Joga o Raycast
                if (Physics.Raycast(transform.position, Player.transform.position, out hit, Distance))
                {
                    //Se acertar o Jogador
                    Transform objectHit = hit.transform;
                    if (objectHit.tag == "Player")
                    {
                        follow = true;
                    }
                }
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }

        }
        else
        {
            rigdBody.isKinematic = true;
            follow = false;
        }


        if (follow)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

    }

}
