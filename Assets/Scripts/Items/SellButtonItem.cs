using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;
    public Text itemPrice;
    public Text itemDesc;

    public Item item;

    public void BuyItem()
    {
        Inventory inventory = Inventory.instance;
        if (inventory.coinsCount >= item.price)
        {
            inventory.AddToContent(item);
            inventory.UpdateInventoryUI();
            inventory.coinsCount -= item.price;
            inventory.UpdateTextUI();
        }
    }

}
