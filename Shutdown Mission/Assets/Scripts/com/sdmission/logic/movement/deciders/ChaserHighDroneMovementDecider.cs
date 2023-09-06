using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;
using com.sdmission.logic.model;

namespace com.sdmission.logic.movement.deciders
{
    public class ChaserHighDroneMovementDecider : IMovementDecider
    {
	    public GameMap map;
		private MovementManager manager;
		private Coordinates<int> currentEnemyPosition;
		private bool enemyVisible;
		
		public ChaserHighDroneMovementDecider(GameMap map, MovementManager manager) {
		    this.map = map;
			this.manager = manager;
			enemyVisible = false;
		}
		
		public void updateEnemyPosition(Coordinates<int> enemyPosition) {
		    if(enemyPosition == null) {
				enemyVisible = false;
			} else {
			     enemyVisible = true;
				 this.currentEnemyPosition = enemyPosition;
			}
		}
		
		public Coordinates<int> getNextMove() {
			if(enemyVisible) {
				int direction = 0;
				int directionX = manager.currentTilePosition.x - currentEnemyPosition.x;
				int directionY = manager.currentTilePosition.z - currentEnemyPosition.z;
				if(directionX < 0 ) {
					direction = ObjectsMovement.EAST;
				} else if(directionX > 0 ) {
					direction = ObjectsMovement.WEST;
				} else if(directionY > 0 ) {
					direction = ObjectsMovement.SOUTH;
				} else if(directionY < 0 ) {
					direction = ObjectsMovement.NORTH;
				}
				Coordinates<int> nextTile = map.getNextTile(manager.currentTilePosition, direction, 1);
				if (map.requestMove(manager.currentTilePosition, nextTile, direction)) {
					return nextTile;
				}
			}
		    int randomDirection = Random.Range(0, 4);
			int i = 0;
			while(i < 4) {
				Coordinates<int> nextTile = map.getNextTile(manager.currentTilePosition, randomDirection, 1);
				if(!map.tileOuttOfBounds(nextTile)) {
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