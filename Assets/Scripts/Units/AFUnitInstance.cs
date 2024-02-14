using System;
using System.Collections.Generic;
using System.Linq;
using AFSInterview.Armies;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AFSInterview.Units
{
    public class AFUnitInstance : MonoBehaviour, IAFDamageable
    {
        [Inject]
        private AFUnitsManager unitsManager;
        
        [field: SerializeField, Expandable]
        public AFUnitData UnitData { get; private set; }
        
        [field: SerializeField]
        public AFArmy UnitArmy { get; private set; }
        
        private float currentHealthPoints;
        public float CurrentHealthPoints
        {
            get => currentHealthPoints;
            private set
            {
                currentHealthPoints = value;
                OnCurrentHealthPointsChanged?.Invoke();
            }
        }

        [field: SerializeField]
        public UnityEvent OnCurrentHealthPointsChanged { get; private set; }

        public float MaxHealthPoints => UnitData.HealthPoints;
        public float ArmorPoints => UnitData.ArmorPoints;
        public IReadOnlyList<AFUnitAttribute> Attributes => UnitData.UnitAttributes;
        public float AttackDamage => UnitData.AttackDamage;
        public bool HasDamageOverride => UnitData.HasDamageOverride;
        public AFAttackOverrideByAttribute AttackOverrideByAttribute => UnitData.AttackOverrideByAttribute;
        
        [field: SerializeField, Foldout("Unit Selection")]
        public UnityEvent<AFUnitInstance> OnUnitSelected { get; private set; }
        
        [field: SerializeField, Foldout("Unit Selection")]
        public UnityEvent<AFUnitInstance> OnUnitDeselected { get; private set; }

        public bool IsSelected => unitsManager.CurrentTurnUnit == this;
        
        [field: SerializeField, Foldout("Unit Hover")]
        public UnityEvent<AFUnitInstance> OnUnitHovered { get; private set; }
        
        [field: SerializeField, Foldout("Unit Hover")]
        public UnityEvent<AFUnitInstance> OnUnitUnhovered { get; private set; }
        
        [field: SerializeField]
        public bool IsHovered { get; private set; }
        
        [field: SerializeField]
        public UnityEvent<AFUnitInstance> OnUnitDeath { get; private set; }
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (UnitData == null)
            {
                Debug.LogError($"[{nameof(AFUnitInstance)}.{nameof(Initialize)}] Unit data is not set! Make sure it's assigned in the inspector!", this);
                return;
            }

            CurrentHealthPoints = UnitData.HealthPoints;
            unitsManager.RegisterUnit(this);
            unitsManager.OnSelectedUnitChanged.AddListener(UpdateUnitHighlight);
        }

        private void OnDestroy()
        {
            unitsManager.OnSelectedUnitChanged.RemoveListener(UpdateUnitHighlight);
        }

        private void OnMouseEnter()
        {
            HoverUnit();
        }

        private void OnMouseExit()
        {
            UnhoverUnit();
        }

        public void SetUnitArmy(AFArmy army)
        {
            UnitArmy = army;
        }

        public float GetAttackDamage(IAFDamageable damageable)
        {
            if (damageable is not AFUnitInstance unitInstance)
            {
                return AttackDamage;
            }
            
            if (!HasDamageOverride || unitInstance.Attributes.Count == 0)
            {
                return AttackDamage;
            }

            return unitInstance.Attributes.Any(attribute => attribute == AttackOverrideByAttribute.RequiredAttribute) ? AttackOverrideByAttribute.DamageOverride : AttackDamage;
        }

        public bool CanBeDamaged()
        {
            if (IsSelected)
            {
                return false;
            }

            if (unitsManager.CurrentTurnUnit.UnitArmy == UnitArmy)
            {
                return false;
            }
            
            return true;
        }

        public void Damage(float receivedDamage)
        {
            var finalDamage = receivedDamage - ArmorPoints;

            if (finalDamage < 1f)
            {
                finalDamage = 1f;
            }

            CurrentHealthPoints -= finalDamage;
            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (CurrentHealthPoints > 0f)
            {
                return;
            }
            
            OnUnitDeath?.Invoke(this);
            Destroy(gameObject);
        }

        private void UpdateUnitHighlight(AFUnitInstance selectedUnit)
        {
            if (selectedUnit == this)
            {
                OnUnitSelected?.Invoke(this);
            }
            else
            {
                OnUnitDeselected?.Invoke(this);
            }
        }

        public void HoverUnit()
        {
            IsHovered = true;
            OnUnitHovered?.Invoke(this);
        }

        public void UnhoverUnit()
        {
            IsHovered = false;
            OnUnitUnhovered.Invoke(this);
        }
    }
}