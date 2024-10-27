using System.Collections;
using UnityEngine;

namespace Lares.BattleSystem
{

    public struct AttackInfo
    {
        public float AttackValue { get; private set; } // the attackers physical attack or magic attack stat
        public float AttackPower { get; private set; } //the power of the attack
        public float WeaponModifier { get; private set; } // physical or seri modifier of the equipped weapon 
        public bool Crit { get; private set; }
        public float AttackCritMod { get; private set; }
        public float WeaponCritMod { get; private set; }

        public AttackInfo(float attackValue, float attackPower, float weaponModifier, bool crit, float attackCritMod, float weaponCritMod)
        {
            AttackValue = attackValue;
            AttackPower = attackPower;
            WeaponModifier = weaponModifier;
            Crit = crit;
            AttackCritMod = attackCritMod;
            WeaponCritMod = weaponCritMod;
        }
    }

    public class BattleInfo : MonoBehaviour
    {
        public int Level { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int SP { get; private set; }
        public int MaxSP { get; private set; }
        public int EXP { get; private set; }
        public float PAttack { get; private set; }
        public float PDefense { get; private set; }
        public float SAttack { get; private set; }
        public float SDefense { get; private set; }
        public float Agility { get; private set; }
        public float Luck { get; private set; }
        public float PAtkCritMod { get; private set; }
        public float SAtkCritMod { get; private set; }

        public System.Action DeathEvent;

        public void SpecialAttack(AttackInfo attack)
        {
            float damage = ((2f * attack.AttackValue) - SDefense) * attack.AttackPower * (attack.WeaponModifier / 10f);
            damage = damage / 100;

            if (attack.Crit)
            {
                float critMod = ((attack.AttackCritMod + attack.WeaponCritMod) / 100) + 1;
                damage *= critMod;
            }
            DecreaseHP(damage);
        }

        public void PhysicalAttack(AttackInfo attack)
        {
            float damage = ((2 * attack.AttackValue) - PDefense) * attack.AttackPower * (attack.WeaponModifier / 10);
            damage = damage / 100;

            if (attack.Crit)
            {
                float critMod = ((attack.AttackCritMod + attack.WeaponCritMod) / 100) + 1;
                damage *= critMod;
            }
            DecreaseHP(damage);

        }

        protected void DecreaseHP(float val)
        {
            // check HP conditions and rounding 
            if ((HP - Mathf.Floor(val)) < 0)
            {
                DeathEvent?.Invoke();
                HP = 0;
                return;
            }
            HP -= (int)Mathf.Floor(val);
        }

        public void IncreaseHP(float val)
        {
            // check HP conditions and rounding
            if (HP + Mathf.Round(val) > MaxHP)
            {
                HP = MaxHP; return;
            }
            HP += (int)Mathf.Round(val);
        }

        public void IncreaseEXP()
        {
            //if (level up conditions met) 
            //LevelUpProcedure()

        }

        public void LevelUpProcedure()
        {
            //need to work out leveling up equations and how levels affect base stats. 
        }
    }

}