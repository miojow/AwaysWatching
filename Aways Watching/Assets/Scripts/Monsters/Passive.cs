using UnityEngine;
using System.Collections;

public class Passive :  MonoBehaviour {

    public GameObject Player;
    private Controller controller;
    private Rigidbody rigdBody;
    public Monster monster;
    public bool follow = false;
    private NavMeshAgent agent;
	// Use this for initialization
	void Awake () {
        monster = gameObject.GetComponent<Monster>();
        //gameObject.GetComponent<CharacterController>().detectCollisions = true;
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        //rigdBody.isKinematic = true;
    }

    void LateUpdate()
    {


    }

    void Update()
    {
        if (follow == true)
        {
            if (agent.hasPath)
            {
                agent.Resume();
            }
            else
            {
                agent.SetDestination(Player.transform.position);
            }


        }

        if (!monster.coolDown && !monster.stun)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) <= monster.lookDist)
            {
                if (agent.velocity == Vector3.zero)
                {
                    transform.LookAt(Player.transform.position);

                }
                //Se o jogador estiver ao alcance
                if (Vector3.Distance(transform.position, Player.transform.position) <= monster.Distance)
                {

                    follow = true;

                }
            }
            else
            {
                follow = false;
                agent.Stop();
            }
        }
        else
        {
            follow = false;
            agent.Stop();
        }



    }








}
