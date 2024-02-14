using UnityEngine;
using Zenject;

namespace AFSInterview.Units.View
{
    public class AFUnitOutline : MonoBehaviour
    {
        [Inject]
        private AFUnitsManager unitsManager;
        
        [SerializeField]
        private Outline outline;

        [SerializeField]
        private Color selectedOutlineColor;

        [SerializeField]
        private Color damageableOutlineColor;
        
        public void UpdateOutline(AFUnitInstance unitInstance)
        {
            if (unitsManager.CurrentTurnUnit == unitInstance)
            {
                outline.OutlineColor = selectedOutlineColor;
                outline.enabled = true;
                return;
            }

            if (unitInstance.CanBeDamaged())
            {
                outline.OutlineColor = damageableOutlineColor;
                outline.enabled = true;
                return;
            }

            outline.enabled = false;
        }
    }
}