using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lares.Inventory
{
    
    [CreateAssetMenu(fileName = "ScriptableInventory", menuName = "Scriptable Objects/ScriptableInventory")]
    public class ScriptableInventory : ScriptableObject
    {
        public InventoryItem[] inventoryItems;
    }
}
