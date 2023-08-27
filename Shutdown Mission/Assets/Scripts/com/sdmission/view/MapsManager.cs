using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.sdmission.view
{
    public class MapsManager : MonoBehaviour
    {
		public GameObject wallTile;
		public GameObject baseTile;
		
        private void Awake()
        {
            Debug.Log("View initialized");
        }
		
		void Start()
        {
            Debug.Log("View started");
			
			
			// remove example
			Vector3 position = new Vector3(0f, 0f, 0f);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(wallTile, position, rotation);
			
		}
		
		private void Update()
        {
		
		}		
    }
}