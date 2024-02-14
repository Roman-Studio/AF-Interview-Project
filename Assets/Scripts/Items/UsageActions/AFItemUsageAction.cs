using UnityEngine;

namespace AFSInterview.Items.UsageActions
{
    public abstract class AFItemUsageAction : ScriptableObject
    {
        public abstract void PerformAction(AFItem usedItem);
    }
}