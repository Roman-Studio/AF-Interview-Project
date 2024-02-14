using AFSInterview.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview.Units.UI
{
    public class AFUnitHealthBar : AFObserverMonoBehaviour<AFUnitInstance>
    {
        [SerializeField]
        private Slider healthSlider;

        [SerializeField]
        private TextMeshProUGUI healthValue;
        
        protected override void RegisterEvents()
        {
            ObservedObject.OnCurrentHealthPointsChanged.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            ObservedObject.OnCurrentHealthPointsChanged.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            healthSlider.maxValue = ObservedObject.MaxHealthPoints;
            healthSlider.value = ObservedObject.CurrentHealthPoints;
            healthValue.text = $"{ObservedObject.CurrentHealthPoints}/{ObservedObject.MaxHealthPoints}";
        }
    }
}