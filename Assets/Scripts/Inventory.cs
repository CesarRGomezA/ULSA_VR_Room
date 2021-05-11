using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Item> items;
    [SerializeField]
    List<Image> slots;

    void Start()
    {
        UpdateInventory();
    }

    void UpdateInventory()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].sprite = items[0].Icon;
        }
    }
}
