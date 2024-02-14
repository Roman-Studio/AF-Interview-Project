using Zenject;
using UnityEngine;

namespace AFSInterview.Items
{
	public class AFItemPresenter : MonoBehaviour, IAFItemHolder
	{
		[Inject]
		private DiContainer zenjectContainer;
		
		[SerializeField] 
		private AFItem item;

		private void Awake()
		{
			zenjectContainer.Inject(item);
		}

		public AFItem GetItem(bool disposeHolder)
		{
			if (disposeHolder)
			{
				Destroy(gameObject);
			}

			return item;
		}
	}
}