using AFSInterview.Items;
using AFSInterview.Money;
using AFSInterview.Turns;
using AFSInterview.Units;
using UnityEngine;
using Zenject;

namespace AFSInterview.Core
{
    public class AFMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private AFMoneyManager moneyManager;

        [SerializeField]
        private AFUnitsManager unitsManager;

        [SerializeField]
        private AFTurnManager turnManager;

        [SerializeField]
        private AFItemsManager itemsManager;

        public override void InstallBindings()
        {
            Container.BindFactory<MonoBehaviour, AFGenericGameObjectFactory>();
            QueueInstance(moneyManager);
            QueueInstance(unitsManager);
            QueueInstance(turnManager);
            QueueInstance(itemsManager);
        }
        
        private void QueueInstance<TInstance>(TInstance instance)
        {
            Container.BindInstance(instance).IfNotBound();
            Container.QueueForInject(instance);
        }
    }
}