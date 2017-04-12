using System.Collections.Generic;

namespace GM.ECSLibrary
{
    public class Catalog
    {
        private Dictionary<string, object> Contents { get; set; }

        public Catalog()
        {
            Contents = new Dictionary<string, object>();
        }

        public void SetEntry(string key, object entry)
        {
            if (HasEntry(key))
            {
                Contents[key] = entry;
            }
            else
            {
                Contents.Add(key, entry);
            }
        }

        public bool HasEntry(string key)
        {
            return Contents.ContainsKey(key);
        }

        public T GetEntry<T>(string key)
        {
            return (T)Contents[key];
        }
    }
}
