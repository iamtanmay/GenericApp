using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities{

    /// <summary>
    /// 
    /// </summary>
    public class PhysicalInventoryMgr : MonoBehaviour    {
        public List<PhysicalInventoryItem> prefabs;
        public List<PhysicalInventoryItem> instantiatedItems;
        public List<Transform> slots;
        public List<Vector3> slotPositions;

        [ContextMenu("Get Slot Positions")]
        public void GetPositions(){            
            slotPositions = new List<Vector3>();
            foreach(Transform slot in slots)
                slotPositions.Add(slot.position);
        }

        public void PlaceItems(){        
            instantiatedItems = new List<PhysicalInventoryItem>();
            foreach(PhysicalInventoryItem item in prefabs){
                if (item.slotID < slotPositions.Count){
                    PhysicalInventoryItem temp = (Instantiate(item, slotPositions[item.slotID], Quaternion.identity));
                    temp.transform.parent = transform;
                    instantiatedItems.Add(temp);
                }
            }
        }

        public void ClearItems(){
            foreach(PhysicalInventoryItem item in instantiatedItems)
                DestroyImmediate(item);
            instantiatedItems = new List<PhysicalInventoryItem>();
        }

        [ContextMenu("Toggle Debug Cubes")]
        public void ToggleDebugCubes(){   
            foreach(Transform slot in slots){
                slot.GetComponent<MeshRenderer>().enabled = !slot.GetComponent<MeshRenderer>().enabled;
            }         
        }
        
        public void LoadData(){
        }

        public void SaveData(){
        }
    }
}