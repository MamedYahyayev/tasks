using ReactiveUI;
using System;

using LookScoreAdmin.Command;
using LookScoreCommon.Model;
using System.Windows.Forms;
using LookScoreServer.Service.WCFServices;
using System.ServiceModel;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class ClubEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IClubService _clubService;

        #endregion

        public ClubEditorViewModel()
        {
            Club = new Club();
            Countries = new string[] { "Germany", "England", "Spain" };

            ChannelFactory<IClubService> channel = new ChannelFactory<IClubService>("ClubService");
            _clubService = channel.CreateChannel();
        }

        #region Public Properties

        private Club _club;
        public Club Club
        {
            get => _club;
            set => this.RaiseAndSetIfChanged(ref _club, value);
        }

        private string[] _countries;
        public string[] Countries
        {
            get => _countries;
            set => this.RaiseAndSetIfChanged(ref _countries, value);
        }

        #endregion

        #region Functions

        private void SaveClub()
        {
            _clubService.InsertClub(Club);

            BackToPreviousView();
        }

        private void UploadFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            Club.LogoUrl = fileDialog.FileName;
        }

        private void BackToPreviousView()
        {
            MainViewModel.Instance.SetCurrentViewModel(new ClubViewModel());
        }

        #endregion


        #region Commands

        private readonly VoidReactiveCommand _saveClubCommand;
        public VoidReactiveCommand SaveClubCommand =>
            _saveClubCommand ?? VoidReactiveCommand.Create(SaveClub);


        private readonly VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand ?? VoidReactiveCommand.Create(BackToPreviousView);

        private readonly VoidReactiveCommand _uploadFileCommand;
        public VoidReactiveCommand UploadFileCommand =>
            _uploadFileCommand ?? VoidReactiveCommand.Create(UploadFile);

        #endregion
    }
}
