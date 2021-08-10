using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace ProjectRenamer
{
    public static class DirectoryExtensions
    {
        public static void CopyTo(this DirectoryInfo directory, string path)
        {
            var newDir = Directory.CreateDirectory(path);
            CopyAll(directory, newDir);
        }

        public static DirectoryInfo GetSubdirectory(this DirectoryInfo directory, string name)
        {
            foreach (var dir in directory.GetDirectories())
            {
                if (dir.Name == name)
                {
                    return dir;
                }
            }

            throw new DirectoryNotFoundException();
        }

        public static void SubdirectoryRename(this DirectoryInfo directory, string oldName, string newName)
        {
            var newPath = $"{directory.FullName}\\{newName}";
            directory.GetSubdirectory(oldName).MoveTo(newPath);
        }

        public static FileInfo GetFile(this DirectoryInfo directory, string name)
        {
            foreach (var file in directory.GetFiles())
            {
                if (file.Name == name)
                {
                    return file;
                }
            }

            throw new FileNotFoundException();
        }

        public static void FileRename(this DirectoryInfo directory, string oldName, string newName)
        {
            var path = $"{directory.FullName}\\{newName}";
            directory.GetFile(oldName).MoveTo(path);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Если директория для копирования файлов не существует, то создаем ее
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Копируем все файлы в новую директорию
            foreach (var fi in source.GetFiles())
            {
                // Выводим информацию о копировании в консоль
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Копируем рекурсивно все поддиректории
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                // Создаем новую поддиректорию в директории
                var nextTargetSubDir =
                  target.CreateSubdirectory(diSourceSubDir.Name);
                // Опять вызываем функцию копирования
                // Рекурсия
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
