using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Lares.Inventory
{
    [Serializable]
    public struct InventoryData
    {
        [ObjectID] public int ItemID;
        public string itemName;
        public string itemDescription;
        public UnityEngine.UI.Image itemImage;
    }

    [CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Scriptable Objects/InventoryDatabase")]
    public class ScriptableInventory : ScriptableObject
    {
        [SerializeReference] public List<InventoryItem> ItemList = new List<InventoryItem>();

    }

    public class ObjectID : PropertyAttribute { }
}
