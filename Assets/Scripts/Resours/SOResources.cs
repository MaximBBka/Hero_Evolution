using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu(menuName = "Data/Game/AllRes")]
    public class SOResources : ScriptableObject
    {
        public ModelResources[] ModelResources;
    }

    [Serializable]
    public struct ModelResources
    {
        public SOResource sOResource;
    }
}
