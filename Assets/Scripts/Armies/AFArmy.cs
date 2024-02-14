using AFSInterview.Units;
using UnityEngine;

namespace AFSInterview.Armies
{
    public class AFArmy : MonoBehaviour
    {
        [field: SerializeField]
        public Color ArmyColor { get; private set; }
        
        private void Awake()
        {
            AssignArmyToUnitsInChildren();
        }

        private void AssignArmyToUnitsInChildren()
        {
            foreach (var unit in GetComponentsInChildren<AFUnitInstance>())
            {
                unit.SetUnitArmy(this);
            }
        }
    }
}