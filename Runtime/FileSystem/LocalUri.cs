using System;
using System.IO;

namespace alpoLib.Core.FileSystem
{
    public sealed class LocalUri
    {
        private string uriString;
        private readonly int schemeLength;

        public LocalUri(string localPath)
        {
            uriString = MakeUriString(localPath);
            schemeLength = uriString.IndexOf(Uri.SchemeDelimiter, StringComparison.InvariantCulture);
        }

        public LocalUri(LocalUri rootUri, string relativePath)
        {
            uriString = rootUri.uriString;
            schemeLength = uriString.IndexOf(Uri.SchemeDelimiter, StringComparison.InvariantCulture);
            Append(relativePath);
        }

        public LocalUri(Uri uri)
        {
            uriString = uri.ToString();
            schemeLength = uriString.IndexOf(Uri.SchemeDelimiter, StringComparison.InvariantCulture);
        }

        public override string ToString() => uriString;

        public Uri ToUri() => new(uriString);
        
        public string LocalPath
        {
            get
            {
                var index = uriString.IndexOf(Uri.SchemeDelimiter, StringComparison.InvariantCulture);
                if (index <= 0)
                    throw new Exception($"Invalid Uri!\nLocalPath\n[delimiter : {Uri.SchemeDelimiter}]\n[uri : {uriString}]");
                
                var delimiterLength = Uri.SchemeDelimiter.Length;
                var windowsIndex =
                    uriString.IndexOf(":/", index + delimiterLength, StringComparison.InvariantCulture);
                return windowsIndex == index + delimiterLength + 2 ? uriString[(index + delimiterLength + 1)..] : uriString[(index + delimiterLength)..];

            }
        }

        public string Scheme => uriString[..schemeLength];

        public string ToRelativeLocalPath(LocalUri rootUri)
        {
            if (uriString.StartsWith(rootUri.uriString))
                return uriString[rootUri.uriString.Length..];
            throw new Exception($"Invalid Uri!\nToRelativeLocalPath\n[uri : {uriString}]\n[rootUri : {rootUri.uriString}]");
        }
        
        public void Append(string relativeUri)
        {
            var replaced = relativeUri.Replace('\\', '/');
            if (replaced.Length >= 1 && replaced[0] != '/')
                uriString = $"{uriString}/{replaced}";
            else
                uriString = $"{uriString}{replaced}";
        }
        
        private string MakeUriString(string localPath)
        {
            if (localPath.StartsWith("file://"))
            {
                return IsWindowsAbsolutePath(localPath, schemeLength) ? localPath.Insert(schemeLength, "/") : localPath;
            }

            if (localPath.Contains(Uri.SchemeDelimiter))
                return localPath;

            if (IsWindowsAbsolutePath(localPath))
                return $"file:///{localPath.Replace('\\', '/')}";
            if (Path.IsPathRooted(localPath))
                return $"file://{localPath}";

            var isWindowsPath = false;
            var currentDir = Environment.CurrentDirectory;
            if (IsWindowsAbsolutePath(currentDir))
                isWindowsPath = true;

            var absPath = Path.Combine(currentDir, localPath).Replace('\\', '/');
            return $"{(isWindowsPath ? "file:///" : "file://")}{absPath}";
        }

        private static bool IsWindowsAbsolutePath(string path, int startIndex = 0)
        {
            if (path.Length < startIndex + 3 ||
                path[startIndex + 1] != ':' ||
                (path[startIndex + 2] != '\\' && path[startIndex + 2] != '/'))
                return false;
            
            return IsEnglishLetter(path[startIndex]);
        }

        private static bool IsEnglishLetter(char c)
        {
            return c is >= 'A' and <= 'Z' or >= 'a' and <= 'z';
        }
        
        public static implicit operator Uri(LocalUri lu)
        {
            return lu.ToUri();
        }
    }
}
