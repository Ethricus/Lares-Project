using UnityEngine;

namespace Lares.Inventory
{
    public class HealthConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _percentageIncrease;
        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Health potion used");
        }
    }

    public class MagicConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _percentageIncrease;
        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Magic potion used");
        }
    }

    public class PhysicalAttackConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _PAtkModifier;
        [SerializeField] private float _timeInEffect;

        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Physical Attack potion used");
        }
    }

    public class PhysicalDefenseConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _PDefModifier;
        [SerializeField] private float _timeInEffect;

        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Physical Defense potion used");
        }
    }

    public class MagicAttackConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _SAtkModifier;
        [SerializeField] private float _timeInEffect;

        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Magic Attack potion used");
        }
    }

    public class MagicDefenseConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _SDefModifier;
        [SerializeField] private float _timeInEffect;
        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Magic Defense potion used");
        }
    }

    public class AgilityConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _AgilityModifier;
        [SerializeField] private float _timeInEffect;
        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Agility potion used");
        }
    }

    public class LuckConsumable : InventoryItem, IConsumable
    {
        [SerializeField] private int _LuckModifier;
        [SerializeField] private float _timeInEffect;
        public override void OnUse(GameObject playerRef)
        {
            Debug.Log("Luck potion used");
        }
    }
}
