using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Agressive : MonoBehaviour {
    private Transform target;
    private Rigidbody rigdBody;
    private Monster monster;
    public bool follow = false;
    private NavMeshAgent agent;
    //Variaveis de Walk
    float posX;
    float posY;
    float posZ;
    private bool Walking;
    private bool newPosition;
    private bool getWaypoints = true;
    public float PlayerDistance;
    Vector3 destination;
    private List<Transform> Waypoints = new List<Transform>();


	void Start () {
        monster = gameObject.GetComponent<Monster>();
        rigdBody = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        newPosition = true;

	}
	

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, monster.lookDist))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (hit.transform.tag == "Player")
            {
                target = hit.transform;
                
            }
        }

        if (target)
        {
            PlayerDistance = Vector3.Distance(transform.position, target.transform.position);
            Walking = false;
            if (Vector3.Distance(transform.position, target.transform.position) <= monster.lookDist)
            {
                //Se o jogador estiver ao alcance
                if (Vector3.Distance(transform.position, target.transform.position) <= monster.Distance)
                {
                    follow = true;
                }
            }
            else
            {
                follow = false;
                target = null;
            }


            if (follow && !monster.stun && !monster.coolDown)
            {
                if (agent.hasPath)
                {
                    agent.Resume();
                }
                else
                {
                    agent.SetDestination(target.transform.position);
                }

            }
            else if(monster.stun || monster.coolDown)
            {
                agent.Stop();
            }
        }
        else
        {
                Walk();
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Patrol" && getWaypoints)
        {
            Transform[] objects = col.GetComponentsInChildren<Transform>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Waypoint")
                {
                    Waypoints.Add(objects[i].transform);
                }

            }
            getWaypoints = false;
        }

    }

    void Walk()
    {
        if (newPosition)
        {
            StartCoroutine(Picknumber(15f));
            agent.SetDestination(destination);
        }


        if (Vector3.Distance(transform.position,destination)<1)
        {
            newPosition = true;
        }

    }

     IEnumerator Picknumber (float time){

         int pick = Random.Range(0, Waypoints.Count-1);
         posX = Waypoints[pick].position.x;
         posY = Waypoints[pick].position.y;
         posZ = Waypoints[pick].position.z;
         destination = new Vector3(posX, posY, posZ);
         newPosition = false;
         yield return new WaitForSeconds(time);
         newPosition = true;

     }


}
