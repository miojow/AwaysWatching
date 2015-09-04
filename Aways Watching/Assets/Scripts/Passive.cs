using UnityEngine;
using System.Collections;

public class Passive : MonoBehaviour {

    [Tooltip("Distancia para seguir Player")]
    public float Distance;
    [Tooltip("Distancia para Enxergar Player")]
    public float lookDist = 10;
    public int Life;
    public float Power;
    public GameObject Player;
    private Controller controller;
    private Rigidbody rigdBody;
    public bool follow = false;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigdBody = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //rigdBody.isKinematic = true;
    }
    void Update()
    {

        if (Vector3.Distance(transform.position, Player.transform.position) <= lookDist)
        {
            if (agent.velocity == Vector3.zero)
            {
                transform.LookAt(Player.transform.position);
            }
            //Se o jogador estiver ao alcance
            if (Vector3.Distance(transform.position, Player.transform.position) <= Distance)
            {
                //rigdBody.isKinematic = false;
                follow = true;
            }

        }
        else
        {
            follow = false;
            //rigdBody.isKinematic = true;
            agent.Stop();
            
        }

        if (follow)
        {
            agent.SetDestination(Player.transform.position);
        }

        //if (Vector3.Distance(transform.position, Player.transform.position) < 1)//PARA DEBUG
        //{
        //    follow = false;
        //    //rigdBody.isKinematic = true;
        //    agent.Stop();
        //}
    }

}
