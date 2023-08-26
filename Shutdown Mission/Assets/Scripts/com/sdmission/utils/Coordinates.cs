using UnityEngine;

namespace com.sdmission.utils
{
    public class Coordinates<T>
    {
        public T x;
        public T z;
        public T layer;

        public Coordinates(){}

        public Coordinates(T x, T z, T layer)
        {
            this.x = x;
            this.z = z;
            this.layer = layer;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Z: {1}, Layer: {2}", x, z, layer);
        }
    }
}