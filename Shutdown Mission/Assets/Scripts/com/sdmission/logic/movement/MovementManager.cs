using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;

namespace com.sdmission.logic.movement
{
    public class MovementManager
    {
		public GameObject gameObject;
		//public float speed;
		//public boolean targetReached;
		//public int nextMove;
		
        //public const float FRAME_RATE = 50;
        private Coordinates<float> nextPosition;
        private Coordinates<float> currentPosition;
        private int currentDirection;
        private Queue<Coordinates<int>> futureTilePositions;
        public Coordinates<int> currentTilePosition;
        private Coordinates<int> nextTilePosition;
        //private float speedMultiplier = 1.2f;
        private bool hasReachedTarget;
        private float objectSpeed;
		private float speed;
		
		public MovementManager(GameObject gameObject, float speed)
        {
			this.gameObject = gameObject;
			this.objectSpeed = speed;
			this.speed = speed;
			hasReachedTarget = true;
			futureTilePositions = new Queue<Coordinates<int>>();
        }
		
		public void SetInitialTilePosition(Coordinates<int> initialPosition)
        {
            currentTilePosition = initialPosition;
            nextTilePosition = initialPosition;
            nextPosition = new Coordinates<float>(initialPosition.x , initialPosition.z, initialPosition.layer);
            currentPosition = nextPosition;
        }

        public void OnPositionConfirmed(Coordinates<int> position)
        {
            futureTilePositions.Enqueue(position);
            Debug.Log("Player moved to " + position.ToString() + "("+ futureTilePositions.Count + " enqueued tiles)");
            //var bombsInTile:Array = GameData.instance.positionManager.bombsInTile(newTilePosition);
            //updateSpeed();
        }

		public bool isIddle() {
			if(hasReachedTarget){
                if(futureTilePositions.Count == 0) {
					return true;
				}
			}
			return false;
		}

        public void OnTimeTick() 
        {
            if(hasReachedTarget){
                if(futureTilePositions.Count == 0) {
                    if(objectSpeed != 0)
                    {
                        objectSpeed = 0;
                        updateLocation(currentPosition);
                    }
                } else {
                    calculateNewTarget();
                    walkToTarget();
                }
            } else {
                walkToTarget();
            }
        }
		
     private void calculateNewTarget() 
        {
            if(futureTilePositions.Count < 1)
            {
                Debug.LogError("Should not pop from empty future tile positions queue");
                return;
            }
            nextTilePosition = futureTilePositions.Dequeue();
            if(nextTilePosition.x < currentTilePosition.x)
            {
                currentDirection =  ObjectsMovement.WEST;
            } 
            else if(nextTilePosition.x > currentTilePosition.x)
            {
                currentDirection = ObjectsMovement.EAST;
            } 
            else if(nextTilePosition.z < currentTilePosition.z)
            {
                currentDirection = ObjectsMovement.SOUTH;
            }
            else if(nextTilePosition.z > currentTilePosition.z)
            {
                currentDirection = ObjectsMovement.NORTH;
            }
//            objectSpeed = movableObject.getMaxSpeed();
//            updateSpeed();
            nextPosition = new Coordinates<float>(nextTilePosition.x, nextTilePosition.z, nextTilePosition.layer);
            hasReachedTarget = false;
            updateDirection(currentDirection);
        }

        private void walkToTarget()
        {
            float currentX = currentPosition.x;
            float currentZ = currentPosition.z;
            switch(currentDirection)
            {
                case ObjectsMovement.WEST:
                {
                    currentX -= getStepSize();
                    if(currentX <= nextPosition.x)
                    {
                        currentX = nextPosition.x;
                        hasReachedTarget = true;
                    }
                    break;
                }
                case ObjectsMovement.EAST:
                {
                    currentX += getStepSize();
                    if(currentX >= nextPosition.x)
                    {
                        currentX = nextPosition.x;
                        hasReachedTarget = true;
                    }
                    break;
                }
                case ObjectsMovement.SOUTH:
                {
                    currentZ -= getStepSize();
                    if(currentZ <= nextPosition.z)
                    {
                        currentZ = nextPosition.z;
                        hasReachedTarget = true;
                    }
                    break;
                }
                case ObjectsMovement.NORTH:
                {
                    currentZ += getStepSize();
                    if(currentZ >= nextPosition.z)
                    {
                        currentZ = nextPosition.z;
                        hasReachedTarget = true;
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }
            currentPosition = new Coordinates<float>(currentX, currentZ, currentPosition.layer);
            if(hasReachedTarget) 
            {
                currentTilePosition = nextTilePosition;
            }
            Debug.Log("Position update: " + currentPosition.ToString() + "(speed: " + objectSpeed + "; step size: " + getStepSize() + ")");
            updateLocation(currentPosition);
        }

        public void updateDirection(int direction)
        {
            float rotationY = 0;
            if(direction == ObjectsMovement.SOUTH) {
                rotationY = 180;
            } else if (direction == ObjectsMovement.EAST) {
                rotationY = 90;
            } else if (direction == ObjectsMovement.WEST) {
                rotationY = 270;
            }
			
			float oldRx = gameObject.transform.eulerAngles.x;
			float oldRz = gameObject.transform.eulerAngles.z;
			
			gameObject.transform.eulerAngles = new Vector3(oldRx, rotationY, oldRz);
            //GameController.Instance.viewManager.UpdateObjectRotation("Player" + id, 0, rotationY, 0);
        }
		
        public void updateLocation(Coordinates<float> coordinates)
        {
           gameObject.transform.localPosition = new Vector3(coordinates.x, coordinates.layer, coordinates.z);
        }
		
		private float getStepSize() 
        {
            //return movableObject.getMaxSpeed() / FRAME_RATE * speedMultiplier ;
			return speed;
        }
    }
}
