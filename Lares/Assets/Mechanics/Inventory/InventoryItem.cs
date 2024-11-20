using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Lares.Inventory
{
    public class ObjectID : PropertyAttribute { }

    [Serializable]
    public struct InventoryData
    {
        [ObjectID] public string ItemID;
        public string itemName;
        public string itemDescription;
        public UnityEngine.UI.Image itemImage;
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

        public static bool operator == (InventoryItem a, InventoryItem b) { return a.ItemData.ItemID == b.ItemData.ItemID; }
        public static bool operator != (InventoryItem a, InventoryItem b) { return a.ItemData.ItemID != b.ItemData.ItemID; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            if (!(obj is InventoryItem)) return false;
            return ItemData.ItemID == (obj as InventoryItem).ItemData.ItemID;
        }

        public override int GetHashCode() { return ItemData.GetHashCode(); }
    }

    public class HealthPotion : InventoryItem
    {
        [SerializeField] private int HealthPotionParameterTest;
        public override void OnUse()
        {
            Debug.Log("Health potion used");
        }
    }

    public class MagicPotion : InventoryItem
    {
        [SerializeField] private int MagicPotionParameterTest;
        public override void OnUse() { Debug.Log("Magic Potion used!"); }
    }

}
