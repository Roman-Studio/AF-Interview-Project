using AFSInterview.Money;
using Zenject;

namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	public class AFInventoryController : MonoBehaviour
	{
		[Inject]
		private AFMoneyManager moneyManager;
		
		[SerializeField] 
		private List<AFItem> items;
		
		public int ItemsCount => items.Count;

		public void SellAllItemsUpToValue(int maxValue)
		{
			for (var i = items.Count - 1; i >= 0; i--)
			{
				var itemValue = items[i].Value;

				if (itemValue > maxValue)
				{
					continue;
				}

				moneyManager.AddMoney(itemValue);
				items.RemoveAt(i);
			}
		}

		public void AddItem(AFItem item)
		{
			items.Add(item);
		}
	}
}