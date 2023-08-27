using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;

namespace com.sdmission.logic.movement
{
    public class MovementCoordinator
    {
	    private IMovementDecider decider;
		private MovementManager manager;
	
		public MovementCoordinator(IMovementDecider decider, MovementManager manager)
		{
			this.decider = decider;
			this.manager = manager;
		}
		
		public void OnUpdate() {
			if(manager.isIddle()) {
				Coordinates<int> nextMove = decider.getNextMove();
				if(nextMove != null) {
					manager.OnPositionConfirmed(nextMove);
				}
			}
		}
	}	
}