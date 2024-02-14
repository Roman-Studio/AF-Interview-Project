using System.Collections.Generic;
using AFSInterview.Items.UsageActions;
using NaughtyAttributes;
using System;
using UnityEngine;
using Zenject;

namespace AFSInterview.Items
{
	[Serializable]
	public class AFItem
	{
		[Inject]
		private DiContainer zenjectContainer;
		
		[Inject]
		private AFItemsManager itemsManager;
		
		[SerializeField] 
		private string name;
		
		[SerializeField] 
		private int value;

		[SerializeField, SerializeReference, Expandable]
		private List<AFItemUsageAction> itemUsageActions;

		public string Name => name;
		public int Value => value;
		public bool IsConsumable => itemUsageActions.Count > 0;

		public AFItem(string name, int value, List<AFItemUsageAction> itemUsageActions)
		{
			this.name = name;
			this.value = value;
			this.itemUsageActions = itemUsageActions;
		}

		public void Use()
		{
			if (!IsConsumable)
			{
				return;
			}
			
			Debug.Log($"Using: {Name}");
			
			foreach (var itemUsageAction in itemUsageActions)
			{
				zenjectContainer.Inject(itemUsageAction);
				itemUsageAction.PerformAction(this);
			}

			itemsManager.InventoryController.RemoveItem(this);
		}
	}
}