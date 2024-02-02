using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public GameObject[] objects;

    public static DontDestroy instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroy dans la scène");
            return;
        }

        instance = this;
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void DestroyObjects()
    {
        foreach (var element in objects)
        {
            Destroy(element);
        }
    }
}
