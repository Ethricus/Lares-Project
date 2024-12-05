using System;
using UnityEngine;

namespace Lares.Inventory
{
    public class ObjectID : PropertyAttribute { }

    [Serializable]
    public class InventoryItem : IItem
    {
        [ObjectID] public string Id;
        public string Name;
        public string Description;
        public Sprite Image;

        public virtual void OnUse(GameObject playerRef) { return; }

        public static bool operator ==(InventoryItem a, InventoryItem b) { return a.Id == b.Id; }
        public static bool operator !=(InventoryItem a, InventoryItem b) { return a.Id != b.Id; }

        public override bool Equals(object obj)
        {
            if (obj is null or not InventoryItem) return false;
            return Id == ((InventoryItem)obj).Id;
        }

        public override int GetHashCode() { return this.GetHashCode(); }
    }
}
