using UnityEngine;

namespace Mechanics.Inventory
{
    public interface IItem { public void OnUse(GameObject playerRef); }
    public interface IConsumable : IItem { }
    public interface IMaterial : IItem { }
    public interface IAccessory : IItem { }
    public interface IWeapon : IItem { }
    public interface IKey : IItem { }
}
