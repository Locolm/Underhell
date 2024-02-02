using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicActivation : MonoBehaviour
{
    public GameObject[] obj = new GameObject[2];

    public int isAButton = 0; 
    // 0 =no, 1 = se désactive dès que touché et change de look

    public bool isAMechanism = true;
    public bool openDoor = true;
    public int doDirectdamage = 0;
    //<0 no, else = damage du trap
    public bool isReactiveToAnything = false;
    public bool MakeShake;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool used = false;
        if (collision.gameObject.CompareTag("MechanismA") && isAMechanism)
        {
            // Code à exécuter lorsque l'objet avec le tag "MechanicA" entre en collision
            Debug.Log("L'objet avec le tag 'MechanismA' a touché le collider!");
            obj[0].SetActive(false);
            obj[1].SetActive(true);
            used = true;
        } 
        
        if (collision.gameObject.CompareTag("Player") && doDirectdamage > 0)
        {
            PlayerHealth.instance.TakeDamage(doDirectdamage);
            used = true;

        }

        if (collision.gameObject.CompareTag("Player") && isAButton>0)
        {
            if (isAButton == 1)
            {
                Debug.Log("tu m'as touché");
                obj[0].SetActive(false);
                obj[1].SetActive(true);
                used = true;
            }
        }

        if (isReactiveToAnything)
        {
            if (isAButton == 1)
            {
                Debug.Log("tu m'as touché");
                obj[0].SetActive(false);
                obj[1].SetActive(true);
                used = true;
            }
        }
        if(MakeShake && used)
        {
            CameraShake.Instance.ShakeCamera(10.0f, 2.0f);
            Destroy(gameObject);
        }
        else
        {
            if (used)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool used = false;
        if (collision.CompareTag("MechanismA") && isAMechanism)
        {
            // Code à exécuter lorsque l'objet avec le tag "MechanicA" entre en collision
            Debug.Log("L'objet avec le tag 'MechanicA' a touché le collider!");
            obj[0].SetActive(false);
            obj[1].SetActive(true);
            used = true;
        }

        if (collision.CompareTag("Player") && doDirectdamage > 0)
        {
            PlayerHealth.instance.TakeDamage(doDirectdamage);
            used = true;
        }

        if (collision.CompareTag("Player") && isAButton>0)
        {
            if (isAButton == 1)
            {
                Debug.Log("tu m'as touché");
                obj[0].SetActive(false);
                obj[1].SetActive(true);
                used = true;
            }
        }

        if (isReactiveToAnything)
        {
            if (isAButton == 1)
            {
                Debug.Log("tu m'as touché");
                obj[0].SetActive(false);
                obj[1].SetActive(true);
                used = true;
            }
        }
        if (MakeShake&& used)
        {
            CameraShake.Instance.ShakeCamera(10.0f, 2.0f);
            Destroy(gameObject);
        }
        else
        {
            if (used)
            {
                Destroy(gameObject);
            }
        }

    }
}
