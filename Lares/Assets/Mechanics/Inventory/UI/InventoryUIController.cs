using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

namespace Lares.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventory;
        private List<InventoryNode> _inventoryList;

        [Header("Scroll Views")]
        [SerializeField] private Transform _consumableScrollView;
        private Transform _consumableContentHolder;
        [SerializeField] private Transform _materialScrollView;
        private Transform _materialContentHolder;
        [SerializeField] private Transform _keyScrollView;
        private Transform _keyContentHolder;

        [Header ("Inventory Data")]
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;

        [Header("Buttons")]
        [SerializeField] private GameObject _inventoryItemButtonPrefab;
        [SerializeField] private Button _onUseButton;
        [SerializeField] private Button _consumableButton;
        [SerializeField] private Button _materialsButton;
        [SerializeField] private Button _keyButton;

        private InventoryNode _currentInventoryNode = null;

        private void Start()
        {
            InventoryItemButton.ItemSelected += SetCurrentSelected;
            _onUseButton.onClick.AddListener(CallOnUseFunction);
            _consumableButton.onClick.AddListener(SetToConsumableView) ;
            _materialsButton.onClick.AddListener(SetToMaterialView);
            _keyButton.onClick.AddListener(SetToKeyView);
            _consumableContentHolder = _consumableScrollView.GetComponent<ScrollRect>().content.transform;
            _materialContentHolder = _materialScrollView.GetComponent<ScrollRect>().content.transform;
            _keyContentHolder = _keyScrollView.GetComponent<ScrollRect>().content.transform;

            LoadInventory();
        }

        private void SetToConsumableView()
        {
            _consumableScrollView.gameObject.SetActive(true);
            _materialScrollView.gameObject.SetActive(false);
            _keyScrollView.gameObject.SetActive(false);
        }

        private void SetToMaterialView()
        {
            _consumableScrollView.gameObject.SetActive(false);
            _materialScrollView.gameObject.SetActive(true);
            _keyScrollView.gameObject.SetActive(false);
        }

        private void SetToKeyView()
        {
            _consumableScrollView.gameObject.SetActive(false);
            _materialScrollView.gameObject.SetActive(false);
            _keyScrollView.gameObject.SetActive(true);
        }

        private void SetCurrentSelected(InventoryNode item)
        {
            _currentInventoryNode = item;
            _itemName.text = item.Item.ItemData.ItemName;
            _itemDescriptionText.text = item.Item.ItemData.ItemDescription;
        }

        private void CallOnUseFunction()
        {
            Debug.Log("On Use button clicked");
            _currentInventoryNode.Item.OnUse(_inventory.PlayerReference);
        }

        void LoadInventory()
        {
            _inventoryList = _inventory.InventoryData;
            
            GameObject temp;
            foreach (InventoryNode item in _inventoryList)
            {
                temp = Instantiate(_inventoryItemButtonPrefab);
                temp.GetComponent<InventoryItemButton>().ItemData = item;

                switch (item.Item)
                {
                    case IConsumable:
                        temp.transform.SetParent(_consumableContentHolder);
                        break;
                    case IMaterial:
                        temp.transform.SetParent(_materialContentHolder);
                        break;
                    case IKey:
                        temp.transform.SetParent(_keyContentHolder);
                        break;
                }
            }
        }

        private void ClearInventory()
        {
            for (int i = 0; i < _consumableScrollView.childCount; i++)
            {
                Destroy(_consumableScrollView.GetChild(i).gameObject);
            }
            for (int i = 0; i < _materialScrollView.childCount; i++)
            {
                Destroy(_materialScrollView.GetChild(i).gameObject);
            }
            for (int i = 0; i < _keyScrollView.childCount; i++)
            {
                Destroy(_keyScrollView.GetChild(i).gameObject);
            }
        }
    }
}