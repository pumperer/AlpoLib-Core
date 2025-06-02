using System;
using System.IO;

namespace alpoLib.Core.FileSystem
{
    public static class UriFileSystemHelper
    {
        public static byte[] ReadByte(LocalUri uri)
        {
            using var r = UnityEngine.Networking.UnityWebRequest.Get(uri.ToUri());
            r.SendWebRequest();
            while (!r.isDone){}

            return r.downloadHandler.data;
        }

        public static void DeleteFolder(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }
        
        public static void Delete(string path)
        {
            try
            {
                if (path.StartsWith("file://"))
                {
                    var uri = new LocalUri(path);
                    File.Delete(uri.LocalPath);
                }
                else if (!path.Contains("://"))
                    File.Delete(path);
                else
                    throw new NotSupportedException();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        public static bool Exists(string path)
        {
            if (!path.StartsWith("file://"))
                return !path.Contains("://") && File.Exists(path);
            
            var uri = new LocalUri(path);
            return File.Exists(uri.LocalPath);
        }
    }
}
