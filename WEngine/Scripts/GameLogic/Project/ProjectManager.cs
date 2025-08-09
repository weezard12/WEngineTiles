using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEngine.Scripts.Main.Utils.FileUtils;
namespace WEngine.Scripts.GameLogic.Project
{
    internal class ProjectManager
    {
        public const string EngineName = "WEngine";
        public static TilesProject CurrentProject { get; private set; }
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

        public static string GetFileInProject(string projectName, string fileName)
        {
            return Path.Combine(GetProjectPath(projectName), fileName);
        }
        public static string[] GetFilesInProject(string projectName, string folderName)
        {
            return Directory.GetFiles(Path.Combine(GetProjectPath(projectName), folderName));
        }

        public static void CreateProject(string projectName)
        {
            // Get the project path
            string projectPath = GetProjectPath(projectName);

            // Create necessary subdirectories
            CreateFolderIfDoesntExist(Path.Combine(projectPath, "Assets"));
            CreateFolderIfDoesntExist(Path.Combine(projectPath, "World"));
            CreateFolderIfDoesntExist(Path.Combine(projectPath, "Tilesets"));
        }

        internal static void DeleteProject(string projectName)
        {
            Directory.Delete(GetProjectPath(projectName), true);
        }

        public static void SetCurrentProject(string projectName)
        {
            CurrentProject = new TilesProject()
            {
                Name = projectName,
            };
        }

        internal static void WriteFileInProject(string projectName, string fileName, string json)
        {
            System.IO.File.WriteAllText(GetFileInProject(projectName, fileName), json);
        }
    }
}
