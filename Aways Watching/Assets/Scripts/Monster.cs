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
     private GameObject Player;
     private Controller controller;
     private Rigidbody rigdBody;
     public bool follow = false;
     private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigdBody = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
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
                Distance = 5f;
                Speed = 0.5f;
                Life = 50;
                Power = 30;

                break;
        }

	}
	

    void Update()
    {

        if (Vector3.Distance(transform.position, Player.transform.position) <= lookDist)
        {
            transform.LookAt(Player.transform.position);
            //Se o jogador estiver ao alcance
            if (Vector3.Distance(transform.position, Player.transform.position) <= Distance)
            {
                rigdBody.isKinematic = false;
                        follow = true;
             }
        }
        else
        {
            rigdBody.isKinematic = true;
            follow = false;
        }


        if (follow)
        {
            agent.SetDestination(Player.transform.position);
        }

    }

}
