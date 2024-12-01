using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Inventory
{
    [CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Scriptable Objects/InventoryDatabase")]
    public class ScriptableInventory : ScriptableObject
    {
        [SerializeReference] public List<InventoryItem> ItemList = new List<InventoryItem>();
    }
}
