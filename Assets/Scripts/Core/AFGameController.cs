using System.Collections;
using AFSInterview.Units;
using UnityEngine;
using Zenject;

namespace AFSInterview.Core
{
    public class AFGameController : MonoBehaviour
    {
        [Inject]
        private AFUnitsManager unitsManager;

        private IEnumerator Start()
        {
            InitializeManagers();
            yield return null;
            StartGame();
        }

        private void InitializeManagers()
        {
            unitsManager.Initialize();
        }

        private void StartGame()
        {
            unitsManager.RandomizeUnitsOrder();
        }
    }
}