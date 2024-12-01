using UnityEngine;

namespace Mechanics.BattleSystem
{
    public class Attack : MonoBehaviour
    {
        public GameObject attackPrefab;
        public enum AttackType { PHYSICAL, SERI };
        public AttackType attackType;
        public int AttackValue;
        public int AttackPower;
        public int CritChance;
        public int SPReduction;

        protected virtual void OnAttackStart() { }
        protected virtual void OnAttackEnd() { }

        protected void OnCollisionEnter(Collision collision)
        {
            //OnAttackEnd();
        }
    }
}
