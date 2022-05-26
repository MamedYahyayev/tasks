using LookScoreInterfaces.Exceptions;
//using LookScoreInterfaces.Model.Enums;
//using LookScoreInterfaces.Service.FileServices;
using LookScoreInterfaces.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreInterfaces
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var fileType = ConfigurationManager.AppSettings.Get("fileType");
            //if (string.IsNullOrEmpty(fileType)) throw new FileTypeNotConfiguredException("File Type doesn't exist in config file!");

            //var isValid = FileValidator.IsValidFileType(fileType);
            //if (!isValid) throw new UnsupportedFileTypeException("Invalid File Type, please use json or xml file type");

            //FileType fileService = FileHelper.GetFileType(fileType);

            //DataService.InitInstance(FileServiceFactory.CreateFileService(fileService));

            //Console.ReadLine();
        }
    }
}
