using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    GameObject[] cam;
    public List<Camera> Cameras = new List<Camera>();
	// Use this for initialization
	void Start () {
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
                c.enabled = true;
            }
            else
                c.enabled = false;
        }

    }

}
