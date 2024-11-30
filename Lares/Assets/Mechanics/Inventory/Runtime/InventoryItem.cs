using System;
using UnityEngine;

namespace Lares.Inventory
{
    public class ObjectID : PropertyAttribute { }

    [Serializable]
    public class InventoryItem : IItem
    {
        [ObjectID] public string ItemId;
        public string ItemName;
        public string ItemDescription;
        public Sprite ItemImage;

        public virtual void OnUse(GameObject playerRef) { return; }

        public static bool operator ==(InventoryItem a, InventoryItem b) { return a.ItemId == b.ItemId; }
        public static bool operator !=(InventoryItem a, InventoryItem b) { return a.ItemId != b.ItemId; }

        public override bool Equals(object obj)
        {
            if (obj is null or not InventoryItem) return false;
            return ItemId == ((InventoryItem)obj).ItemId;
        }

        public override int GetHashCode() { return this.GetHashCode(); }
    }
}
