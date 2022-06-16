﻿using ReactiveUI;
using System;

using LookScoreAdmin.Command;
using LookScoreCommon.Model;
using LookScoreServer.Repository;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class ClubEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private Club _club;
        private ClubRepository _clubRepository;
        private string[] _countries;

        #endregion

        public ClubEditorViewModel()
        {
            Club = new Club();

            Countries = new string[] { "Germany", "England", "Spain" };
        }

        #region Public Properties

        public Club Club
        {
            get => _club;
            set => this.RaiseAndSetIfChanged(ref _club, value);
        }

        public string[] Countries
        {
            get => _countries;
            set => this.RaiseAndSetIfChanged(ref _countries, value);
        }

        #endregion

        #region Functions

        private void SaveClub()
        {
            _clubRepository = new ClubRepository();

            _clubRepository.Insert(Club);

            BackToPreviousView();
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

        #endregion
    }
}
