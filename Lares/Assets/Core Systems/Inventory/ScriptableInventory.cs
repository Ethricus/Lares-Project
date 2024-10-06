using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lares
{
    [Serializable]
    public struct InventoryItem
    {
        public int itemID;
        public string itemName;
        public string itemDescription;
        public Image itemImage;
        public onUseFunctions onUse;
    }

    [CreateAssetMenu(fileName = "ScriptableInventory", menuName = "Scriptable Objects/ScriptableInventory")]
    public class ScriptableInventory : ScriptableObject
    {
        public InventoryItem[] inventoryItem;
    }
}
