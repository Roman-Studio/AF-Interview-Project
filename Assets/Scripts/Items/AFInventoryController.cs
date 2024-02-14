using System;
using AFSInterview.Money;
using UnityEngine.Events;
using Zenject;

namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	public class AFInventoryController : MonoBehaviour
	{
		[Inject]
		private DiContainer zenjectContainer;
		
		[Inject]
		private AFMoneyManager moneyManager;
		
		[SerializeField] 
		private List<AFItem> items;
		public IReadOnlyList<AFItem> Items => items;
		
		[field: SerializeField]
		public UnityEvent<AFItem> OnItemAdded { get; private set; }
		
		[field: SerializeField]
		public UnityEvent<AFItem> OnItemRemoved { get; private set; }
		
		public int ItemsCount => items.Count;

		private void Awake()
		{
			foreach (var item in items)
			{
				zenjectContainer.Inject(item);
			}
		}

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
			OnItemAdded?.Invoke(item);
		}

		public bool RemoveItem(AFItem itemToRemove)
		{
			var result = items.Remove(itemToRemove);

			if (result)
			{
				OnItemRemoved?.Invoke(itemToRemove);
			}

			return result;
		}
	}
}