using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMaps
{
    public class TransformMap : DictionaryMap<Transform>
    {
        [ContextMenu("DumpUncategorized")]
        public void DumpUncategorized()
        {
            foreach (KeyValuePair<string, Transform> pair in uncategorizedAssets)
                Debug.Log(pair.Key);
        }

    }
}