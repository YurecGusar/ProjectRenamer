using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ProjectRenamer
{
    public class Creator
    {
        private ConfigService _config = new ConfigService();
        private const string _sourceName = "StyleCop";
        private string _projName;
        private string _targetPath;
        private string[] _folderNames = new string[5];
        private DirectoryInfo _sourceDirectory = new DirectoryInfo(_sourceName);
        private DirectoryInfo _targetDirectory;

        public Creator()
        {
            _folderNames = _config.FolderNames;
        }

        public void MakeProject(string name, string foldersMake, string path = "" )
        {
            if (path == "")
            {
                path = _sourceDirectory.Parent.FullName;
            }
            _projName = name;
            _targetPath = $"{path}\\{name}";
            _sourceDirectory.CopyTo(_targetPath);
            _targetDirectory = new DirectoryInfo(_targetPath);
            _targetDirectory.GetSubdirectory(_sourceName).FileRename(_sourceName+".sln", name+".sln");
            var solutionFile =_targetDirectory.GetSubdirectory(_sourceName).GetFile(name + ".sln");
            var solutionText = File.ReadAllText(solutionFile.FullName);
            File.WriteAllText(solutionFile.FullName, solutionText.Replace(_sourceName, name));
            _targetDirectory.GetSubdirectory(_sourceName).SubdirectoryRename(_sourceName, name);
            _targetDirectory.GetSubdirectory(_sourceName).GetSubdirectory(name).FileRename(_sourceName + ".csproj", name + ".csproj");
            var programFile = _targetDirectory.GetSubdirectory(_sourceName).GetSubdirectory(name).GetFile("Program.cs");
            var programText = File.ReadAllText(programFile.FullName);
            File.WriteAllText(programFile.FullName, programText.Replace(_sourceName, name));
            _targetDirectory.SubdirectoryRename(_sourceName, name);
            if (foldersMake == "Y")
            {
                CreateDefaultFolders();
            }
        }

        private void CreateDefaultFolders()
        {
            foreach (var item in _folderNames)
            {
                var folders = new DirectoryInfo(@$"{_targetPath}\{_projName}\{_projName}\{item}");
                if (!folders.Exists)
                {
                    folders.Create();
                }
            }
        }
    }
}
