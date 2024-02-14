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

        public override void InstallBindings()
        {
            QueueInstance(moneyManager);
            QueueInstance(unitsManager);
            QueueInstance(turnManager);
        }
        
        private void QueueInstance<TInstance>(TInstance instance)
        {
            Container.BindInstance(instance).IfNotBound();
            Container.QueueForInject(instance);
        }
    }
}