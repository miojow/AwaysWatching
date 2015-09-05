using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public CharacterController CharacterController;
    public Camera camera;
    public float speed = 3.0f;
    public float rotateSpeed = 5.0f;
    public int Life = 100;

    void Awake()
    {
        CharacterController = gameObject.GetComponent("CharacterController") as CharacterController;
    }

    void Update()
    {
        //Rotaciona o Eixo Y
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed,0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        CharacterController.SimpleMove(forward * curSpeed);
        camera.enabled = true;
    }
}



