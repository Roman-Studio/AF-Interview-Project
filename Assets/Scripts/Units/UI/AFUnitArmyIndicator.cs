using AFSInterview.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview.Units.UI
{
    public class AFUnitArmyIndicator : AFObserverMonoBehaviour<AFUnitInstance>
    {
        [SerializeField]
        private Image armyIndicator;
        
        protected override void RegisterEvents()
        {
            
        }

        protected override void UnregisterEvents()
        {
            
        }

        protected override void OnReactToChanges()
        {
            armyIndicator.color = ObservedObject.UnitArmy.ArmyColor;
        }
    }
}