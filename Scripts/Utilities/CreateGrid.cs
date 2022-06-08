using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities{

    /// <summary>
    /// Instantiates objects in 3D Grid. If no Prefab target is provided, places small cubes
    /// Usage: Fill x,y,z,offset_x...,target and click "Generate Grid"/"Destroy Grid" from context Menu
    /// x,y,z size of Grid
    /// offset_x... spacing of Grid
    /// target object to instantiate
    /// generateAtRuntime run in Play mode
    /// </summary>
    public class CreateGrid : MonoBehaviour    {

        public int x, y, z;
        public float offset_x, offset_y, offset_z;
        public Transform target;
        public bool generateAtRuntime = false;
        public Transform[] grid;

        void Start()        { 
            if (generateAtRuntime)
                GenerateGrid();           
        }

        [ContextMenu("Generate Grid")]
        public void GenerateGrid () {

            x = x==0?1:x;
            y = y==0?1:y;
            z = z==0?1:z;

            grid = new Transform[x*y*z];
            float midpoint_x = ((float)x-1f)/2f, midpoint_y = ((float)y-1f)/2f, midpoint_z = ((float)z-1f)/2f;

            Vector3 startPos = transform.position - new Vector3(midpoint_x * offset_x, midpoint_y * offset_y, midpoint_z * offset_z);

            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    for (int k = 0; k < z; k++) {

                        Vector3 initializePos = startPos + new Vector3(i*offset_x, j*offset_y, k* offset_z);

                        if (target != null)
                            grid[i*y*z+j*z+k] = Instantiate(target, initializePos, Quaternion.identity);

                        else{
                            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            cube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                            cube.transform.position = initializePos;
                            cube.transform.parent = transform;
                            grid[i*y*z+j*z+k] = cube.transform;
                        }                            
                    }
                }
            }

            Debug.Log("Created Grid of size:" + x + " x " + y + " x " + z + " with " + grid.Length + " elements");
        }

        [ContextMenu("Destroy Grid")]
        public void DestroyGrid(){

            foreach (Transform t in grid)
                DestroyImmediate(t.gameObject);

            grid = new Transform[0];
        }
    }
}