using Lares.Inventory;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lares.Inventory
{
    public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler
    {
        public InventoryNode Node;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _numberText;

        public static System.Action<InventoryNode> ItemSelected;

        void Start()
        {
            if (Node is null) { Debug.Log("item data is null!"); }
            _itemName.text = Node.Item.Name;
            _numberText.text = Node.ItemCount.ToString();
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            ItemSelected.Invoke(Node);
        }
    }
}
