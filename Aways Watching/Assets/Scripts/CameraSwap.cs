using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public GameObject player;
    private Controller Controller;

    void Awake()
    {
        thisCam = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 screenPos = thisCam.WorldToScreenPoint(player.transform.position);
        Ray ray = thisCam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray,out hit)) {
            Transform objectHit = hit.transform;
            Debug.Log(hit.collider.name);
            if (objectHit.tag == "Player")
            {
                Debug.Log("ACHOU O PLAYER NA CAMERA: "+thisCam.transform.name);
                Controller = objectHit.gameObject.GetComponent<Controller>();
                if ((Controller.camera  == null) || (Controller.camera.transform.position != this.transform.position))
                {
                    if (Controller.camera  != null)
                        Controller.camera.enabled = false;
                    thisCam.enabled = true;
                    Controller.camera = thisCam;
                }
            }
         }

        if (thisCam.enabled == true)
        {
            thisCam.transform.LookAt(player.transform);
        }

        Debug.DrawLine(transform.position, hit.point, Color.cyan);
    }







}
