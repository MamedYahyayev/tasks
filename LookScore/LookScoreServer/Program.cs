using LookScoreCommon.Enums;
using LookScoreInterfaces.Exceptions;
using LookScoreInterfaces.Util;
using LookScoreServer.Service.FileServices;
using LookScoreServer.Service.WCFServices;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LookScoreServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileType = ConfigurationManager.AppSettings.Get("fileType");
            if (string.IsNullOrEmpty(fileType)) throw new FileTypeNotConfiguredException("File Type doesn't exist in config file!");

            var isValid = FileValidator.IsValidFileType(fileType);
            if (!isValid) throw new UnsupportedFileTypeException("Invalid File Type, please use json or xml file type");

            FileType fileService = (FileType)FileHelper.GetFileType(fileType);

            DataService.InitInstance(FileServiceFactory.CreateFileService(fileService));

            ServiceHost serviceHost = new ServiceHost(typeof(GameService));
            ServiceHost clubServiceHost = new ServiceHost(typeof(ClubService));
            ServiceHost statisticServiceHost = new ServiceHost(typeof(StatisticService));

            serviceHost.Open();
            clubServiceHost.Open();
            statisticServiceHost.Open();


            Console.Read();

            serviceHost.Close();
            clubServiceHost.Close();
            statisticServiceHost.Close();

        }
    }
}
