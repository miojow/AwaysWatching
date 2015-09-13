using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public Camera[] multiCam;
    public GameObject player;
    public Controller controller;
    private Vector3 thisPos; //Posição atual da que a camera olha
    private float divisionY = 5;
    private float divisionX = 7;
    private Vector3 c; // Centro da camera
    private Vector3 p;
    private RaycastHit hit;
    private Ray ray;


    void Awake()
    {
        multiCam =  FindObjectsOfType(typeof(Camera)) as Camera[];
        thisCam = gameObject.GetComponent<Camera>();
        thisCam.enabled = false;
        thisPos = Vector3.forward;
        c = new Vector3(0.5F, 0.5F, 0);
        p = thisCam.transform.position;
    }

    void Update()
    {

        //Ray topLeft = thisCam.ViewportPointToRay(new Vector3(0, 0, 0));
        //Ray topRight = thisCam.ViewportPointToRay(new Vector3(1, 0, 0));
        //Ray botRight = thisCam.ViewportPointToRay(new Vector3(1, 1, 0));
        //Ray botLeft = thisCam.ViewportPointToRay(new Vector3(0, 1, 0));
        float posX = 0;
        float posY = 0;
        if (!player)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int p = 0; p < 7; p++)
                {
                    ray = thisCam.ViewportPointToRay(new Vector3(posX, posY, 0));
                    Debug.DrawRay(ray.origin, ray.direction, Color.green);
                    if (Physics.Raycast(ray, out hit, 10))
                    {
                        Transform objectHit = hit.transform;
                        if (objectHit.tag == "Player")
                        {
                            controller = objectHit.GetComponent<Controller>();
                            if (controller.camera){
                                if (controller.camera.name != thisCam.name)
                                {
                                    controller.camera.enabled = false;
                                }
                            }
                            player = objectHit.gameObject;
                            controller.camera = thisCam;
                        }
                        else
                        {
                            thisCam.enabled = false;
                        }
                    }

                    posY = posY + 0.125f;
                }
                posY = 0;
                posX = posX + 0.125f;
            }
        }

        if (player)
        {
            thisCam.transform.LookAt(player.transform.position);
            Debug.DrawLine(transform.position, player.transform.position);
            if (Vector3.Distance(transform.position, player.transform.position) > 10)
            {
                player = null;
            }
        }
        


    //    //float minDistance = float.MaxValue;
    //    //int closest = 0;
    //    RaycastHit hit;
    //    Vector3 screenPos = thisCam.WorldToScreenPoint(player.transform.position);
    //    //Ray ray = thisCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    //    //for (int i = 0; i < multiCam.Length; i++)
    //    //{
    //    //    float distance = Vector3.Distance(player.transform.position, multiCam[i].transform.position);
    //    //    if (distance < minDistance)
    //    //    {
    //    //        minDistance = distance; closest = i;
    //    //    }
    //    //    foreach (Camera c in multiCam)
    //    //    {
    //    //        if (c != multiCam[closest])
    //    //        {
    //    //            c.enabled = false;
    //    //            c.GetComponent<AudioListener>().enabled = false;
    //    //        }
    //    //    }
    //    //    multiCam[closest].enabled = true;
    //    //    multiCam[closest].transform.LookAt(player.transform);
    //    //    multiCam[closest].GetComponent<AudioListener>().enabled = true;

    //    //}

    }







}
