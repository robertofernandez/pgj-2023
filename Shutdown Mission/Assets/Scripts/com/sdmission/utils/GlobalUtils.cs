using System.Collections.Generic;
using com.sdmission.logic.movement;

namespace com.sdmission.utils
{
    public static class GlobalUtils
    {
        private static Dictionary<string, int> directionTranslations;

        public static int convertDirection(string input)
        {
            if (directionTranslations == null)
            {
                directionTranslations = new Dictionary<string, int>();
                directionTranslations.Add("N", ObjectsMovement.NORTH);
                directionTranslations.Add("S", ObjectsMovement.SOUTH);
                directionTranslations.Add("W", ObjectsMovement.WEST);
                directionTranslations.Add("E", ObjectsMovement.EAST);
            }
            if (directionTranslations.ContainsKey(input))
            {
                return directionTranslations[input];
            }
            else
            {
                return ObjectsMovement.NONE;
            }
        }
    }
}
