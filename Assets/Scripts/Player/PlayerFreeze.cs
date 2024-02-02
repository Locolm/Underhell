using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    private bool isFrozen = false;

    public static PlayerFreeze instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerFreeze dans la sc�ne");
            return;
        }

        instance = this;
        FreezePlayer(2f);
    }

    public void FreezePlayer(float duration)
    {
        if (!isFrozen)
        {
            isFrozen = true;
            StartCoroutine(FreezeRoutine(duration));
        }
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        // Bloquer le mouvement du joueur
        PlayerMove.instance.enabled = false;

        // Attendre la dur�e de cong�lation
        yield return new WaitForSeconds(duration);

        // D�bloquer le mouvement du joueur
        PlayerMove.instance.enabled = true;
        isFrozen = false;
    }
}
