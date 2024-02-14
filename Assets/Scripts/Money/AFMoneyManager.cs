using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview.Money
{
    public class AFMoneyManager : MonoBehaviour
    {
        [field: SerializeField]
        public float Money { get; private set; }

        [field: SerializeField]
        public UnityEvent<float> OnMoneyChanged { get; private set; }

        public void AddMoney(float moneyToAdd)
        {
            Money += moneyToAdd;
            OnMoneyChanged?.Invoke(moneyToAdd);
        }
    }
}