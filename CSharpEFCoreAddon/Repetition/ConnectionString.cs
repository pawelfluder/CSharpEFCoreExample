using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpEFCoreExample.Repetition
{
    public class ConnectionString
    {
        private readonly FileService fileService;

        public ConnectionString(FileService fileService)
        {
            this.fileService = fileService;
        }

        public string Get()
        {
            var projectName = "CSharpEFCoreExample";
            var startupProjectFolder = fileService.Path.GetProjectFolderPath(projectName);
            var upFolder = fileService.Path.MoveDirectoriesUp(startupProjectFolder, 1);
            var dbFolder = upFolder + "/" + "Database";
            var success = Directory.CreateDirectory(dbFolder);
            var dbFileName = "MyCoolDataBase.db";
            var dbFilePath = dbFolder + "/" + dbFileName;
            dbFilePath = dbFilePath.Replace('\\', '/');
            var connectionString = "Data Source=" + dbFilePath;
            return connectionString;
        }
    }
}
