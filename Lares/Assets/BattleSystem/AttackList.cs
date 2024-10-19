using UnityEngine;

namespace Lares.BattleSystem
{
    [CreateAssetMenu(fileName = "AttackList", menuName = "Scriptable Objects/AttackList")]
    public class AttackList : ScriptableObject
    {
       public AttackData[] attacks;
    }
}
