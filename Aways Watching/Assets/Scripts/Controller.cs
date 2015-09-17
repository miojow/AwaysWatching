using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    public Camera camera;
    public float speed = 3.0f;
    public float rotateSpeed = 5.0f;
    public int Life = 100;
    private Animator animator;
    private bool Weapon;
    private bool canAttack;

    void Awake()
    {
        controller = gameObject.GetComponent("CharacterController") as CharacterController;
        animator = GetComponent<Animator>();
        canAttack = true;
        Weapon = true;
    }

    void Update()
    {
        #region Movimentação
        if (canAttack)//Se não estiver caminhando ou correndo
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);
            camera.enabled = true;
            animator.SetFloat("Speed", controller.velocity.magnitude);
        }
        #endregion

        #region Ataque
        if (Input.GetMouseButtonDown(0))
        {
            if (Weapon) // Se o personagem tiver arma
            {
                if (canAttack)// Se tiver fora do CoolDown
                {
                    
                    StartCoroutine(Attack(0.41f)); //Tempo da animação
                }
            }
        }


        #endregion



    }

    IEnumerator Attack(float time)
    {
        canAttack = false;
        animator.SetBool("Attack",true);
        yield return new WaitForSeconds(time);
        animator.SetBool("Attack", false);
        canAttack = true;
    }
}



