using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;

namespace Lares.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventory;
        private List<InventoryNode> _inventoryList;

        [Header("Content Containers")]
        [SerializeField] private Transform _consumableContentContainer;
        [SerializeField] private Transform _materialContentContainer;
        [SerializeField] private Transform _keyContentContainers;

        [Header ("Inventory Data")]
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;

        [Header("Buttons")]
        [SerializeField] private GameObject _InventoryItemButtonPrefab;
        [SerializeField] private Button _OnUseButton;
        [SerializeField] private Button _recoveryButton;
        [SerializeField] private Button _materialsButton;
        [SerializeField] private Button _keyButton;

        private InventoryNode _currentInventoryNode = null;

        private void Start()
        {
            LoadInventory();
            InventoryItemButton.ItemSelected += SetCurrentSelected;
            
        }

        private void SetCurrentSelected(InventoryNode item)
        {
            _currentInventoryNode = item;
            _itemName.text = item.Item.ItemData.ItemName;
            _itemDescriptionText.text = item.Item.ItemData.ItemDescription;
        }

        void LoadInventory()
        {
            _inventoryList = new List<InventoryNode>();
            _inventoryList = _inventory.InventoryData;

            GameObject temp;
            foreach (InventoryNode item in _inventoryList)
            {
                temp = Instantiate(_InventoryItemButtonPrefab);
                temp.GetComponent<InventoryItemButton>().ItemData = item;

                if (item.Item is IConsumable)
                {
                    temp.transform.SetParent(_consumableContentContainer);
                }
                else if (item.Item is IMaterial)
                {
                    temp.transform.SetParent(_materialContentContainer);
                }
                else if (item.Item is IKey)
                {
                    temp.transform.SetParent(_keyContentContainers);
                }
            }
        }

        private void ClearInventory()
        {
            for (int i = 0; i < _consumableContentContainer.childCount; i++)
            {
                Destroy(_consumableContentContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < _materialContentContainer.childCount; i++)
            {
                Destroy(_materialContentContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < _keyContentContainers.childCount; i++)
            {
                Destroy(_keyContentContainers.GetChild(i).gameObject);
            }
        }

        public void UpdateItemText(string newName, string newText)
        {
            
        }
    }
}