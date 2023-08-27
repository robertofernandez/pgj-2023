using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using com.sdmission.logic.model;

namespace com.sdmission.view
{
    public class MapsManager : MonoBehaviour
    {
		public GameObject wallTile;
		public GameObject baseTile;
		
		public GameMap logicMap;
		
        private void Awake()
        {
            Debug.Log("View initialized");
        }
		
		void Start()
        {
            Debug.Log("View started");
			
			string filePath = Application.dataPath + "/Levels/level1.txt";
			Debug.Log("reading " + filePath);
			
			float currentX = 0f;
			float currentZ = 0f;
			
			if (File.Exists(filePath))
			{
				string[] lines = File.ReadAllLines(filePath);
				foreach (string line in lines)
				{
					currentX = 0f;
					//Debug.Log(line);
					for (int i = 0; i < line.Length; i++)
					{
						char c = line[i];
						if('1' == c) {
							instantiateWall(currentX, currentZ);
						} else if('0' == c) {
							instantiateBase(currentX, currentZ);
						}
						currentX++;
					}
					currentZ++;
				}
				
				GameMapTile[,,] matrix = new GameMapTile[(int)currentX, 1, (int)currentZ];
			    int indexX = 0;
			    int indexZ = 0;

				foreach (string line in lines)
				{
					indexX = 0;
					for (int i = 0; i < line.Length; i++)
					{
						char c = line[i];
						if('1' == c) {
							matrix[indexX, 0, indexZ] = new GameMapTile("_" + indexX + "_" + indexZ, GameMap.FENCE);
						} else if('0' == c) {
							matrix[indexX, 0, indexZ] = new GameMapTile("_" + indexX + "_" + indexZ, GameMap.BASE);
						}
						indexX++;
					}
					indexZ++;
				}
				logicMap = new GameMap(matrix);
			}
			else
			{
				Debug.LogError("No level file.");
			}
    			
		}
		
		private void Update()
        {
		
		}
		
		public void instantiateWall(float x, float z) {
			Vector3 position = new Vector3(x, 0f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(wallTile, position, rotation);
		}
		
		public void instantiateBase(float x, float z) {
			Vector3 position = new Vector3(x, -0.5f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(baseTile, position, rotation);
		}
    }
}