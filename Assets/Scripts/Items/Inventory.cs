using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<GameObject> allItemsPrefab = new List<GameObject>();
    public List<Mask> AllMasksStats = new List<Mask>();

    public int nbrObjMax=10;
    public int coinsCount;
    public Text coinsCountText;

    public int contentCurrentIndex = 0;
    public Image itemUIImage;
    public Text itemNameUI;
    public Text itemNomber;

    public Sprite emptyItemImage;

    public PlayerEffect playerEffects;

    public class ItemInfo
    {
        public Item item;
        public int occ;

        public ItemInfo(Item i, int count)
        {
            item = i;
            occ = count;
        }
    }

    public List<ItemInfo> content = new List<ItemInfo>();
    public List<Item> Resources = new List<Item>(); //non consumable object
    public List<int> QuestProgression = new List<int>(); // chaque case correspond à l'avencement d'une quête
    public List<int> BossPrincipaux = new List<int>(); //les 13calamités
    public List<int> colorList = new List<int>(); // les couleurs obtenus et leurs pouvoirs
    public int colorIn; //couleur équipée
    public List<int> maskList = new List<int>(); //Les masques obtenus 1, non obtenu, 0 et équipé 2.
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance Inventory dans la sc�ne");
            return;
        }
        instance = this;
    }

    void Start()
    {
        UpdateInventoryUI();
    }

    /*void Update()
    {
        Debug.Log("currentindex :"+ contentCurrentIndex.ToString()+ " c'est l'objet "+ content[contentCurrentIndex].id.ToString());
    }*/

    public int Mask()
    {
        int id=-1;
        for (int i = 0; i < maskList.Count; i++)
        {
            if (maskList[i] == 2)
            {
                id= i;
            }
        }

        //Mettre à jours les stats en fonction du mask
        //PlayerHealth.instance.Machin=AllMasksStats[id]."enter one of following argument"
        //id
        //name
        //image
        //price
        //desc
        //capawater
        //capacold
        //res
        //degbrut
        //degpourcent
        //tpsglace
        //rayonExplo
        //largeurslash
        //degstones
        //regainitem
        //canuseitem
        //canusemana
        //dashstatus
        //cooldowndash
        //healeffect
        //speedeffect
        //manaeffect
        //manause
        //speed
        //jumphigh
        //canseehp
        //tpsinv
        //positifs[]
        //negatifs[]


        return id;
    }

    public void ConsumeItem()
    {
        //vérifications
        if (content.Count ==0)
        {
            return;
        }
        if (contentCurrentIndex >= content.Count)
            {
                contentCurrentIndex = 0;
            }
        if (contentCurrentIndex < 0 )
            {
                contentCurrentIndex = content.Count -1;
            }
        
        //Debug.Log(contentCurrentIndex.ToString());
        
        ItemInfo selection = content[contentCurrentIndex];

        Item currentItem = selection.item;
        
        if (currentItem.id ==3) //potion d'invulnérabilité
        {
            PlayerHealth.instance.TakeDamage(5); //le joueur subit de faible dégâts afin de déclencher son invulnérabilité
        }
        else{
            PlayerHealth.instance.GainHealth(currentItem.hpGiven);
            playerEffects.AddSpeed(currentItem.speedGiven, currentItem.speedDuration);
        }
        EventSystem.current.SetSelectedGameObject(null); // Lâcher le focus des boutons
        Debug.Log(selection.occ.ToString());
        if (selection.occ<=1)
        {
            GetNextItem();
            content.Remove(selection);
        }
        else
        {
            selection.occ-=1;
        }
        UpdateInventoryUI();
    }

    public void AddToContent(Item item)
    {
        if(IsNotFull())
        {
            if (content.Count==0)
            {
                ItemInfo newItemInfo = new ItemInfo(item, 1);
                content.Add(newItemInfo);
            }
            else 
            {
                if (!research(item))// si research est true l'occ+=1 dans la fonction
                {
                    ItemInfo newItemInfo = new ItemInfo(item, 1);
                    content.Add(newItemInfo);
                }
            }
        }
        else 
        {
            SpawnItemPrefab(item);
        }
    }

    public void SpawnItemPrefab(Item item)
    {
        if (item.id - 1 >= 0 && item.id - 1 < allItemsPrefab.Count)
        {
            GameObject prefab = allItemsPrefab[item.id - 1];
            Vector3 spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("L'ID de l'item est invalide : " + item.id);
        }
    }

    public bool IsNotFull()
    {
        int count =0;
        for(int i = 0; i < content.Count; i++)
        {
            count+=content[i].occ+1;
        }

        if (nbrObjMax>count)
        {
            return true;
        }
        else
        {
            Debug.Log("inventory full");
            return false;
        }
    }

    public bool research(Item item)
    {
        int search = item.id;
        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].item.id == search)
            {
                if (content[i].occ+1<=content[i].item.maxInventory)
                {
                    content[i].occ+=1;
                    return true;
                }
            }
        }
    return false; // Si l'élément n'est pas trouvé

    }

    public void SetIndexItem(int index)
    {
        contentCurrentIndex = index;
        //vérifications
        if (content.Count == 0 || contentCurrentIndex >= content.Count )
        {
            contentCurrentIndex = 0;
        }
        if (contentCurrentIndex < 0)
        {
            contentCurrentIndex = content.Count - 1;
        }
        EventSystem.current.SetSelectedGameObject(null); // Lâcher le focus des boutons
        UpdateInventoryUI();
    }

    public void GetNextItem()
    {
        //vérifications
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if (contentCurrentIndex >= content.Count)
        {
            contentCurrentIndex = 0;
        }
        EventSystem.current.SetSelectedGameObject(null); // Lâcher le focus des boutons
        UpdateInventoryUI();
    }

    public void GetPreviousItem()
    {
        //vérifications
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex--;
        if (contentCurrentIndex < 0)
        {
            contentCurrentIndex = content.Count - 1;
        }
        EventSystem.current.SetSelectedGameObject(null); // Lâcher le focus des boutons
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (content.Count > 0)
        {
            ItemInfo selection = content[contentCurrentIndex];
            Item currentItem = selection.item;
            itemUIImage.sprite = currentItem.image;
            itemNameUI.text = currentItem.name;
            itemNomber.text = CountOccurrences().ToString()+"/"+currentItem.maxInventory.ToString();
        }
        else
        {
            itemUIImage.sprite = emptyItemImage;
            itemNameUI.text = "";
            itemNomber.text = "0";
        }
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUI();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUI();

    }

    public void ResetCoins()
    {
        coinsCount = 0;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }

    /*public void SortContent()
    {
        contentCurrentIndex = 0;
        content.Sort((x, y) => x.id.CompareTo(y.id));
    }*/

    public int CountOccurrences()
    {
        return content[contentCurrentIndex].occ;
    }

    /*bool CheckDistinctIds()
    {
        //content.Sort((x, y) => x.id.CompareTo(y.id));

        for (int i = 0; i < content.Count - 1; i++)
        {
            if (content[i].id != content[i + 1].id)
            {
                return true; // Il existe deux éléments avec des identifiants différents
            }
        }

        return false; // Aucun doublon d'identifiant n'a été trouvé
    }*/

}
