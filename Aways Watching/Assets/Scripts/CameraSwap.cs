using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public GameObject player;
    public Controller controller;

    void Awake()
    {
        thisCam = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        thisCam.enabled = false;
        controller = player.GetComponent<Controller>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 screenPos = thisCam.WorldToScreenPoint(player.transform.position);
        Ray ray = thisCam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray,out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Player")
            {
                thisCam.enabled = true;
                controller.camera = thisCam;

            }
            else
            {
                thisCam.enabled = false;
            }
         }

        if (thisCam.enabled == true)
        {
            thisCam.transform.LookAt(player.transform);
        }

        Debug.DrawLine(transform.position, hit.point, Color.cyan);
    }







}
