using UnityEngine;
using System.Collections;

public class CameraZone : MonoBehaviour {

    private CameraController cameraController;
    public Camera thisCam;
    public Camera RedCam;
    public Transform Player;
    private Quaternion myRotation;
    private Vector3 myPosition;

	// Use this for initialization
	void Awake () {
        cameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
        thisCam = gameObject.GetComponentInParent<Camera>();
        myRotation = transform.rotation;
        myPosition = transform.position;
	}

    void Update()
    {
        if (thisCam.enabled == true)
        {
            thisCam.transform.LookAt(Player.transform.position);
            RedCam.enabled = true;
        }
        else
        {
            thisCam.transform.LookAt(null);
            RedCam.enabled = false;
            
        }
    }

    void LateUpdate()
    {
        transform.rotation = myRotation;
        transform.position = myPosition;
    }


    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            cameraController.enableMyCamera(thisCam);
            Player = Other.transform;
        }
    }

}
