using System.Configuration;
using System.ServiceModel;
using System.Windows;

namespace LookScoreAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var fileType = ConfigurationManager.AppSettings.Get("fileType");
            //if (string.IsNullOrEmpty(fileType)) throw new FileTypeNotConfiguredException("File Type doesn't exist in config file!");

            //var isValid = FileValidator.IsValidFileType(fileType);
            //if (!isValid) throw new UnsupportedFileTypeException("Invalid File Type, please use json or xml file type");

            //FileType fileService = FileHelper.GetFileType(fileType);

            //DataService.InitInstance(FileServiceFactory.CreateFileService(fileService));

            base.OnStartup(e);
        }
    }
}
