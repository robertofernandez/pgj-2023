using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;
using com.sdmission.logic.model;

namespace com.sdmission.logic.movement.deciders
{
    public class InputMovementDecider : IMovementDecider
    {
	    public GameMap map;
		private MovementManager manager;
		private float directionX = 0;
		private float directionY = 0;
		
		public InputMovementDecider(GameMap map, MovementManager manager) {
		    this.map = map;
			this.manager = manager;
		}
		
		public void updateDirections(float directionX, float directionY) {
		    this.directionX = directionX;
			this.directionY = directionY;
		}

		public Coordinates<int> getNextMove() {
			int direction = 0;
			if(directionX > 0 ) {
				direction = ObjectsMovement.EAST;
			} else if(directionX < 0 ) {
				direction = ObjectsMovement.WEST;
			} else if(directionY < 0 ) {
				direction = ObjectsMovement.SOUTH;
			} else if(directionY > 0 ) {
				direction = ObjectsMovement.NORTH;
			} else {
				return null;
			}
			Coordinates<int> nextTile = map.getNextTile(manager.currentTilePosition, direction, 1);
			if (map.requestMove(manager.currentTilePosition, nextTile, direction)) {
				return nextTile;
			} else {
				return null;
			}
		}
	}
}