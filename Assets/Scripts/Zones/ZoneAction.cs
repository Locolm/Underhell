using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAction : MonoBehaviour
{
    public GameObject objectDestroy;
    public List<GameObject> debris;
    public bool isShaking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(objectDestroy);
            
            foreach (GameObject obj in debris)
            {
                obj.SetActive(true);
            }
            if(isShaking)
            {
                CameraShake.Instance.ShakeCamera(40.0f, 1.0f);
            }
            Destroy(gameObject);
        }
    }
}

