using UnityEngine;

namespace Codebase.Content
{
    [System.Serializable]
    public class Content
    {
        public string Name;
        public int Cost;
        [System.NonSerialized] public bool Access;
        public Sprite Preview;
    }
}
