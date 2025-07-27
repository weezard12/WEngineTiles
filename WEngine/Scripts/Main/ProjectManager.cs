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
        public static string GetProjectPath(string projectName)
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mainDirectory = Path.Combine(applicationDataDirectory, $"WEngine\\{projectName}");
            return mainDirectory;
        }

        public static void Initialize()
        {
            // Create the main directory for WEngine if it doesn't exist
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mainDirectory = Path.Combine(applicationDataDirectory, "WEngine");
            CreateFolderIfDoesntExist(mainDirectory);
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
