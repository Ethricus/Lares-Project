using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Lares.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        private List<Inventory.Pair<InventoryItem, int>> _inventoryList;
        
        [Header("Content Containers")]
        [SerializeField] private Transform _recoveryContentContainer;
        [SerializeField] private Transform _materialsContentContainer;
        [SerializeField] private Transform _keyContentContainers;

        [Header("Inventory Data")]
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;

        [Header("Buttons")]
        [SerializeField] private GameObject _InventoryItemButtonPrefab;
        [SerializeField] private Button _recoveryButton;
        [SerializeField] private Button _materialsButton;
        [SerializeField] private Button _keyButton;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LoadInventory();
            _recoveryButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.RECOVERY));
            _materialsButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.MATERIAL));
            _keyButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.KEY));
        }

        private void OnEnable()
        {
            LoadInventory();
            _recoveryButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.RECOVERY));
            _materialsButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.MATERIAL));
            _keyButton.onClick.AddListener(() => ChangeInventoryView(ItemTypes.KEY));
        }

        private void ChangeInventoryView(ItemTypes itemType)
        {
            LoadInventory();
            switch (itemType)
            {
                case ItemTypes.RECOVERY:
                    _recoveryContentContainer.gameObject.SetActive(true);
                    _materialsContentContainer.gameObject.SetActive(false);
                    _keyContentContainers.gameObject.SetActive(false);
                    break;
                case ItemTypes.MATERIAL:
                    _recoveryContentContainer.gameObject.SetActive(false);
                    _materialsContentContainer.gameObject.SetActive(true);
                    _keyContentContainers.gameObject.SetActive(false);
                    break;
                case ItemTypes.KEY:
                    _recoveryContentContainer.gameObject.SetActive(false);
                    _materialsContentContainer.gameObject.SetActive(false);
                    _keyContentContainers.gameObject.SetActive(true);
                    break;
            }
        }

        public void LoadInventory()
        {
            _inventoryList = _inventory.ReturnInventoryAsInventoryItems();
            ClearInventory();

            GameObject temp;
            foreach (Inventory.Pair<InventoryItem, int> data in _inventoryList)
            {
                temp = Instantiate(_InventoryItemButtonPrefab);
                temp.GetComponent<InventoryItemButton>().itemData = data;
                temp.GetComponent<InventoryItemButton>().inventoryUIController = this;

                switch (data.Item1.itemType)
                {
                    case ItemTypes.RECOVERY:
                        temp.transform.SetParent(_recoveryContentContainer);
                        break;
                    case ItemTypes.MATERIAL:
                        temp.transform.SetParent(_materialsContentContainer);
                        break;
                    case ItemTypes.KEY:
                        temp.transform.SetParent(_keyContentContainers);
                        break;
                }
            }
        }

        private void ClearInventory()
        {
            for (int i = 0; i < _recoveryContentContainer.childCount; i++)
            {
                Destroy(_recoveryContentContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < _materialsContentContainer.childCount; i++)
            {
                Destroy(_materialsContentContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < _keyContentContainers.childCount; i++)
            {
                Destroy(_keyContentContainers.GetChild(i).gameObject);
            }
        }

        public void UpdateItemText(string newName, string newText)
        {
            _itemName.text = newName;
            _itemDescriptionText.text = newText;
        }
    }
}
