using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using UnityEngine;

namespace ObjectMaps
{
    public class PrefabMap : DictionaryMap<Transform>
    {
        public string prefabPath;

        [ContextMenu("Load")]
        public void Load()
        {
            _assets = new Dictionary<string, Dictionary<string, Transform>>();
            categorizedAssets = new List<KeyValuePairList<Transform>>();

            string[] dirs = Directory.GetDirectories(Application.dataPath + "/Resources/" + prefabPath, "*", SearchOption.TopDirectoryOnly);

            foreach (string dir in dirs)
            {
                string[] tempstrarr = dir.Split("//");
                string tempstr = tempstrarr[tempstrarr.Length - 1];
                tempstrarr = tempstr.Split("\\");
                tempstr = tempstrarr[tempstrarr.Length - 1];

                _objects = new Dictionary<string, Transform>();
                UnityEngine.Object[] temp = Resources.LoadAll(prefabPath + "\\" + tempstr, typeof(Transform));

                foreach (UnityEngine.Object t in temp)
                    _objects.Add(t.name, (Transform)t);

                _assets.Add(tempstr, _objects);
                categorizedAssets.Add(new KeyValuePairList<Transform>(tempstr, objects));
            }
            SyncDictionary();
        }
    }
}