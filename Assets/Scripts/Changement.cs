using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changement : MonoBehaviour
{
    public int QuestBossColor = 0; // 0 quest | 1 boss | 2 color ou autre
    public int changementType = 0; // 0 modification brut, 1 modification ajout, 2 modification retrait
    public int id = 0; // id de modification
    public int val = 1; // valeur de modification
    public bool doesDestroy = true; // Si vrai, le GameObject sera détruit

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (QuestBossColor)
            {
                case 0: // QuestProgression
                    ModifyValue(Inventory.instance.QuestProgression, changementType, id, val);
                    break;
                case 1: // BossPrincipaux
                    ModifyValue(Inventory.instance.BossPrincipaux, changementType, id, val);
                    break;
                case 2: // colorList
                    ModifyValue(Inventory.instance.colorList, changementType, id, val);
                    break;
                default:
                    Debug.LogError("QuestBossColor incorrect.");
                    break;
            }
        }
        Debug.Log("Valeur actuelle de QuestProgression[" + id + "] : " + Inventory.instance.QuestProgression[id]);
    }

    private void ModifyValue(List<int> list, int type, int index, int value)
    {
        switch (type)
        {
            case 0: // Modification brut
                list[index] = value;
                Debug.Log("Modification brute de " + list + "[" + index + "] : " + value);
                break;
            case 1: // Ajout
                list[index] += value;
                Debug.Log("Ajout de " + value + " à " + list + "[" + index + "] : " + list[index]);
                break;
            case 2: // Retrait
                list[index] -= value;
                Debug.Log("Retrait de " + value + " de " + list + "[" + index + "] : " + list[index]);
                break;
            default:
                Debug.LogError("ChangementType incorrect.");
                break;
        }
        if (doesDestroy)
        {
            Destroy(gameObject); // Détruit le GameObject après avoir exécuté le code du OnTriggerEnter2D
        }
    }
}
