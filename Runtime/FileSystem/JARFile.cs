#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alpoLib.Core.FileSystem
{
    internal static class JARFile
    {
        private static AndroidJavaObject assets = null;

        static JARFile()
        {
            assets = new AndroidJavaObject("com.alpo.androidmodule.AssetAccessor");
            Debug.Log($"AssetAccessor : {assets != null}");
        }

        internal static byte[] ReadFileFromJar(string path)
        {
            var obj = assets.CallStatic<AndroidJavaObject>("ReadAssetFromJar", path);
            if (obj == null)
                return null;

            var bytes = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(obj.GetRawObject());
            return bytes;
        }

        internal static bool CheckFileExistsJar(string path)
        {
            return assets.CallStatic<bool>("CheckFileExistsJar", path);
        }
    }
}
#endif