using UnityEngine;

namespace Lares.BattleSystem
{
    public struct AttackData
    {
        public float AttackPower;
        // animationindex
        // particle effect 
        // behaviour script 
    }

    public class AttackActions : MonoBehaviour
    {
        protected BattleInfo _battleInfo;
        protected WeaponInfo _weaponInfo;

        void Start()
        {
            _battleInfo = GetComponent<BattleInfo>();
        }

        public void SpecialAttack(BattleInfo other, AttackData attack)
        {
            bool isCrit = ((Random.Range(0, 1000) < (_battleInfo.Luck * _weaponInfo.WeaponLuckModifier) / 1000));
            //get a random number and check if it's over or under 
            AttackInfo newAttack = new AttackInfo(_battleInfo.SAttack, attack.AttackPower, _weaponInfo.WeaponModifier, isCrit, _battleInfo.PAtkCritMod, _weaponInfo.SAtkCritMod);

            other.SpecialAttack(newAttack);

        }
        public void PhysicalAttack() { }
    }
}