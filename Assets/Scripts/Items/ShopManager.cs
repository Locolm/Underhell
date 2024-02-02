using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text nameText;
    public Animator animator;

    public GameObject sellButtonPrefab;
    public Transform sellButtonParent;

    public static ShopManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void OpenShop(Item[] items, string pnjName)
    {
        nameText.text = pnjName;
        UpdateItemsTosell(items);
        animator.SetBool("IsOpen",true);
    }

    public void UpdateItemsTosell(Item[] items)
    {
        //clear les boutons déjà présents
        for (int i = 0; i< sellButtonParent.childCount; i++)
        {
            Destroy(sellButtonParent.GetChild(i).gameObject);
        }
        //refait apparaître les boutons correspondant aux objets du marchand
        for (int i = 0; i< items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab,sellButtonParent);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].price.ToString();
            buttonScript.itemDesc.text = items[i].description;

            buttonScript.item = items[i];
            button.GetComponent<Button>().onClick.AddListener(delegate {buttonScript.BuyItem();});
        }
    }

    public void CloseShop()
    {
        PauseMenu.instance.enabled=true;
        animator.SetBool("IsOpen", false);
    }
}
