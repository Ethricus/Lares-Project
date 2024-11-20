using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lares.Inventory
{
    [CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Scriptable Objects/InventoryDatabase")]
    public class ScriptableInventory : ScriptableObject
    {
        [SerializeReference] public List<InventoryItem> ItemList = new List<InventoryItem>();
    }

   
}
