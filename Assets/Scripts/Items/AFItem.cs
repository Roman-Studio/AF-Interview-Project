namespace AFSInterview.Items
{
	using System;
	using UnityEngine;

	[Serializable]
	public class AFItem
	{
		[SerializeField] private string name;
		[SerializeField] private int value;

		public string Name => name;
		public int Value => value;

		public AFItem(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

		public void Use()
		{
			Debug.Log("Using" + Name);
		}
	}
}