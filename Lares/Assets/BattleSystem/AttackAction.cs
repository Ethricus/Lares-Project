using System;
using UnityEngine;

namespace Lares.BattleSystem
{
    [System.Serializable]
    public struct AttackData
    {
        public string AttackName;
        public string AttackDesciption;
        public float AttackPower;
        public int AnimationIndex;
        public GameObject AttackObject;
        
        AttackData(string attackName, string attackDescription, float attackPower, int animationIndex, GameObject attackObject)
        {
            AttackName = attackName;
            AttackDesciption = attackDescription;
            AttackPower = attackPower;
            AnimationIndex = animationIndex;
            AttackObject = attackObject;
        }
    }

    public class AttackActions : MonoBehaviour
    {
        protected BattleInfo _battleInfo;
        protected WeaponInfo _weaponInfo;

        public System.Action<AttackData> Attack;

        void Start()
        {
            _battleInfo = GetComponent<BattleInfo>();
        }

        public void SpecialAttack(BattleInfo other, AttackData attack)
        {
            bool isCrit = ((UnityEngine.Random.Range(0, 1000) < (_battleInfo.Luck * _weaponInfo.WeaponLuckModifier) / 1000));
            //get a random number and check if it's over or under 
            AttackInfo newAttack = new AttackInfo(_battleInfo.SAttack, attack.AttackPower, _weaponInfo.WeaponModifier, isCrit, _battleInfo.PAtkCritMod, _weaponInfo.SAtkCritMod);

            other.SpecialAttack(newAttack);

        }
        public void PhysicalAttack() { }
    }
}