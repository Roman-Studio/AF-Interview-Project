using AFSInterview.Money;
using UnityEngine;
using Zenject;

namespace AFSInterview.Items.UsageActions
{
    [CreateAssetMenu(fileName = "AddMoneyUsageAction", menuName = "AF/Items/UsageActions/AddMoney")]
    public class AFAddMoneyUsageAction : AFItemUsageAction
    {
        [Inject]
        private AFMoneyManager moneyManager;
        
        public override void PerformAction(AFItem usedItem)
        {
            moneyManager.AddMoney(usedItem.Value);
        }
    }
}