using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public struct KeyValuePairList<T>
    {
        public string key;
        public List<KeyValuePair<T>> val;

        public KeyValuePairList(string key, List<KeyValuePair<T>> val)
        {
            this.key = key;
            this.val = val;
        }
    }

    [Serializable]
    public struct KeyValuePair<T>
    {
        public string key;
        public T val;

        public KeyValuePair(string key, T val)
        {
            this.key = key;
            this.val = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TransformCollection : MonoBehaviour {


        public List<KeyValuePair<Transform>> collection = new List<KeyValuePair<Transform>>();
        public Dictionary<string, Transform> _collection = new Dictionary<string, Transform>();

        void SyncDictionary()
        {
            collection.Clear();
            foreach (KeyValuePair<string, Transform> kvp in _collection)
                collection.Add(new KeyValuePair<Transform>(kvp.Key, kvp.Value));
        }

        void ReverseSyncDictionary()
        {
            _collection.Clear();
            foreach (KeyValuePair<Transform> kvp in collection)
                _collection.Add(kvp.key, kvp.val);
        }

        public void Awake()
        {
            ReverseSyncDictionary();
        }

        [ContextMenu("Save Collection")]
        public void SaveCollection()
        {
            _collection = new Dictionary<string, Transform>();
            foreach (Transform child in transform)
                _collection.Add(child.gameObject.name, child);

            SyncDictionary();
        }

        [ContextMenu("Clear Collection")]
        public void ClearCollection()
        {
            collection= new List<KeyValuePair<Transform>>();
            _collection = new Dictionary<string, Transform> ();
        }

        [ContextMenu("Destroy Collection")]
        public void DestroyCollection()
        {
            collection = new List<KeyValuePair<Transform>>();

            foreach (KeyValuePair<string, Transform> kvp in _collection)
                DestroyImmediate(kvp.Value.gameObject);

            _collection = new Dictionary<string, Transform>();
        }

        public Transform this[string key]
        {
            get => GetTransform(key);
            set => SetTransform(key, value);
        }

        Transform GetTransform(string iKey)
        {
            if (_collection.ContainsKey(iKey))
            {
                return _collection[iKey];
            }
            else
                return null;
        }

        void SetTransform(string iKey, Transform value)
        {
            if (!_collection.ContainsKey(iKey))
            {
                _collection.Add(iKey, value);
                SyncDictionary();
            }
        }
    }
}