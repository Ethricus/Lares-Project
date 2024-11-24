using Lares.Inventory;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lares.Inventory
{
    public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public InventoryNode ItemData = null;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _numberText;

        public static System.Action<InventoryNode> ItemSelected;

        void Start()
        {
            if (ItemData == null) { Debug.Log("item data is null!"); }
            _itemName.text = ItemData.Item.ItemData.ItemName;
            _numberText.text = ItemData.ItemCount.ToString();
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            //Output to console the GameObject's name and the following message
            //InventoryUIController.UpdateItemText(ItemData.Item.ItemData.ItemName, ItemData.Item.ItemData.ItemDescription);
            ItemSelected.Invoke(ItemData);
        }

        //Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            //Output the following message with the GameObject's name
            Debug.Log("Cursor Exiting " + name + " GameObject");
        }
    }
}
