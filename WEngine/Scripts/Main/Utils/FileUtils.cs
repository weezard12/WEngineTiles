using System;
using System.IO;
using System.Text;
using IOFile = System.IO.File; // Alias for System.IO.File

namespace WEngine.Scripts.Main.Utils
{
    public class FileUtils
    {
        public static void CreateFolderIfDoesntExist(string path, bool clearDirectory = false)
        {
            // Just in case
            if (path.Equals("Program Files") || Path.GetDirectoryName(path).Equals("C:\\") || path.Length < 15)
                return;

            try
            {
                // If the path is to a file, call the method again with the file's folder as path
                if (Path.HasExtension(path))
                {
                    CreateFolderIfDoesntExist(Path.GetDirectoryName(path), clearDirectory);
                    return;
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return;
                }
                if (clearDirectory)
                {
                    Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    IOFile.Delete(path); // Use alias
                    CreateFolderIfDoesntExist(path, clearDirectory);
                }
            }
        }

        public static void CreateFileIfDoesntExist(string path, string content = "")
        {
            try
            {
                // If the path contains a directory structure, ensure it exists
                string directoryPath = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    CreateFolderIfDoesntExist(directoryPath);
                }

                // Check if the file already exists
                if (!IOFile.Exists(path)) // Use alias
                {
                    // Create the file and close the stream immediately
                    IOFile.WriteAllText(path, content); // Use alias
                }
            }
            catch (Exception ex)
            {
                throw; // Optionally rethrow the exception if needed
            }
        }

        public static bool ArePathsTheSame(string path1, string path2)
        {
            if (path1.Equals(path2)) return true;

            StringBuilder sb = new StringBuilder();
            foreach (char c in path1)
            {
                if (c == '\\')
                    sb.Append("\\");
                sb.Append(c);
            }
            if (sb.ToString().Equals(path2)) return true;

            sb.Clear();
            foreach (char c in path2)
            {
                if (c == '\\')
                    sb.Append("\\");
                sb.Append(c);
            }
            if (sb.ToString().Equals(path1)) return true;

            return false;
        }
    }
}