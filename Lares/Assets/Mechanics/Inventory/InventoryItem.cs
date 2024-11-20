using System;
using Unity.VisualScripting;
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
        public UnityEngine.UI.Image ItemImage;
    }

    public interface IItem
    {
        public void OnUse();
    }

    [Serializable]
    public class InventoryItem : IItem
    {
        public InventoryData ItemData;
        public virtual void OnUse() { return; }

        public static bool operator == (InventoryItem a, InventoryItem b) { return a.ItemData.ItemId == b.ItemData.ItemId; }
        public static bool operator != (InventoryItem a, InventoryItem b) { return a.ItemData.ItemId != b.ItemData.ItemId; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            if (!(obj is InventoryItem)) return false;
            return ItemData.ItemId == (obj as InventoryItem).ItemData.ItemId;
        }

        public override int GetHashCode() { return ItemData.GetHashCode(); }
    }

    public class HealthPotion : InventoryItem
    {
        [SerializeField] private int _healthPotionParameterTest;
        public override void OnUse()
        {
            Debug.Log("Health potion used");
        }
    }

    public class MagicPotion : InventoryItem
    {
        [SerializeField] private int _magicPotionParameterTest;
        public override void OnUse() { Debug.Log("Magic Potion used!"); }
    }
}
