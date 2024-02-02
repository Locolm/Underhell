using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    public int occ;

    public ItemInfo(Item i, int count)
    {
        item = i;
        occ = count;
    }
}
