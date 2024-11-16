using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lares.Inventory
{
    public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Inventory.Pair<InventoryItem, int> itemData;
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Image image;
        public InventoryUIController inventoryUIController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            image.sprite = itemData.Item1.itemSprite;
            numberText.text = itemData.Item2.ToString();

        }

        //Detect if the Cursor starts to pass over the GameObject
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            //Output to console the GameObject's name and the following message
            inventoryUIController.UpdateItemText(itemData.Item1.itemName, itemData.Item1.itemDescription);
        }

        //Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            //Output the following message with the GameObject's name
            Debug.Log("Cursor Exiting " + name + " GameObject");
        }
    }
}
