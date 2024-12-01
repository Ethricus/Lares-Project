using UnityEngine;

namespace Mechanics.BattleSystem
{
    public struct AttackInfo
    {
        public BattleComponent Other;
        public int AttackValue;
        public int AttackPower;
        public bool Crit;
        public float AttackCritMod; 

        public AttackInfo(BattleComponent other, Attack attack)
        {
            Other = other;
            AttackValue = attack.AttackValue;
            AttackPower = attack.AttackPower;
            Crit = (Random.Range(0, 1000) > other.Luck / 1000);
            if (attack.attackType == Attack.AttackType.SERI)
            {
                AttackCritMod = other.SAtkCritMod;
                return;
            }
            AttackCritMod = other.PAtkCritMod;
        }
    }
}
