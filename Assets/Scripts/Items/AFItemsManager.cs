using AFSInterview.Core;
using UnityEngine;
using Zenject;

namespace AFSInterview.Items
{
	public class AFItemsManager : MonoBehaviour
	{
		[Inject]
		private AFGenericGameObjectFactory itemsFactory;    
		
		[SerializeField] 
		private AFInventoryController inventoryController;
		public AFInventoryController InventoryController => inventoryController;
		
		[SerializeField] 
		private int itemSellMaxValue;
		
		[SerializeField] 
		private Transform itemSpawnParent;
		
		[SerializeField] 
		private GameObject itemPrefab;
		
		[SerializeField] 
		private BoxCollider itemSpawnArea;
		
		[SerializeField] 
		private float itemSpawnInterval;

		[SerializeField]
		private LayerMask itemsLayerMask;
		
		[SerializeField]
		private float maxPickupDistance = 100f;

		private Camera mainCamera;
		private Camera MainCamera
		{
			get
			{
				if (mainCamera == null)
				{
					mainCamera = Camera.main;
				}

				return mainCamera;
			}
		}

		private float nextItemSpawnTime;
		
		private void Update()
		{
			if (Time.time >= nextItemSpawnTime)
			{
				SpawnNewItem();
			}

			if (Input.GetMouseButtonDown(0))
			{
				TryPickUpItem();
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				inventoryController.SellAllItemsUpToValue(itemSellMaxValue);
			}
		}

		private void SpawnNewItem()
		{
			nextItemSpawnTime = Time.time + itemSpawnInterval;
			
			var spawnAreaBounds = itemSpawnArea.bounds;
			var position = new Vector3(
				Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
				0f,
				Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
			);

			itemsFactory.Create(itemPrefab, position, Quaternion.identity, itemSpawnParent);
		}

		private void TryPickUpItem()
		{
			var ray = MainCamera.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray, out var hit, maxPickupDistance, itemsLayerMask) || !hit.collider.TryGetComponent<IAFItemHolder>(out var itemHolder))
			{
				return;
			}

			var item = itemHolder.GetItem(true);
            inventoryController.AddItem(item);
            Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + inventoryController.ItemsCount + " items");
		}
	}
}