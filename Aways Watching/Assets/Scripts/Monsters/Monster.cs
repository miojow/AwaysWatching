using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    [Tooltip("Distancia para seguir Player")]
    public float Distance;
    [Tooltip("Distancia para Enxergar Player")]
    public float lookDist = 10;
    public int Life;
    public float Power;

    public bool stun = false;    //Variavel de Stun
    public bool recover = false; //Previne ficar entrando na rotina de Stun

    public bool coolDown = false;//Variavel de Cooldown de ataque
    public bool InCD = false;//Previne ficar entrando na Cooldown de ataque

    void Update()
    {
        if (stun)
        {
            if (!recover)
            {
                StartCoroutine(RecoverStun(3f));
            }
        }
        if (coolDown)
        {
            if (!InCD)
            {
                StartCoroutine(Wait(1f));
            }

        }
    }


    public IEnumerator RecoverStun(float time)
    {
        recover = true;
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.TransformDirection(Vector3.back), 2);
        yield return new WaitForSeconds(time);
        stun = false;
        recover = false;
    }

    public IEnumerator Wait(float time)
    {
        if (coolDown == true)
        {
            recover = true;
            yield return new WaitForSeconds(time);
            recover = false;
            coolDown = false;
        }
    }


}
