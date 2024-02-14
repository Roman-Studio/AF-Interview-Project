using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace AFSInterview.Units
{
    [CreateAssetMenu(fileName = "Unit", menuName = "AF/Units/Unit")]
    public class AFUnitData : ScriptableObject
    {
        [SerializeField]
        private List<AFUnitAttribute> unitAttributes = new ();
        public IReadOnlyList<AFUnitAttribute> UnitAttributes => unitAttributes;
        
        [field: SerializeField]
        public float HealthPoints { get; private set; }
                
        [field: SerializeField]
        public float ArmorPoints { get; private set; }
        
        [field: SerializeField]
        public int AttackInterval { get; private set; }
        
        [field: SerializeField]
        public float AttackDamage { get; private set; }
        
        [field: SerializeField]
        public bool HasDamageOverride { get; private set; }
        
        [field: SerializeField, ShowIf(nameof(HasDamageOverride))]
        public AFAttackOverrideByAttribute AttackOverrideByAttribute { get; private set; }
    }
}