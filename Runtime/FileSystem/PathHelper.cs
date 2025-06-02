using System.IO;
using UnityEngine;

namespace alpoLib.Core.FileSystem
{
	public static class PathHelper
    {
        private static string documentPath = "";

        public static string DocumentRootPath => GetDocumentRootPath();
        public static string DocumentSubPath { get; set; } = "";
        public static string DocumentTempPath => "/temp";
        
        private static string GetDocumentRootPath()
        {
            if (!string.IsNullOrEmpty(documentPath))
                return documentPath;

            string resultPath;
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    resultPath = Application.temporaryCachePath;
                    break;
                
                case RuntimePlatform.Android:
                    resultPath = Application.temporaryCachePath;
                    break;

                case RuntimePlatform.WindowsEditor:
					resultPath = Application.dataPath.Replace('\\', '/').Replace("Assets", string.Empty);
                    if (resultPath.EndsWith('/'))
                        resultPath = resultPath[..resultPath.LastIndexOf('/')];
					resultPath = Path.Combine(resultPath, "AppData").Replace('\\', '/');
                    break;

				default:
                    resultPath = Application.temporaryCachePath;
					break;
            }

            documentPath = resultPath;
            return documentPath;
        }

        public static string GetLocalPathForRemoteFile(string remoteFilePath, bool isTemp = false)
        {
            var path = Path.Combine(DocumentRootPath, isTemp ? DocumentTempPath : DocumentSubPath);
            return Path.Combine(path, remoteFilePath).Replace("\\", "/");
        }

        public static string GetStreamingAssetFilePath(string relativePath)
        {
            var streamingPath = Path.Combine(Application.streamingAssetsPath, relativePath);
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			streamingPath = Path.Combine("file://", streamingPath);
#endif
            return streamingPath;
        }

        public static string GetInstallPackRawFolderPath(bool withPlatformFolder, bool createIfNotExist = false)
        {
            var installPackRawDir = Path.Combine(Application.dataPath, "..", "InstallPack");
            if (withPlatformFolder)
            {
                string platformName;
#if UNITY_ANDROID
                platformName = "Android";
#elif UNITY_IOS
                platformName = "iOS";
#else
                platformName = "Android";
#endif
                if (!string.IsNullOrEmpty(platformName))
                    installPackRawDir = Path.Combine(installPackRawDir, platformName);
            }
            
#if UNITY_EDITOR
            if (createIfNotExist && !Directory.Exists(installPackRawDir))
                Directory.CreateDirectory(installPackRawDir);
#endif
            return installPackRawDir;
        }
        
        public static string GetPatchSetPath()
        {
            var patchSetPath = Path.Combine(Application.dataPath, "..", "PatchSet");
            return patchSetPath;
        }
    }
}
