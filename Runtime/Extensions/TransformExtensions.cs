using System;

namespace UnityEngine
{
    public static class TransformExtensions
    {
        public enum SearchOption
        {
            Equal,
            Contain,
        }
        
        public static Transform Search(this Transform t, string name, SearchOption option = SearchOption.Equal)
        {
            switch (option)
            {
                case SearchOption.Equal:
                {
                    if (t.name.Equals(name))
                        return t;
                    break;
                }
                case SearchOption.Contain:
                {
                    if (t.name.Contains(name))
                        return t;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException($"SearchOption should be one of Equal or Contain.");
            }

            for (int n = 0, max = t.childCount; n < max; ++n)
            {
                var found = t.GetChild(n).Search(name);
                if (found != null)
                    return found;
            }

            return null;
        }
        
        #region Multiply Local Position
        
        public static void MultiplyLocalPosition(this Transform t, float scalar)
        {
            var v = t.localPosition;
            v *= scalar;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPosition(this Transform t, Vector3 v3d)
        {
            var v = t.localPosition;
            v.Scale(v3d);
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPosition(this Transform t, float x, float y, float z)
        {
            var v = t.localPosition;
            v.x *= x;
            v.y *= y;
            v.z *= z;
            t.localPosition = v;
        }

        public static void MultiplyLocalPositionX(this Transform t, float x)
        {
            var v = t.localPosition;
            v.x *= x;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPositionY(this Transform t, float y)
        {
            var v = t.localPosition;
            v.y *= y;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPositionZ(this Transform t, float z)
        {
            var v = t.localPosition;
            v.z *= z;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPositionXY(this Transform t, float x, float y)
        {
            var v = t.localPosition;
            v.x *= x;
            v.y *= y;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPositionYZ(this Transform t, float y, float z)
        {
            var v = t.localPosition;
            v.y *= y;
            v.z *= z;
            t.localPosition = v;
        }
        
        public static void MultiplyLocalPositionXZ(this Transform t, float x, float z)
        {
            var v = t.localPosition;
            v.x *= x;
            v.z *= z;
            t.localPosition = v;
        }
        
        #endregion
        
        #region Multiply World Position
        
        public static void MultiplyWorldPosition(this Transform t, float scalar)
        {
            var v = t.position;
            v *= scalar;
            t.position = v;
        }
        
        public static void MultiplyWorldPosition(this Transform t, Vector3 v3d)
        {
            var v = t.position;
            v.Scale(v3d);
            t.position = v;
        }
        
        public static void MultiplyWorldPosition(this Transform t, float x, float y, float z)
        {
            var v = t.position;
            v.x *= x;
            v.y *= y;
            v.z *= z;
            t.position = v;
        }

        public static void MultiplyWorldPositionX(this Transform t, float x)
        {
            var v = t.position;
            v.x *= x;
            t.position = v;
        }
        
        public static void MultiplyWorldPositionY(this Transform t, float y)
        {
            var v = t.position;
            v.y *= y;
            t.position = v;
        }
        
        public static void MultiplyWorldPositionZ(this Transform t, float z)
        {
            var v = t.position;
            v.z *= z;
            t.position = v;
        }
        
        public static void MultiplyWorldPositionXY(this Transform t, float x, float y)
        {
            var v = t.position;
            v.x *= x;
            v.y *= y;
            t.position = v;
        }
        
        public static void MultiplyWorldPositionYZ(this Transform t, float y, float z)
        {
            var v = t.position;
            v.y *= y;
            v.z *= z;
            t.position = v;
        }
        
        public static void MultiplyWorldPositionXZ(this Transform t, float x, float z)
        {
            var v = t.position;
            v.x *= x;
            v.z *= z;
            t.position = v;
        }
        
        #endregion
        
        #region Multiply Local Scale
        
        public static void MultiplyLocalScale(this Transform t, float scalar)
        {
            var v = t.localScale;
            v *= scalar;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScale(this Transform t, Vector3 v3d)
        {
            var v = t.localScale;
            v.Scale(v3d);
            t.localScale = v;
        }
        
        public static void MultiplyLocalScale(this Transform t, float x, float y, float z)
        {
            var v = t.localScale;
            v.x *= x;
            v.y *= y;
            v.z *= z;
            t.localScale = v;
        }

        public static void MultiplyLocalScaleX(this Transform t, float x)
        {
            var v = t.localScale;
            v.x *= x;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScaleY(this Transform t, float y)
        {
            var v = t.localScale;
            v.y *= y;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScaleZ(this Transform t, float z)
        {
            var v = t.localScale;
            v.z *= z;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScaleXY(this Transform t, float x, float y)
        {
            var v = t.localScale;
            v.x *= x;
            v.y *= y;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScaleYZ(this Transform t, float y, float z)
        {
            var v = t.localScale;
            v.y *= y;
            v.z *= z;
            t.localScale = v;
        }
        
        public static void MultiplyLocalScaleXZ(this Transform t, float x, float z)
        {
            var v = t.localScale;
            v.x *= x;
            v.z *= z;
            t.localScale = v;
        }
        
        #endregion
        
        public static void SetParentEx(this Transform t, Transform parent)
        {
            SetParentEx(t, parent, Vector3.zero);
        }

        public static void SetParentEx(this Transform t, Transform parent, Vector3 localPos)
        {
            SetParentEx(t, parent, localPos, Vector3.one);
        }

        public static void SetParentEx(this Transform t, Transform parent, Vector3 localPos, Vector3 localScale)
        {
            t.parent = parent;

            var transform = t.transform;
            transform.localPosition = localPos;
            transform.localScale = localScale;
            transform.localRotation = Quaternion.identity;
        }

        public static void SetIdentity(this Transform t)
        {
            var transform = t.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public static void DestroyAllChildren(this Transform t)
        {
            var childCount = t.childCount;
            for (var i = childCount - 1; i >= 0; --i)
            {
                var childTr = t.GetChild(i);
                Object.Destroy(childTr.gameObject);
            }
        }
    }
}