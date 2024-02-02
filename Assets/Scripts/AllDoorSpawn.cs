using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDoorSpawn : MonoBehaviour
{
    public GameObject[] doorSpawn;

    public GameObject joueur;

    public static AllDoorSpawn instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AllDoorSpawn dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        int doorIndex = PlayerPrefs.GetInt("door", 0);

        // Vérifier si l'index est valide
        if (doorIndex < 0 || doorIndex > doorSpawn.Length){
            doorIndex = 0;
        }

        if (doorSpawn.Length>0)
        {
            // Déplacer le joueur à la position du spawn correspondant à l'index
            Transform spawnPoint = doorSpawn[doorIndex].transform;
            joueur.transform.position = spawnPoint.position;
            joueur.transform.rotation = spawnPoint.rotation;
        }
    }

}
