namespace UnityEngine
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 v2d)
        {
            return new Vector3(v2d.x, v2d.y, 0);
        }
        
        public static Vector2 ScaleX(this Vector2 v2d, float x)
        {
            v2d.x *= x;
            return v2d;
        }
        
        public static Vector2 ScaleY(this Vector2 v2d, float y)
        {
            v2d.y *= y;
            return v2d;
        }
        
        public static Vector2 ScaleXY(this Vector2 v2d, float x, float y)
        {
            v2d.x *= x;
            v2d.y *= y;
            return v2d;
        }
    }
    
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 v3d)
        {
            return new Vector2(v3d.x, v3d.y);
        }
        
        public static Vector3 ScaleX(this Vector3 v3d, float x)
        {
            v3d.x *= x;
            return v3d;
        }
        
        public static Vector3 ScaleY(this Vector3 v3d, float y)
        {
            v3d.y *= y;
            return v3d;
        }
        
        public static Vector3 ScaleZ(this Vector3 v3d, float z)
        {
            v3d.z *= z;
            return v3d;
        }
        
        public static Vector3 ScaleXY(this Vector3 v3d, float x, float y)
        {
            v3d.x *= x;
            v3d.y *= y;
            return v3d;
        }
        
        public static Vector3 ScaleYZ(this Vector3 v3d, float y, float z)
        {
            v3d.y *= y;
            v3d.z *= z;
            return v3d;
        }
        
        public static Vector3 ScaleXZ(this Vector3 v3d, float x, float z)
        {
            v3d.x *= x;
            v3d.z *= z;
            return v3d;
        }
        
        public static Vector3 ScaleXYZ(this Vector3 v3d, float x, float y, float z)
        {
            v3d.x *= x;
            v3d.y *= y;
            v3d.z *= z;
            return v3d;
        }
    }
}