using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Camera thisCam;
    public GameObject player;

    void Awake()
    {
        thisCam = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        thisCam.enabled = false;
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
                thisCam.enabled = true;
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
