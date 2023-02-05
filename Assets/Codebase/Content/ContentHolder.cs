using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Content
{
    public class ContentHolder<T> : ScriptableObject where T : Content
    {
        [SerializeField] protected List<T> _contents;

        public List<T> Contents => _contents;

        public virtual T GetContent(string name)
        {
            return _contents.Find(x => x.Name == name);
        }

    }
}
