using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEngine.Scripts.Main.Utils.FileUtils;
namespace WEngine.Scripts.Main
{
    internal class ProjectManager
    {
        public const string EngineName = "WEngine";
        public static void Initialize()
        {
            // Create the main directory for WEngine if it doesn't exist
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mainDirectory = Path.Combine(applicationDataDirectory, EngineName);
            CreateFolderIfDoesntExist(mainDirectory);
        }
        public static string GetProjectPath(string projectName)
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mainDirectory = Path.Combine(applicationDataDirectory, $"{EngineName}\\{projectName}");
            return mainDirectory;
        }

        public static List<string> GetAllProjectsNames()
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mainDirectory = Path.Combine(applicationDataDirectory, EngineName);

            if (!Directory.Exists(mainDirectory))
            {
                return new List<string>(); // Or throw exception if preferred
            }

            return Directory.EnumerateDirectories(mainDirectory)
                            .Select(dir => Path.GetFileName(dir))
                            .ToList();
        }

        public static void CreateProject(string projectName)
        {
            // Get the project path
            string projectPath = GetProjectPath(projectName);

            // Create necessary subdirectories
            CreateFolderIfDoesntExist(Path.Combine(projectPath, "Assets"));
            CreateFolderIfDoesntExist(Path.Combine(projectPath, "Tilesets"));
        }
    }
}
