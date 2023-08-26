using System;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;
using com.sdmission.logic.movement;

namespace com.sdmission.logic.model
{
    public class GameMap
    {
        public GameMapTile[,,] matrix;
        private int sizeX;
        private int sizeZ;

        public GameMap(GameMapTile[,,] matrix)
        {
            this.matrix = matrix;
            this.sizeX = matrix.GetLength(0);
            this.sizeZ = matrix.GetLength(2);
        }

        public Coordinates<int> getNextTile(Coordinates<int> currentTilePosition, int direction, int steps)
        {
            Debug.Log("[GameMap] calculating next tile for point <" + currentTilePosition.x + ", " + currentTilePosition.z + "> in direction " + direction);
            switch(direction)
            {
                case ObjectsMovement.WEST:
                {
                    return new Coordinates<int>(currentTilePosition.x - steps, currentTilePosition.z, currentTilePosition.layer);
                }
                case ObjectsMovement.EAST:
                {
                    return new Coordinates<int>(currentTilePosition.x + steps, currentTilePosition.z, currentTilePosition.layer);
                }
                case ObjectsMovement.SOUTH:
                {
                    return new Coordinates<int>(currentTilePosition.x, currentTilePosition.z - steps, currentTilePosition.layer);
                }
                case ObjectsMovement.NORTH:
                {
                    return new Coordinates<int>(currentTilePosition.x, currentTilePosition.z + steps, currentTilePosition.layer);
                }
                default:
                {
                    return new Coordinates<int>(currentTilePosition.x, currentTilePosition.z, currentTilePosition.layer);
                }
            }
        }

        public bool solidElement(Coordinates<int> previousTile, Coordinates<int> requestedTile) {
            GameMapTile item = matrix[requestedTile.x, requestedTile.layer, requestedTile.z];
            if(item != null) {
                if(item.blockType == "fence"){
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public bool requestMove(Coordinates<int> previousTile, Coordinates<int> requestedTile, int requestedDirection) {
            Debug.Log("[GameMap] requesting to move from <" + previousTile.x + ", " + previousTile.z + "> to <" + requestedTile.x + ", " + requestedTile.z + ">.");
            if(tileOuttOfBounds(requestedTile) || solidElement(previousTile, requestedTile)) {
                Debug.Log("[GameMap] won't to move from <" + previousTile.x + ", " + previousTile.z + "> to <" + requestedTile.x + ", " + requestedTile.z + "> (map element)");
                return false;
            }
            if(tileContainsSolidCharacter(requestedTile)) {
                Debug.Log("[GameMap] won't to move from <" + previousTile.x + ", " + previousTile.z + "> to <" + requestedTile.x + ", " + requestedTile.z + "> (character element)");
                return false;
            } else {
                Debug.Log("[GameMap] moving from <" + previousTile.x + ", " + previousTile.z + "> to <" + requestedTile.x + ", " + requestedTile.z + ">");
                return true;
            }
        }

        public bool tileOuttOfBounds(Coordinates<int> tile)
        {
            return tile.x < 1 || tile.x >= sizeX
                || tile.z < 1 || tile.z >= sizeZ;
        }

        public bool tileContainsSolidCharacter(Coordinates<int> tile)
        {
            //FUTURE implement when contained iteam are implemented
            return false;
        }
    }
}
