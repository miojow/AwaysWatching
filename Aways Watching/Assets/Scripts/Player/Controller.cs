using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    public Camera camera;
    public float Walkspeed = 1.0f;
    public float rotateSpeed = 5.0f;
    public float stamina =100;
    float curSpeed;
     [HideInInspector]
    public bool Running;
    public float runSpeed;
    public int Life = 100;
    private Animator animator;
     [HideInInspector]
    public bool Weapon;
     [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public Inventory inventory;
    public Transform Hand;
     [HideInInspector]
    public int Key = 0;

    public string TriggerName;

    public Transform DropItem; //Criado para jogar itens no chão neste ponto

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        controller = gameObject.GetComponent("CharacterController") as CharacterController;
        animator = GetComponent<Animator>();
        canAttack = true;
    }
    

    void Update()
    {
        #region Movimentação
        if (canAttack)//Se não estiver caminhando ou correndo
        {
            if (controller.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                Running = true;
            }
            else if (controller.velocity.magnitude <= 0 || Input.GetKeyUp(KeyCode.LeftShift))
            {
                Running = false;
            }
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Running && stamina>0)
            {
                curSpeed = runSpeed * Input.GetAxis("Vertical");
                stamina -= (30 * Time.deltaTime);
            }
            else
            {
                curSpeed = Walkspeed * Input.GetAxis("Vertical");
            }

            controller.SimpleMove(forward * curSpeed);
            camera.enabled = true;
            animator.SetFloat("Speed", controller.velocity.magnitude);
        }
        #endregion

        #region Ataque
        if (Input.GetMouseButtonDown(0) && Time.timeScale>0)
        {
            if (EventSystem.current.currentSelectedGameObject == null) // Se não estiver clicando em um botão da GUI
            {
                if (Weapon) // Se o personagem tiver arma
                {
                    if (canAttack)// Se tiver fora do CoolDown
                    {

                        StartCoroutine(Attack(0.61f)); //Tempo da animação
                    }
                }
                //}
            }

        }


        #endregion

        #region RecarregarStamina

        if (!Running)
        {
            if (stamina < 100)
            {
                stamina += (30 * Time.deltaTime);
            }
            if (stamina > 100)
                stamina = 100;
        }
        if (stamina <= 0)
        {
            stamina = 0;
            Input.GetKeyUp(KeyCode.E);
            Running = false;
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

    IEnumerator SendKey(int newKey)
    {
        Key = newKey;
        yield return new WaitForSeconds(1f);
        Key = 0;
    }

    public void UseKey(int newKey)
    {
        StartCoroutine(SendKey(newKey));
    }

    public void OnTriggerStay(Collider Other)
    {
        
        if (Other.tag == "Item")
        {
            Item item = Other.GetComponent<Item>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.addItem(item.itemID);
                Destroy(Other.gameObject);
            }
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Monster")
        {
            hit.gameObject.GetComponent<Monster>().coolDown = true;
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.TransformDirection(Vector3.back), 2f);
        }
    }
}



