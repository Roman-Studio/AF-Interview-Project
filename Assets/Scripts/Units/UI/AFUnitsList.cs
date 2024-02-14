using AFSInterview.Core;
using Zenject;

namespace AFSInterview.Units.UI
{
    public class AFUnitsList : AFMonoBehaviourPool<AFUnitPanel, AFUnitInstance>
    {
        [Inject]
        private AFUnitsManager unitsManager;
        
        protected override void RegisterEvents()
        {
            unitsManager.OnSelectedUnitChanged.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            unitsManager.OnSelectedUnitChanged.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            BindCollection(unitsManager.RegisteredUnits);
        }

        private void ReactToChanges(AFUnitInstance unitInstance)
        {
            ReactToChanges();
        }
    }
}