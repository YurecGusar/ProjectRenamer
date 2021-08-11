using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ProjectRenamer
{
    public class Creator
    {
        private readonly string _sourceName;
        private string _projName;
        private string _targetPath;
        private string[] _folderNames;
        private ConfigService _config = new ConfigService();
        private DirectoryInfo _sourceDirectory;
        private DirectoryInfo _targetDirectory;

        public Creator()
        {
            _folderNames = _config.FolderNames;
            _sourceName = _config.SourceName;
            _sourceDirectory = new DirectoryInfo(_sourceName);
        }

        public void MakeProject(string name, string path, string foldersFlag)
        {
            _projName = name;            
            _targetPath = GetPath(path);

            _sourceDirectory.CopyTo(_targetPath);
            _targetDirectory = new DirectoryInfo(_targetPath);
            _targetDirectory.GetSubdirectory(_sourceName).FileRename($"{_sourceName}.sln", $"{_projName}.sln");
            var solutionFile =_targetDirectory.GetSubdirectory(_sourceName).GetFile($"{_projName}.sln");
            var solutionText = File.ReadAllText(solutionFile.FullName);
            File.WriteAllText(solutionFile.FullName, solutionText.Replace(_sourceName, _projName));
            _targetDirectory.GetSubdirectory(_sourceName).SubdirectoryRename(_sourceName, _projName);
            _targetDirectory.GetSubdirectory(_sourceName).GetSubdirectory(_projName).FileRename($"{_sourceName}.csproj", $"{name}.csproj");
            var programFile = _targetDirectory.GetSubdirectory(_sourceName).GetSubdirectory(_projName).GetFile("Program.cs");
            var programText = File.ReadAllText(programFile.FullName);
            File.WriteAllText(programFile.FullName, programText.Replace(_sourceName, _projName));
            _targetDirectory.SubdirectoryRename(_sourceName, _projName);
            AddFolders(foldersFlag.ToUpper());
        }

        private string GetPath(string path)
        {
            if (path == String.Empty)
            {
                path = _sourceDirectory.Parent.FullName;
            }

            return @$"{path}\{_projName}";
        }

        private void AddFolders(string folderrsFlag)
        {
            while (!CreateDefaultFolders(folderrsFlag))
            {
                Console.Write("You entered an invalid key, please try again (Y/N): ");
                folderrsFlag = Console.ReadLine();
                CreateDefaultFolders(folderrsFlag.ToUpper());
            }
        }
        private bool CreateDefaultFolders(string foldersMake)
        {
            if (foldersMake == "Y")
            {
                foreach (var item in _folderNames)
                {
                    var folders = new DirectoryInfo(@$"{_targetPath}\{_projName}\{_projName}\{item}");
                    if (!folders.Exists)
                    {
                        folders.Create();
                    }
                }

                return true;
            }
            else 
            {
                if (foldersMake == "N")
                {
                    return true;
                }

                return false;
            }
        }
    }
}
