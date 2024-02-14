using AFSInterview.Core;
using Zenject;

namespace AFSInterview.Items.UI
{
    public class AFInventoryUI : AFMonoBehaviourPool<AFItemPanel, AFItem>
    {
        [Inject]
        private AFItemsManager itemsManager;
        
        protected override void RegisterEvents()
        {
            itemsManager.InventoryController.OnItemAdded.AddListener(ReactToChanges);
            itemsManager.InventoryController.OnItemRemoved.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            itemsManager.InventoryController.OnItemAdded.RemoveListener(ReactToChanges);
            itemsManager.InventoryController.OnItemRemoved.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            BindCollection(itemsManager.InventoryController.Items);
        }

        private void ReactToChanges(AFItem item)
        {
            ReactToChanges();
        }
    }
}