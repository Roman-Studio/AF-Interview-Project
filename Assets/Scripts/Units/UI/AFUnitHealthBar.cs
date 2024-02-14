using AFSInterview.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AFSInterview.Units.UI
{
    public class AFUnitHealthBar : AFObserverMonoBehaviour<AFUnitInstance>
    {
        [Inject]
        private AFUnitsManager unitsManager;
        
        [SerializeField]
        private Slider healthSlider;

        [SerializeField]
        private TextMeshProUGUI healthValue;
        
        protected override void RegisterEvents()
        {
            ObservedObject.OnCurrentHealthPointsChanged.AddListener(ReactToChanges);
            ObservedObject.OnUnitHovered.AddListener(ReactToChanges);
            ObservedObject.OnUnitUnhovered.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            ObservedObject.OnCurrentHealthPointsChanged.RemoveListener(ReactToChanges);
            ObservedObject.OnUnitHovered.RemoveListener(ReactToChanges);
            ObservedObject.OnUnitUnhovered.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            healthSlider.maxValue = ObservedObject.MaxHealthPoints;
            
            if (ObservedObject.IsHovered && ObservedObject.CanBeDamaged())
            {
                ShowPredictionValues();
            }
            else
            {
                ShowCurrentValues();
            }
        }

        private void ReactToChanges(AFUnitInstance unitInstance)
        {
            ReactToChanges();
        }

        private void ShowPredictionValues()
        {
            var predictedDamage = unitsManager.CurrentTurnUnit.GetPredictedAttackDamage(ObservedObject);
            var predictedHealthValue = ObservedObject.CurrentHealthPoints - predictedDamage;
            healthSlider.value = predictedHealthValue;
            healthValue.text = $"{predictedHealthValue}/{ObservedObject.MaxHealthPoints}";
        }

        protected void ShowCurrentValues()
        {
            healthSlider.value = ObservedObject.CurrentHealthPoints;
            healthValue.text = $"{ObservedObject.CurrentHealthPoints}/{ObservedObject.MaxHealthPoints}";
        }
    }
}