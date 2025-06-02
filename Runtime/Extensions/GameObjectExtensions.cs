using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static void SetLayerRecursive(this GameObject o, int layer)
        {
            SetLayerInternal(o.transform, layer);
        }

        private static void SetLayerInternal(Transform t, int layer)
        {
            t.gameObject.layer = layer;

            foreach (Transform o in t)
            {
                SetLayerInternal(o, layer);
            }
        }
    }
}