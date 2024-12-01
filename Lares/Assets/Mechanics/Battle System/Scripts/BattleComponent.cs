using Mechanics.Inventory;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Mechanics.BattleSystem
{
    public class BattleComponent : MonoBehaviour
    {
        public Weapon EquippedWeapon;
        public List<Accessory> EquippedAccessories;
        private int GetAccessoryIndex(string id) => EquippedAccessories.FindIndex(i => i.ItemId == id);

        public int Level { get; private set; }
        public int EXP;
        private float _modifierEXPGain;

        public int HP 
        {
            get => _currentHP; 
            private set
            {
                if (value > MaxHP)
                {
                    _currentHP = MaxHP;
                }
                else if (value < 0)
                {
                    Death?.Invoke();
                    _currentHP = 0;
                }
                else
                {
                    _currentHP = value;
                }
            }
        }

        public int MaxHP { get; private set; }
        private int _currentHP;
        private int _baseMaxHP;
        private float _modifierMaxHP;

        public int SP 
        {
            get => _currentSP;
             
            private set
            {
                if (value > MaxSP)
                {
                    _currentSP = MaxSP;
                }
                else if (value <= 0)
                {
                    _currentSP = 0;
                }
                else
                {
                    _currentSP = value;
                }
            }
        }
        public int MaxSP { get; private set; }
        private int _currentSP;
        private int _baseMaxSP;
        private float _modifierMaxSP;

        public int PAtk => Mathf.RoundToInt(_currentPAtk * _modifierPAtk);
        private int _currentPAtk;
        private int _basePAtk;
        private float _modifierPAtk;

        public int PDef => Mathf.RoundToInt(_currentPDef * _modifierPDef);
        private int _currentPDef;
        private int _basePDef;
        private float _modifierPDef;

        public int SAtk => Mathf.RoundToInt(_currentSAtk * _modifierSAtk);
        private int _currentSAtk;
        private int _baseSAtk;
        private float _modifierSAtk;

        public int SDef => Mathf.RoundToInt(_currentSDef * _modifierSDef);
        private int _currentSDef;
        private int _baseSDef;
        private float _modifierSDef;

        public int Luck => Mathf.RoundToInt(_currentLuck * _modifierLuck);
        private int _currentLuck;
        private int _baseLuck;
        private float _modifierLuck;

        public int Agility => Mathf.RoundToInt(_currentAgility * _modifierAgility);
        private int _currentAgility;
        private int _baseAgility;
        private float _modifierAgility;

        public int PAtkCritMod => Mathf.RoundToInt(_currentPAtkCritMod * _modifierPAtkCritMod);
        private int _currentPAtkCritMod;
        private int _basePAtkCritMod;
        private float _modifierPAtkCritMod;

        public int SAtkCritMod => Mathf.RoundToInt(_currentSAtkCritMod * _modifierSAtkCritMod);
        private int _currentSAtkCritMod;
        private int _baseSAtkCritMod;
        private float _modifierSAtkCritMod;

        public System.Action Death; 

        /// <summary>
        /// When a NPC has got enough EXP to level up, stat increases will be done here, base values are used to calcalate adjustment
        /// </summary>
        private void LevelUp()
        {
            //rate of stat increase is logarithmic
        }

        /// <summary>
        /// When an enemy is killed it will pass battleStats to the object that killed it here to calcualte EXP increase. 
        /// </summary>
        /// <param name="battleStats"></param>
        public void CalculateEXP(BattleStats battleStats)
        {
            // exp needed to level up is exponential

        }

        public bool ImplementAttack(Attack attack)
        {
            GameObject.Instantiate(attack.attackPrefab);
            AttackInfo attackInfo = new AttackInfo(this, attack); 
            return false;
        }

        public void AddAccessory(Accessory accessory)
        {
            EquippedAccessories.Add(accessory);
            AddModiferValues(accessory);
        }

        public void RemoveAccessory(Accessory accessory)
        {
            EquippedAccessories.Remove(accessory);
            RemoveModifierValues(accessory);
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == EquippedWeapon || weapon is null) return;
            EquippedWeapon?.OnRemove();
            weapon.OnAdd();
            EquippedWeapon = weapon;
            AddModiferValues(weapon);
        }

        public void AddModiferValues(Equippable equippableItem)
        {
            _modifierMaxHP += equippableItem.MaxHPModifier;
            _modifierMaxSP += equippableItem.MaxSPModifier;
            _modifierEXPGain += equippableItem.EXPGainModifier;
            _modifierPAtk += equippableItem.PAtkModifier;
            _modifierPDef += equippableItem.PDefModifier;
            _modifierSAtk += equippableItem.SAtkModifier;
            _modifierSDef += equippableItem.SDefModifier;
            _modifierAgility += equippableItem.AgilityModifier;
            _modifierLuck += equippableItem.LuckModifier;
            _modifierPAtkCritMod += equippableItem.PAtkModifier;
            _modifierSAtkCritMod += equippableItem.PAtkCritModifier;
        }

        private void RemoveModifierValues(Equippable equippableItem)
        {
            _modifierMaxHP -= equippableItem.MaxHPModifier;
            _modifierMaxSP -= equippableItem.MaxSPModifier;
            _modifierEXPGain -= equippableItem.EXPGainModifier;
            _modifierPAtk -= equippableItem.PAtkModifier;
            _modifierPDef -= equippableItem.PDefModifier;
            _modifierSAtk -= equippableItem.SAtkModifier;
            _modifierSDef -= equippableItem.SDefModifier;
            _modifierAgility -= equippableItem.AgilityModifier;
            _modifierLuck -= equippableItem.LuckModifier;
            _modifierPAtkCritMod -= equippableItem.PAtkModifier;
            _modifierSAtkCritMod -= equippableItem.PAtkCritModifier;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //check if damage collision
            //if damage collision then calcaulte damage in battle calcualtions.
        }
    }
}
