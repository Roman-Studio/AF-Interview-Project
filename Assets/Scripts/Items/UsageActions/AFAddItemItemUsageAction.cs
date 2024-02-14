using UnityEngine;
using Zenject;

namespace AFSInterview.Items.UsageActions
{
    [CreateAssetMenu(fileName = "AddItemItemUsageAction", menuName = "AF/Items/UsageActions/AddItem")]
    public class AFAddItemItemUsageAction : AFItemUsageAction
    {
        [Inject]
        private DiContainer zenjectContainer;
        
        [Inject]
        private AFItemsManager itemsManager;

        [SerializeField]
        private AFItem itemToAdd;
        
        public override void PerformAction(AFItem usedItem)
        {
            zenjectContainer.Inject(itemToAdd);
            itemsManager.InventoryController.AddItem(itemToAdd);
        }
    }
}