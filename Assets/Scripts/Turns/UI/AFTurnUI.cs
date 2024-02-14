using AFSInterview.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace AFSInterview.Turns.UI
{
    public class AFTurnUI : AFObserverMonoBehaviour
    {
        [Inject]
        private AFTurnManager turnManager;
        
        [SerializeField]
        private TextMeshProUGUI currentTurn;
        
        protected override void RegisterEvents()
        {
            turnManager.OnNextTurn.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            turnManager.OnNextTurn.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            currentTurn.text = $"Current turn: {turnManager.CurrentTurn}";
        }
    }
}