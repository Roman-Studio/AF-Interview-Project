using System.Collections.Generic;
using System.Linq;
using AFSInterview.Core;
using AFSInterview.Turns;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AFSInterview.Units
{
    public class AFUnitsManager : MonoBehaviour
    {
        [Inject]
        private AFTurnManager turnManager;
        
        [SerializeField]
        private List<AFUnitInstance> registeredUnits = new ();
        public IReadOnlyList<AFUnitInstance> RegisteredUnits => registeredUnits;

        private int currentUnitIndex;

        public AFUnitInstance CurrentTurnUnit
        {
            get
            {
                if (currentUnitIndex < 0)
                {
                    return null;
                }

                if (currentUnitIndex >= registeredUnits.Count)
                {
                    return null;
                }

                return registeredUnits[currentUnitIndex];
            }
        }
        
        [field: SerializeField]
        public UnityEvent<AFUnitInstance> OnSelectedUnitChanged { get; private set; }

        public void Initialize()
        {
            turnManager.OnNextTurn.AddListener(SelectNextUnit);
        }

        private void OnDestroy()
        {
            turnManager.OnNextTurn.RemoveListener(SelectNextUnit);
        }

        public void RegisterUnit(AFUnitInstance unitInstance)
        {
            registeredUnits.Add(unitInstance);
            unitInstance.OnUnitDeath.AddListener(UnregisterUnit);
        }

        private void UnregisterUnit(AFUnitInstance unitInstance)
        {
            registeredUnits.Remove(unitInstance);
        }

        public void RandomizeUnitsOrder()
        {
            registeredUnits.Shuffle();
            OnSelectedUnitChanged?.Invoke(CurrentTurnUnit);
        }

        private void SelectNextUnit()
        {
            currentUnitIndex++;

            if (currentUnitIndex >= registeredUnits.Count)
            {
                currentUnitIndex = 0;
            }
            
            OnSelectedUnitChanged?.Invoke(CurrentTurnUnit);
        }
    }
}