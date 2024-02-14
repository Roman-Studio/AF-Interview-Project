using AFSInterview.Money;
using UnityEngine;
using Zenject;

namespace AFSInterview.Core
{
    public class AFMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private AFMoneyManager moneyManager;

        public override void InstallBindings()
        {
            QueueInstance(moneyManager);
        }
        
        private void QueueInstance<TInstance>(TInstance instance)
        {
            Container.BindInstance(instance).IfNotBound();
            Container.QueueForInject(instance);
        }
    }
}