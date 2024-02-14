using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview.Turns
{
    public class AFTurnManager : MonoBehaviour
    {
        [field: SerializeField] 
        public int CurrentTurn { get; private set; } = 1;

        [field: SerializeField]
        public UnityEvent OnNextTurn { get; private set; }

        public void NextTurn()
        {
            CurrentTurn++;
            OnNextTurn?.Invoke();
        }
    }
}