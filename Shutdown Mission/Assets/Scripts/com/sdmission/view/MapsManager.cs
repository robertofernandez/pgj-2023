using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using com.sdmission.logic.model;
using com.sdmission.utils;
using com.sdmission.logic.movement;

namespace com.sdmission.view
{
    public class MapsManager : MonoBehaviour
    {
		public GameObject wallTile;
		public GameObject baseTile;
		public GameObject battery;
		public GameObject eve;
		public GameObject blackDrone;
		
		public GameMap logicMap;
		
		public GameObject eveInstance;
		public MovementManager eveMovementManager;
		
        private void Awake()
        {
            Debug.Log("View initialized");
        }
		
		void Start()
        {
            Debug.Log("View started");
			
			string filePath = Application.dataPath + "/Levels/level2.txt";
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
						} else{
							instantiateBase(currentX, currentZ);
							if('2' == c) {
								instantiateBattery(currentX, currentZ);
							} else if('3' == c) {
								instantiateEve(currentX, currentZ);
							} else if('4' == c) {
								instantiateBlackDrone(currentX, currentZ);
							}
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
						} else {
							matrix[indexX, 0, indexZ] = new GameMapTile("_" + indexX + "_" + indexZ, GameMap.BASE);
							if('2' == c) {
								
							}
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
			
			//FIXME remove example
    		eveMovementManager.OnPositionConfirmed(new Coordinates<int>(18,8,0));
			eveMovementManager.OnPositionConfirmed(new Coordinates<int>(17,8,0));
			eveMovementManager.OnPositionConfirmed(new Coordinates<int>(16,8,0));
			eveMovementManager.OnPositionConfirmed(new Coordinates<int>(17,8,0));

		}
		
		private void FixedUpdate()
        {
			eveMovementManager.OnTimeTick();
		}
		
		public void instantiateWall(float x, float z) {
			Vector3 position = new Vector3(x, 0f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(wallTile, position, rotation);
		}

		public void instantiateEve(float x, float z) {
			Vector3 position = new Vector3(x, 0f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(eve, position, rotation);
			eveInstance = instantiatedPrefab;
			eveMovementManager = new MovementManager(eveInstance, 0.05f);
			eveMovementManager.SetInitialTilePosition(new Coordinates<int>((int)x, (int)z, 0));
		}
		
		public void instantiateBlackDrone(float x, float z) {
			Vector3 position = new Vector3(x, 0f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(blackDrone, position, rotation);
		}

		public void instantiateBase(float x, float z) {
			Vector3 position = new Vector3(x, -0.5f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(baseTile, position, rotation);
		}
		
		public void instantiateBattery(float x, float z) {
			Vector3 position = new Vector3(x, 0f, z);
			Quaternion rotation = Quaternion.identity;
			GameObject instantiatedPrefab = Instantiate(battery, position, rotation);
		}
    }
}