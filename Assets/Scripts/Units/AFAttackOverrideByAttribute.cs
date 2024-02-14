using System;
using UnityEngine;

namespace AFSInterview.Units
{
    [Serializable]
    public class AFAttackOverrideByAttribute
    {
        [field: SerializeField]
        public AFUnitAttribute RequiredAttribute { get; private set; }
        
        [field: SerializeField]
        public float DamageOverride { get; private set; }
    }
}