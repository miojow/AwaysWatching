using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    public Camera camera;
    public float Walkspeed = 1.0f;
    public float rotateSpeed = 5.0f;
    public float stamina =100;
    float curSpeed;
    public bool Running;
    public float runSpeed;
    public int Life = 100;
    private Animator animator;
    private bool Weapon;
    private bool canAttack;
    public Inventory inventory;
    public Transform Hand;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
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

    public void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Item")
        {
            Debug.Log("TEM UM ITEM AQUI");
            Item item = Other.GetComponent<Item>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                {//FOR DEBUG ONLY
                    if (item.itemType == Item.ItemType.Weapon)
                    {
                        Other.transform.parent = Hand.transform;
                        Other.transform.position = Hand.transform.position;
                        Other.transform.position = new Vector3(Hand.transform.position.x - 0.212f, Hand.transform.position.y - 0.184f,Hand.transform.position.z +0.002f);
                        Other.transform.rotation = Hand.transform.rotation;
                        Other.transform.rotation = Quaternion.Euler(new Vector3 (Hand.transform.rotation.x + 26484f,Hand.transform.rotation.y+ 258.399f, Hand.transform.rotation.z +60.21963f));
                        Collider[] cols = Other.GetComponents<Collider>();
                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (cols[i].isTrigger == true)
                            {
                                cols[i].enabled = false;
                            }
                        }
                    }
                }
                inventory.addItem(item.itemID);
                //Destroy(Other.gameObject);
            }
        }
    }
}



