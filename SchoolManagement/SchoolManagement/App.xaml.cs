using SchoolManagement.Enum;
using SchoolManagement.Exceptions;
using SchoolManagement.Service;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var fileType = ConfigurationManager.AppSettings.Get("fileType");
            if (string.IsNullOrEmpty(fileType)) throw new FileTypeNotConfiguredException("File Type doesn't exist in config file!");

            var isValid = FileValidator.IsValidFileType(fileType);
            if (!isValid) throw new UnsupportedFileTypeException("Invalid File Type, please use json or xml file type");

            FileType fileService = FileHelper.GetDefaultFileType(fileType);

            var dataService = new DataService(new GeneralFileService().GetFileService(fileService));
            dataService.StartPersistence();

            base.OnStartup(e);
        }

    }
}
