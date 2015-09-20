using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public Camera[] multiCam;
    public GameObject player;
    private Vector3 thisPos; //Posição atual da que a camera olha
    private float divisionY = 5;
    private float divisionX = 7;
    private Vector3 c; // Centro da camera
    private Vector3 p;
    private RaycastHit hit;
    private Ray ray;
    public CameraController camController;
    private Vector3 myRotation;
    public float dist;


    void Awake()
    {
        multiCam =  FindObjectsOfType(typeof(Camera)) as Camera[];
        thisCam = gameObject.GetComponent<Camera>();
        myRotation = thisCam.transform.eulerAngles;
        camController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
        thisCam.enabled = false;
        thisPos = Vector3.forward;
        c = new Vector3(0.5F, 0.5F, 0);
        p = thisCam.transform.position;

    }

    void Update()
    {
        float posX = 0;
        float posY = 0;
        if (!player)
        {
            for (int i = 0; i < 10; i++)
            {
                if (player != null)
                {
                    break;
                }
                for (int p = 0; p < 10; p++)
                {
                    if (player != null)
                    {
                        break;
                    }
                    ray = thisCam.ViewportPointToRay(new Vector3(posX, posY, 0));
                    Debug.DrawRay(ray.origin, ray.direction, Color.green);
                    if (Physics.Raycast(ray, out hit, 20))
                    {
                        Transform objectHit = hit.transform;
                        if (objectHit.tag == "Player")
                        {
                            camController.enableMyCamera(thisCam);
                            player = objectHit.gameObject;
                        }
                        else
                        {
                            player = null;
                        }
                    }

                    posY = posY + 0.125f;
                }
                posY = 0;
                posX = posX + 0.125f;
            }
        }
        if (thisCam.enabled==true && player != null)
        {
            thisCam.transform.LookAt(player.transform.position);
            dist = Vector3.Distance(thisCam.transform.position, player.transform.position);
            Debug.DrawLine(transform.position, player.transform.position);
        }
        else if(thisCam.enabled == false)
            {
                thisCam.transform.LookAt(null);
                player = null;
                thisCam.transform.eulerAngles = myRotation;
            }

    }

    public void resetPlayer()
    {
        player = null;
    }




}
