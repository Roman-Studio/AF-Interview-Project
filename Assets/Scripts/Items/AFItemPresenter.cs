namespace AFSInterview.Items
{
	using UnityEngine;

	public class AFItemPresenter : MonoBehaviour, IAFItemHolder
	{
		[SerializeField] 
		private AFItem item;
        
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