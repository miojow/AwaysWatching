using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    GameObject[] cam;
    public List<Camera> Cameras = new List<Camera>();
    public Controller controller;


	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        cam =GameObject.FindGameObjectsWithTag("Camera");
        for (int i = 0; i < cam.Length; i++)
        {
            Cameras.Add(cam[i].GetComponent<Camera>());
        }
	}

    public void enableMyCamera(Camera camera)
    {
        foreach (Camera c in Cameras)
        {
            if (c.name == camera.name)
            {
                if (controller.camera.name != c.name)
                {
                    if (c.GetComponent<CameraSwap>().dist <= controller.camera.GetComponent<CameraSwap>().dist)
                    {
                        c.enabled = true;
                        controller.camera = c;
                    }
                }
            }
            else if (c.name != camera.name)
            {
                c.enabled = false;
                CameraSwap s = c.GetComponent<CameraSwap>();
                s.resetPlayer();
            }
        }

    }



}
