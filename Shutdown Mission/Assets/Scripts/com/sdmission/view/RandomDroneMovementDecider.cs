using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;
using com.sdmission.logic.model;

namespace com.sdmission.logic.movement
{
    public class RandomDroneMovementDecider : IMovementDecider
    {
	    public GameMap map;
		private MovementManager manager;
		
		public RandomDroneMovementDecider(GameMap map, MovementManager manager) {
		    this.map = map;
			this.manager = manager;
		}
		public Coordinates<int> getNextMove() {
		    int randomDirection = Random.Range(0, 4);
			int i = 0;
			while(i < 4) {
				Coordinates<int> nextTile = map.getNextTile(manager.currentTilePosition, randomDirection, 1);
				if(map.requestMove(manager.currentTilePosition, nextTile, randomDirection)) {
					return nextTile;
				}
				i++;
				randomDirection = (randomDirection + 1) % 4;
			}
			//Debug.Log("No next move");
			return null;
		}
	}
}