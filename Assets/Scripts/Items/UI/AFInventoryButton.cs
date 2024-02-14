using UnityEngine;

namespace AFSInterview.Items.UI
{
    public class AFInventoryButton : MonoBehaviour
    {
        [SerializeField]
        private AFInventoryUI InventoryPanel;

        public void ToggleInventoryPanel()
        {
            InventoryPanel.gameObject.SetActive(!InventoryPanel.gameObject.activeSelf);
        }
    }
}