using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public Camera[] multiCam;
    public GameObject player;
    public Controller controller;

    void Awake()
    {
        multiCam =  FindObjectsOfType(typeof(Camera)) as Camera[];
        thisCam = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        thisCam.enabled = false;
        controller = player.GetComponent<Controller>();
    }

    void Update()
    {
        float minDistance = float.MaxValue;
        int closest = 0;
        RaycastHit hit;
        Vector3 screenPos = thisCam.WorldToScreenPoint(player.transform.position);
        Ray ray = thisCam.ScreenPointToRay(screenPos);
        for (int i = 0; i < multiCam.Length; i++)
        {
            float distance = Vector3.Distance(player.transform.position, multiCam[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance; closest = i;
            }
            foreach (Camera c in multiCam)
            {
                if (c != multiCam[closest])
                {
                    c.enabled = false;
                    c.GetComponent<AudioListener>().enabled = false;
                }
            }
            multiCam[closest].enabled = true;
            multiCam[closest].transform.LookAt(player.transform);
            multiCam[closest].GetComponent<AudioListener>().enabled = true;
        }
        //if (Physics.Raycast(ray,out hit)) {
        //    Transform objectHit = hit.transform;
        //    if (objectHit.tag == "Player")
        //    {
        //        thisCam.enabled = true;
        //        controller.camera = thisCam;

        //    }
        //    else
        //    {
        //        thisCam.enabled = false;
        //    }
        // }

        //if (thisCam.enabled == true)
        //{
    //thisCam.transform.
        //}

        //Debug.DrawLine(transform.position, hit.point, Color.cyan);
    }







}
