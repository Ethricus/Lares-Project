using System;
using UnityEngine;

namespace Lares.Inventory
{
    public class ObjectID : PropertyAttribute { }

    [Serializable]
    public struct InventoryData
    {
        [ObjectID] public string ItemId;
        public string ItemName;
        public string ItemDescription;
        public Sprite ItemImage;
    }

    [Serializable]
    public class InventoryItem : IItem
    {
        public InventoryData ItemData;
        
        public virtual void OnUse(GameObject playerRef) { return; }

        public static bool operator ==(InventoryItem a, InventoryItem b) { return a.ItemData.ItemId == b.ItemData.ItemId; }
        public static bool operator !=(InventoryItem a, InventoryItem b) { return a.ItemData.ItemId != b.ItemData.ItemId; }

        public override bool Equals(object obj)
        {
            if (obj is null or not InventoryItem) return false;
            return ItemData.ItemId == ((InventoryItem)obj).ItemData.ItemId;
        }

        public override int GetHashCode() { return ItemData.GetHashCode(); }
    }
}
