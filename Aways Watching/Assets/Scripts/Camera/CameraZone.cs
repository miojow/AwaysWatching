using UnityEngine;
using System.Collections;

public class CameraZone : MonoBehaviour {

    private CameraController cameraController;
    private Camera thisCam;
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
        }
        else
        {
            thisCam.transform.LookAt(null);
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
