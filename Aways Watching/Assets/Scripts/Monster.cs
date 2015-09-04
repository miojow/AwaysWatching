using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

 [System.Serializable]
public class Monster : MonoBehaviour {
     public enum Type { Agressive, Passive };
     public Type MonsterType;
     public float Distance;
     public float Speed;
     public int Life;
     public float Power;
     public float lookDist = 10;
     private GameObject target;
     private Controller controller;
     private Rigidbody rigdBody;
     public bool follow = false;
     private NavMeshAgent agent;
     //Variaveis de Walk
     float posX;
     float posY;
     public List <Transform> Waypoints = new List<Transform>();


	void Start () {
        //target = GameObject.FindGameObjectWithTag("Player");
        rigdBody = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //rigdBody.isKinematic = true;
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
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= lookDist)
            {
                transform.LookAt(target.transform.position);
                //Se o jogador estiver ao alcance
                if (Vector3.Distance(transform.position, target.transform.position) <= Distance)
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
                agent.SetDestination(target.transform.position);
            }
        }
        else
        {
            Walk();

        }

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Patrol")
        {
            //Debug.Log("LOL");
            Transform[] objects = col.GetComponentsInChildren<Transform>();
            for (int i = 0; i < objects.Length; i++)
            {
                //Debug.Log(objects[i].name);
                if (objects[i].tag == "Waypoint")
                {
                    Waypoints.Add(objects[i].transform);
                }

            }      
        }

    }

    void Walk()
    {

        int pick = Rando

    }

     IEnumerator Picknumber (float time){


         int pick = Random.Range(0, Waypoints.Count);
        
         posX = Waypoints.ToList<1>;
         yield return new WaitForSeconds(time);
     }


}
