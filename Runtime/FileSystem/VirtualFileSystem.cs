using System.IO;
using System.Threading.Tasks;

namespace alpoLib.Core.FileSystem
{
    public static class VirtualFileSystem
    {
        private const string FileSchemeString = "file://";
        
#if UNITY_ANDROID
        private const string JarSchemeString = "jar:file://";
        
        public static byte[] ReadFileFromJar(string path)
        {
            var saPathStr = UnityEngine.Application.streamingAssetsPath;
            var saPath = $"{saPathStr}/";
            if (path.StartsWith(saPath))
                path = path.Substring(saPathStr.Length + 1);
            return JARFile.ReadFileFromJar(path);
        }

        public static bool CheckFileExistsJar(string path)
        {
            var saPathStr = UnityEngine.Application.streamingAssetsPath;
            var saPath = $"{saPathStr}/";
            if (path.StartsWith(saPath))
                path = path.Substring(saPathStr.Length + 1);
            return JARFile.CheckFileExistsJar(path);
        }
#endif

        public static bool FileExists(string uriString)
        {
            if (uriString.StartsWith(FileSchemeString))
            {
                var uri = new LocalUri(uriString);
                var path = uri.LocalPath;
                return File.Exists(path);
            }
            
#if UNITY_ANDROID
            if (uriString.StartsWith(JarSchemeString))
            {
                return CheckFileExistsJar(uriString);
            }
#endif

            return File.Exists(uriString);
        }

        public static byte[] LoadFileBytesFromUri(string uriString)
        {
            if (uriString.StartsWith(FileSchemeString))
            {
                var uri = new LocalUri(uriString);
                var path = uri.LocalPath;
                if (File.Exists(path))
                    return File.ReadAllBytes(path);
            }
#if UNITY_ANDROID
            else if (uriString.StartsWith(JarSchemeString))
            {
                if (CheckFileExistsJar(uriString))
                {
                    var bytes = ReadFileFromJar(uriString);
                    if (bytes != null)
                        return bytes;
                }
            }
#endif
            else
            {
                if (File.Exists(uriString))
                    return File.ReadAllBytes(uriString);
            }

            return null;
        }

        public static Task<byte[]> LoadFileBytesFromUriAsync(string uriString)
        {
            if (uriString.StartsWith(FileSchemeString))
            {
                var uri = new LocalUri(uriString);
                var path = uri.LocalPath;
                if (File.Exists(path))
                    return File.ReadAllBytesAsync(path);
            }
#if UNITY_ANDROID
            else if (uriString.StartsWith(JarSchemeString))
            {
                return Task.Run(() =>
                {
                    if (!CheckFileExistsJar(uriString))
                        return null;
                    
                    var bytes = ReadFileFromJar(uriString);
                    return bytes;

                });
            }
#endif
            else
            {
                if (File.Exists(uriString))
                    return File.ReadAllBytesAsync(uriString);
            }

            return null;
        }
    }
}