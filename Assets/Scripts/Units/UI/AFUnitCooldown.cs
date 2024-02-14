using AFSInterview.Core;
using TMPro;
using UnityEngine;

namespace AFSInterview.Units.UI
{
    public class AFUnitCooldown : AFObserverMonoBehaviour<AFUnitInstance>
    {
        [SerializeField]
        private GameObject cooldownPanel;
        
        [SerializeField]
        private TextMeshProUGUI cooldownValue;
        
        protected override void RegisterEvents()
        {
            ObservedObject.OnCurrentCooldownChanged.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            ObservedObject.OnCurrentCooldownChanged.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            if (ObservedObject.CurrentCooldown > 1)
            {
                cooldownPanel.SetActive(true);
                cooldownValue.text = $"{ObservedObject.CurrentCooldown - 1}";
            }
            else
            {
                cooldownPanel.SetActive(false);
            }
        }
    }
}