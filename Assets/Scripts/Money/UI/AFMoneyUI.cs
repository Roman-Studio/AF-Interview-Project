using AFSInterview.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace AFSInterview.Money.UI
{
    public class AFMoneyUI : AFObserverMonoBehaviour
    {
        [Inject]
        private AFMoneyManager moneyManager;
        
        [SerializeField]
        private TextMeshProUGUI moneyText;
        
        protected override void RegisterEvents()
        {
            moneyManager.OnMoneyChanged.AddListener(OnReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            moneyManager.OnMoneyChanged.RemoveListener(OnReactToChanges);
        }

        private void OnReactToChanges(float money)
        {
            OnReactToChanges();
        }

        protected override void OnReactToChanges()
        {
            moneyText.text = $"Money: {moneyManager.Money}";
        }
    }
}