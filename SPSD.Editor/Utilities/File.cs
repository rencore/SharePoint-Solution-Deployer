#region

using System;
using System.IO;
using System.Text;

#endregion

namespace SPSD.Editor.Utilities
{
    internal static class File
    {
        public static string RelativePath(string source, string target)
        {
            string[] sourceDirs = new FileInfo(source).DirectoryName.Split(Path.DirectorySeparatorChar);
            string[] targetDirs = new FileInfo(target).DirectoryName.Split(Path.DirectorySeparatorChar);

            // Get the shortest of the two paths
            int len = sourceDirs.Length < targetDirs.Length
                          ? sourceDirs.Length
                          : targetDirs.Length;

            // Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            // Find common root
            for (index = 0; index < len; index++)
            {
                if (sourceDirs[index] == targetDirs[index]) lastCommonRoot = index;
                else break;
            }

            // If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException(
                    "The reference file does not have a common base path with the current file. Both files have to be stored on the same drive.");
            }

            // Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            // Add on the ..
            for (index = lastCommonRoot + 1; index < sourceDirs.Length; index++)
            {
                if (sourceDirs[index].Length > 0) relativePath.Append(".." + Path.DirectorySeparatorChar);
            }

            // Add on the folders
            for (index = lastCommonRoot + 1; index < targetDirs.Length; index++)
            {
                relativePath.Append(targetDirs[index] + Path.DirectorySeparatorChar);
            }
            //relativePath.Append(targetDirs[targetDirs.Length - 1]);
            return Path.Combine(relativePath.ToString(), new FileInfo(target).Name);
        }

        public static string RelativePathTo(this FileInfo source, FileInfo target)
        {
            if (target == null || source == null)
            {
                throw new ArgumentException("No file was specified.");
            }
            return RelativePath(source.FullName, target.FullName);
        }
    }
}