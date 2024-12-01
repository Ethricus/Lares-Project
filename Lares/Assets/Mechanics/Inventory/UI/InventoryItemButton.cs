 
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mechanics.Inventory
{
    public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler
    {
        public InventoryNode ItemData;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _numberText;

        public static System.Action<InventoryNode> ItemSelected;

        void Start()
        {
            if (ItemData is null) { Debug.Log("item data is null!"); }
            _itemName.text = ItemData.Item.ItemName;
            _numberText.text = ItemData.ItemCount.ToString();
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            ItemSelected.Invoke(ItemData);
        }
    }
}
