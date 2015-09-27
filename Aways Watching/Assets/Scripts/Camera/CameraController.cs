using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    GameObject[] cam;
    GameObject[] redCam;
    public List<Camera> Cameras = new List<Camera>();
    public List<Camera> RedCameras = new List<Camera>();
    public Controller controller;


	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        cam =GameObject.FindGameObjectsWithTag("Camera");
        redCam = GameObject.FindGameObjectsWithTag("RedCamera");
        for (int i = 0; i < cam.Length; i++)
        {
            Cameras.Add(cam[i].GetComponent<Camera>());
        }
        for (int i = 0; i < redCam.Length; i++)
        {
            RedCameras.Add(redCam[i].GetComponent<Camera>());
        }
	}

    public void enableMyCamera(Camera camera)
    {
        foreach (Camera c in Cameras)
        {
            if (c.name == camera.name)
            {
                if(controller.camera == null || controller.camera.name != c.name)
                {
                    c.enabled = true;
                    controller.camera = c;
                }
            }
            else if (c.name != camera.name)
            {
                c.enabled = false;
            }
        }
    }

    public void enableMyRedCam(Camera redCam)
    {   
        foreach (Camera c2 in RedCameras)
        {
            if (c2.name == redCam.name)
            {
                if (controller.camera.name != c2.name)
                {
                    c2.enabled = true;
                    controller.camera = c2;
                }

            }
            else if (c2.name != redCam.name)
            {
                c2.enabled = false;
            }
        }
    }



}
