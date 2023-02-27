using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace ObjectMaps
{

    /// <summary>
    /// Used to store Dictionary<string, Dictionary<string, T>> with Unity inspector visibility
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DictionaryMap<T> : MonoBehaviour
    {
        public List<KeyValuePairList<T>> categorizedAssets = new List<KeyValuePairList<T>>();
        public Dictionary<string, T> uncategorizedAssets = new Dictionary<string, T>();
        public Dictionary<string, Dictionary<string, T>> _assets = new Dictionary<string, Dictionary<string, T>>();

        public List<KeyValuePair<T>> objects = new List<KeyValuePair<T>>();
        public Dictionary<string,T> _objects = new Dictionary<string, T>();
        public int IDCounter = 0;

        void Awake()
        {
            SyncDictionary();
        }

        /// <summary>
        /// Syncs the categorized and uncategorized assets for quick search by unique ID only
        /// </summary>
        public void SyncDictionary()
        {
            categorizedAssets = new List<KeyValuePairList<T>>();
            uncategorizedAssets = new Dictionary<string, T>();
            foreach (KeyValuePair<string, Dictionary<string, T>> kvp in _assets)
            {
                objects = new List<KeyValuePair<T>>();
                foreach (KeyValuePair<string, T> kvp2 in kvp.Value)
                {
                    objects.Add(new KeyValuePair<T>(kvp2.Key, kvp2.Value));
                    uncategorizedAssets.Add(kvp2.Key, kvp2.Value);
                }
                categorizedAssets.Add(new KeyValuePairList<T>(kvp.Key, objects));
            }
        }

        public int GetNewID()
        {
            return IDCounter++;
        }

        /// <summary>
        /// Adds a new Item to the category and returns a Unique ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddItem(string categoryID, T item)
        {
            if (item == null) return "";

            if (_assets.ContainsKey(categoryID))
            {
                string itemID = GetNewID() + "";
                if (!_assets[categoryID].ContainsKey(itemID))
                    _assets[categoryID].Add(itemID, item);
                else
                    _assets[categoryID][itemID] = item;
                SyncDictionary();
                return itemID;
            }
            else
            {
                _assets.Add(categoryID, new Dictionary<string, T>());
                return AddItem(categoryID, item);
            }
        }

        public T GetItem(string ID)
        {
            if (uncategorizedAssets.ContainsKey(ID))
                return uncategorizedAssets[ID];
            return default(T);
        }

        public T GetItem(string categoryID, string itemID)
        {
            if (_assets.ContainsKey(categoryID))
                if (_assets[categoryID].ContainsKey(itemID))
                    return _assets[categoryID][itemID];
            return default(T);
        }

        /// <summary>
        /// Remove Item based on category ID and unique ID. Return false if it did not exist
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public bool RemoveItem(string categoryID, string itemID)
        {
            if (_assets.ContainsKey(categoryID))
                if (!_assets[categoryID].ContainsKey(itemID))
                {
                    _assets[categoryID].Remove(itemID);
                    SyncDictionary();
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Remove Item based on unique ID only. Return false if it did not exist
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool RemoveItem(string ID)
        {
            if (uncategorizedAssets.ContainsKey(ID))
                foreach (KeyValuePair<string, Dictionary<string, T>> kvp in _assets)
                    if (RemoveItem(kvp.Key, ID))
                        return true;
            return false;
        }

        public int GetSize(string categoryID)
        {
            if (_assets.ContainsKey(categoryID))
                return _assets[categoryID].Count;
            return -1;
        }
    }
}