using System;
using UnityEngine;

namespace Lares.Inventory
{
    public interface IItem
    {
        public void OnUse();
    }

    [Serializable]
    public class InventoryItem : IItem
    {
        [SerializeField] public InventoryData data;
        public virtual void OnUse() { return; }
    }

    public class HealthPotion : InventoryItem
    {
        [SerializeField] int HealthPotionParameterTest;
        public override void OnUse()
        {
            Debug.Log("Health potion used");
        }
    }

    public class MagicPotion : InventoryItem
    {
        [SerializeField] int MagicPotionParameterTest;
        public override void OnUse() { Debug.Log("Magic Potion used!"); }
    }

}
