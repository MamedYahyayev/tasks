using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Model;
using LookScoreCommon.ViewModel;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using ReactiveUI;
using System;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LookScoreViewerClient.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; private set; }

        public MainViewModel()
        {
            Instance = this;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Config",
                DefaultExt = ".reg",
                Filter = "Registry files (.reg)|*.reg",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Windows Registry Editor Version 5.00");
                sb.AppendLine();
                sb.AppendLine(@"[HKEY_CURRENT_USER\Software\RISK\Aran]");
                sb.AppendLine("\"UserId\"=\"" + 1 + "\"");
                sb.AppendLine("\"ServiceAddress\"=\"" + 2 + ":8523" + "\"");
                sb.AppendLine("\"HelperAddress\"=\"" + 3 + ":8524" + "\"");
                sb.AppendLine("\"StorageName\"=\"" + 4 + "\"");
                sb.AppendLine("\"Serial\"=\"" + 5 + "\"");

                var writer = new StreamWriter(saveFileDialog.FileName);
                writer.Write(sb.ToString());
                writer.Close();
            };

            CurrentView = new GameListViewModel();
        }

        #region Public Properties

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        #endregion

        #region Functions

        public void SetCurrentView(BaseViewModel viewModel)
        {
            CurrentView = viewModel;
        }

        #endregion
    }
}
