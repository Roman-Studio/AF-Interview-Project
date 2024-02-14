using AFSInterview.Core;
using TMPro;
using UnityEngine;

namespace AFSInterview.Items.UI
{
    public class AFItemPanel : AFObserverMonoBehaviour<AFItem>
    {
        [SerializeField]
        private TextMeshProUGUI itemName;

        [SerializeField]
        private TextMeshProUGUI itemValue;

        [SerializeField]
        private GameObject consumableInfo;
        
        protected override void RegisterEvents()
        {
            
        }

        protected override void UnregisterEvents()
        {
            
        }

        protected override void OnReactToChanges()
        {
            itemName.text = ObservedObject.Name;
            itemValue.text = ObservedObject.Value.ToString();
            consumableInfo.SetActive(ObservedObject.IsConsumable);
        }

        public void UseItem()
        {
            ObservedObject.Use();
        }
    }
}