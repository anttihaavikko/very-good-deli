

using Godot;

namespace AnttiStarter.Extensions
{
    public static class VectorExtension
    {
        public static Vector2 RandomOffset(this Vector2 v, float maxLength)
        {
            var rng = new RandomNumberGenerator();
            return v + new Vector2(rng.RandfRange(-maxLength, maxLength), rng.RandfRange(-maxLength, maxLength));
        }

        public static Vector2 WhereY(this Vector2 v, float y) {
            return new Vector2(v.X, y);
        }
	
        public static Vector3 WhereZ(this Vector2 v, float z) {
            return new Vector3(v.X, v.Y, z);
        }

        public static float RealAngle(this Vector2 v)
        {
            return Mathf.RadToDeg(Mathf.Atan2(v.Y, v.X));
        }
        
        public static Vector2I RoundToInt(this Vector2 v)
        {
            return new Vector2I(Mathf.RoundToInt(v.X), Mathf.RoundToInt(v.Y));
        }
    }
}