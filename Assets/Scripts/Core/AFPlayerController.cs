using AFSInterview.Turns;
using AFSInterview.Units;
using UnityEngine;
using Zenject;

namespace AFSInterview.Core
{
    public class AFPlayerController : MonoBehaviour
    {
        [Inject]
        private AFUnitsManager unitsManager;

        [Inject]
        private AFTurnManager turnManager;
        
        [SerializeField]
        private LayerMask damageableLayerMask;
		
        [SerializeField]
        private float maxAttackDistance = 100f;

        private Camera mainCamera;
        private Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                }

                return mainCamera;
            }
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryAttackWithCurrentUnit();
            }
        }
                
        private void TryAttackWithCurrentUnit()
        {
            if (unitsManager.CurrentTurnUnit == null)
            {
                return;
            }
            
            var ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, maxAttackDistance, damageableLayerMask) || !hit.transform.TryGetComponent<IAFDamageable>(out var damageable))
            {
                return;
            }

            if (!damageable.CanBeDamaged())
            {
                return;
            }
            
            damageable.Damage(unitsManager.CurrentTurnUnit.GetAttackDamage(damageable));
            turnManager.NextTurn();
        }
    }
}