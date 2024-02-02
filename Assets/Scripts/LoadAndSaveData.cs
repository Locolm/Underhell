using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "lvl3") //le lvl3 (est une extension du 2 et on veux garder la ziq)
        {
            //reset le hasSwitched de l'audio
            AudioManager.instance.hasSwitched = false;
        }

        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        //chargement des quêtes 
        string[] questLines = PlayerPrefs.GetString("Quests", "0,0,0").Split(',');
        for (int i = 0; i < questLines.Length; i++)
            {
                if (questLines[i] != "")
                {
                    int id = int.Parse(questLines[i]);
                    Inventory.instance.QuestProgression.Add(id);
                }
            }

        questLines = PlayerPrefs.GetString("Boss", "0,0,0,0,0,0,0,0,0,0,0,0,0,0").Split(',');
        for (int i = 0; i < questLines.Length; i++)
        {
            if (questLines[i] != "")
            {
                int id = int.Parse(questLines[i]);
                Inventory.instance.BossPrincipaux.Add(id);
            }
        }

        questLines = PlayerPrefs.GetString("Color", "0,0,0,0").Split(',');
        for (int i = 0; i < questLines.Length; i++)
        {
            if (questLines[i] != "")
            {
                int id = int.Parse(questLines[i]);
                Inventory.instance.colorList.Add(id);
            }
        }
        Inventory.instance.colorIn = PlayerPrefs.GetInt("ColorIn", 0);

        questLines = PlayerPrefs.GetString("maskList", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0").Split(',');
        for (int i = 0; i < questLines.Length; i++)
        {
            if (questLines[i] != "")
            {
                int id = int.Parse(questLines[i]);
                Inventory.instance.maskList.Add(id);
            }
        }

 
        int currentHealth = PlayerPrefs.GetInt("Health", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHealth;
        PlayerHealth.instance.healthBar.SetHealth(currentHealth);

        int currentMana = PlayerPrefs.GetInt("Mana", PlayerHealth.instance.maxMana);
        PlayerHealth.instance.currentMana = currentMana;
        PlayerHealth.instance.manaBar.SetMana(currentMana);


        //chargement des items
        string[] itemsSaved = PlayerPrefs.GetString("InventoryItems","").Split(',');
        //Debug.Log("items chargés");
        for (int i=0; i < itemsSaved.Length; i++)
        {
            if (itemsSaved[i]!= "")
            {
            //ajout de l'item à l'inventaire
            //Debug.Log("item chargé : "+itemsSaved[i]);
            int id = int.Parse(itemsSaved[i]);
            Item currentItem = ItemsDataBase.instance.allItems.Single(x => x.id == id);
            Inventory.instance.AddToContent(currentItem);
            //Debug.Log(currentItem.id.ToString());
            }
        }
        int index = PlayerPrefs.GetInt("ContentCurrentIndex", 0);
        Inventory.instance.SetIndexItem(index);
        //Inventory.instance.UpdateInventoryUI();
    }

    public void SaveData()
    {   //Joueur
        SavePlayerData();

        //save nvreached
        int nextlvl = CurrentSceneManager.instance.levelToUnlock;
        if (nextlvl> PlayerPrefs.GetInt("levelReached",1))
        {
            PlayerPrefs.SetInt("levelReached", nextlvl);
        }
        PlayerPrefs.Save();

    }

    public void SavePlayerData()
    {
        Debug.Log("saved");
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
        PlayerPrefs.SetInt("Health", PlayerHealth.instance.currentHealth);
        PlayerPrefs.SetInt("Mana", PlayerHealth.instance.currentMana);
        //save items

        List<Item> itemsList = new List<Item>();
        if (Inventory.instance.content.Count > 0)
        {
            foreach (Inventory.ItemInfo itemInfo in Inventory.instance.content)
            {
                Debug.Log("content no null");
                if (itemInfo.occ>0)
                {
                    for (int i = 0; i < itemInfo.occ; i++)
                    {
                        itemsList.Add(itemInfo.item);
                    }
                }
            }
        }
        string itemsInInventory = string.Join(",", itemsList.Select(x => x.id));
        Debug.Log("Items in inventory :" + itemsInInventory);
        //string itemsInInventory = string.Join(",",Inventory.instance.content.item.Select(x=> x.id));
        //Debug.Log("les items sauvegardés sont : "+itemsInInventory);
        PlayerPrefs.SetInt("ContentCurrentIndex", Inventory.instance.contentCurrentIndex);
        PlayerPrefs.SetString("InventoryItems", itemsInInventory);

        if (Inventory.instance.QuestProgression.Count > 0)
        {
            string questInList = string.Join(",", Inventory.instance.QuestProgression.Select(x => x.ToString()));
            Debug.Log("questprogression"+questInList);
            PlayerPrefs.SetString("Quests", questInList);
        }

        if (Inventory.instance.BossPrincipaux.Count > 0)
        {
            string questInList = string.Join(",", Inventory.instance.BossPrincipaux.Select(x => x.ToString()));
            Debug.Log("questboss" + questInList);
            PlayerPrefs.SetString("Boss", questInList);
        }

        if (Inventory.instance.colorList.Count > 0)
        {
            string questInList = string.Join(",", Inventory.instance.colorList.Select(x => x.ToString()));
            Debug.Log("questcolor" + questInList);
            PlayerPrefs.SetString("Color", questInList);
        }
        PlayerPrefs.SetInt("ColorIn", Inventory.instance.colorIn);

        if (Inventory.instance.maskList.Count > 0)
        {
            string questInList = string.Join(",", Inventory.instance.maskList.Select(x => x.ToString()));
            Debug.Log("maskList" + questInList);
            PlayerPrefs.SetString("maskList", questInList);
        }
    }
}
