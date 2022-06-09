using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using LookScoreTimeRefereeClient.Contract;
using ReactiveUI;
using System.ServiceModel;
using System.Threading.Tasks;

namespace LookScoreTimeRefereeClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly IStatisticService _statisticServiceChannel;
        private readonly IGameService _gameService;

        #endregion


        public MainViewModel()
        {
            var callback = new GameStatisticsCallback();
            callback.StatisticsChanged += OnStatisticsChanged;

            var gameCallback = new GameCallback();
            gameCallback.GameStarted += OnGameStarted;
            gameCallback.GameStopped += OnGameStopped;

            InstanceContext callbackLocation = new InstanceContext(callback);
            _statisticServiceChannel = new DuplexChannelFactory<IStatisticService>(callbackLocation, "StatisticService").CreateChannel();
            _statisticServiceChannel.JoinToChannel();

            InstanceContext gameCallbackInstance = new InstanceContext(gameCallback);
            DuplexChannelFactory<IGameService> channelFactory = new DuplexChannelFactory<IGameService>(gameCallbackInstance, "GameService");
            _gameService = channelFactory.CreateChannel();
            _gameService.JoinToChannel();

            Games = _gameService.FindAllGameDetails();
        }

        #region Public Properties

        private Game[] _games;
        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

        private GameStatistics _currentGameStatistics;
        public GameStatistics CurrentGameStatistics
        {
            get => _currentGameStatistics;
            set => this.RaiseAndSetIfChanged(ref _currentGameStatistics, value);
        }

        private Team _ballOwnerTeam = Team.UNSET;
        public Team BallOwnerTeam
        {
            get => _ballOwnerTeam;
            set => this.RaiseAndSetIfChanged(ref _ballOwnerTeam, value);
        }

        private bool _isBallOwnerTeamChanged;
        public bool IsBallOwnerTeamChanged
        {
            get => _isBallOwnerTeamChanged;
            set => this.RaiseAndSetIfChanged(ref _isBallOwnerTeamChanged, value);
        }

        private int _extraMinute;
        public int ExtraMinute
        {
            get => _extraMinute;
            set => this.RaiseAndSetIfChanged(ref _extraMinute, value);
        }

        private bool _toggleExtraTimeAddVisibility;
        public bool ToggleExtraTimeAddVisibility
        {
            get => _toggleExtraTimeAddVisibility;
            set => this.RaiseAndSetIfChanged(ref _toggleExtraTimeAddVisibility, value);
        }

        private bool _isGameStart;
        public bool IsGameStart
        {
            get => _isGameStart;
            set => this.RaiseAndSetIfChanged(ref _isGameStart, value);
        }

        private bool _isGameStop;
        public bool IsGameStop
        {
            get => _isGameStop;
            set => this.RaiseAndSetIfChanged(ref _isGameStop, value);
        }

        private int _homeTeamBallPossessionTime;
        public int HomeTeamBallPossessionTime
        {
            get => _homeTeamBallPossessionTime;
            set => this.RaiseAndSetIfChanged(ref _homeTeamBallPossessionTime, value);
        }

        private int _guestTeamBallPossessionTime;
        public int GuestTeamBallPossessionTime
        {
            get => _guestTeamBallPossessionTime;
            set => this.RaiseAndSetIfChanged(ref _guestTeamBallPossessionTime, value);
        }

        #endregion

        #region Functions

        private void GameChange()
        {
            if (SelectedGame != null)
            {
                CurrentGameStatistics = _statisticServiceChannel.FindGameStatistics(SelectedGame.Id);
            }
        }

        private void BallOwnerTeamChanged(Team team)
        {
            if (team == Team.HOME)
            {
                if (BallOwnerTeam == Team.HOME)
                {
                    return;
                }

                BallOwnerTeam = Team.HOME;
                IncreaseHomeTeamBallPossessionTime();
            }
            else
            {
                if (BallOwnerTeam == Team.GUEST)
                {
                    return;
                }

                BallOwnerTeam = Team.GUEST;
                IncreaseGuestTeamBallPossessionTime();
            }

        }

        private void DeactivateBallOwnerTeam()
        {
            BallOwnerTeam = Team.UNSET;
        }

        private void IncreaseHomeTeamBallPossessionTime()
        {
            Task.Run(async () =>
            {
                while (BallOwnerTeam == Team.HOME)
                {
                    HomeTeamBallPossessionTime += 1;
                    await Task.Delay(1000);
                }
            });
        }

        private void IncreaseGuestTeamBallPossessionTime()
        {
            Task.Run(async () =>
            {
                while (BallOwnerTeam == Team.GUEST)
                {
                    GuestTeamBallPossessionTime += 1;
                    await Task.Delay(1000);
                }
            });
        }


        private void StartTimer()
        {
            _gameService.StartGame(SelectedGame);
        }

        private void StopTimer()
        {
            _gameService.StopGame(SelectedGame);
        }

        private void ToggleExtraTimeVisibility()
        {
            ToggleExtraTimeAddVisibility = !ToggleExtraTimeAddVisibility;
        }

        #endregion


        #region Commands

        private RelayCommand _gameChangeCommand;
        public RelayCommand GameChangeComamnd
        {
            get
            {
                return _gameChangeCommand ?? (_gameChangeCommand =
                                 new RelayCommand(() => GameChange()));
            }
        }

        private RelayCommand<Team> _ballOwnerTeamChanged;
        public RelayCommand<Team> BallOwnerTeamChangedCommand
        {
            get
            {
                return _ballOwnerTeamChanged ?? (_ballOwnerTeamChanged =
                                 new RelayCommand<Team>((team) => BallOwnerTeamChanged(team)));
            }
        }

        private RelayCommand _deactivateBallOwnerTeamCommand;
        public RelayCommand DeactivateBallOwnerTeamCommand
        {
            get
            {
                return _deactivateBallOwnerTeamCommand ?? (_deactivateBallOwnerTeamCommand = 
                                new RelayCommand(() => DeactivateBallOwnerTeam()));
            }
        }

        private RelayCommand _startTimerCommand;
        public RelayCommand StartTimerCommand
        {
            get
            {
                return _startTimerCommand ?? (_startTimerCommand = new RelayCommand(() => StartTimer()));
            }
        }

        private RelayCommand _stopTimerCommand;
        public RelayCommand StopTimerCommand
        {
            get
            {
                return _stopTimerCommand ?? (_stopTimerCommand = new RelayCommand(() => StopTimer()));
            }
        }


        private RelayCommand _addExtraTimeCommand;
        public RelayCommand AddExtraTimeCommand
        {
            get
            {
                return _addExtraTimeCommand ?? (_addExtraTimeCommand = new RelayCommand(() => ToggleExtraTimeVisibility()));
            }
        }



        #endregion

        #region Events

        protected virtual void OnStatisticsChanged(object source, StatisticEventArgs args)
        {
            CurrentGameStatistics = args.GameStatistics;
        }

        protected virtual void OnGameStarted(object source, GameEventArgs args)
        {
            IsGameStart = true;
        }

        protected virtual void OnGameStopped(object source, GameEventArgs args)
        {
            IsGameStop = true;
        }

        #endregion
    }
}
