using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.utils;

namespace com.sdmission.logic.movement.deciders
{
    public interface IMovementDecider
    {
		Coordinates<int> getNextMove();
	}
}