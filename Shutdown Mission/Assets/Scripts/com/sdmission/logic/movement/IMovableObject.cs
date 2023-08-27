using System.Collections;
using System.Collections.Generic;
using com.sdmission.utils;

namespace com.sdmission.logic.movement
{
    public interface IMovableObject
    {
        void updatePosition(Coordinates<float> position, float speed);
        float getMaxSpeed();
        void updateDirection(int currentDirection);
    }
}
