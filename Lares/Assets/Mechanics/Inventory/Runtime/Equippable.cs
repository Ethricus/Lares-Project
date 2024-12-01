 
using UnityEngine;

namespace Mechanics.Inventory
{
    public class Equippable : InventoryItem, IItem
    {
        public float MaxHPModifier = 0;
        public float MaxSPModifier = 0;
        public float EXPGainModifier = 0;
        public float PAtkModifier = 0;
        public float PDefModifier = 0;
        public float SAtkModifier = 0;
        public float SDefModifier = 0;
        public float AgilityModifier = 0;
        public float LuckModifier = 0;
        public float PAtkCritModifier;
        public float SAtkCritModifier;

        public override void OnUse(GameObject playerRef)
        {
        }

        public virtual void OnAdd()
        {
            //remove from inventory
        }

        public virtual void OnRemove()
        {
            //add to inventory
        }
    }

    public class Weapon : Equippable, IWeapon { }

    public class Accessory : Equippable, IAccessory { }
}
