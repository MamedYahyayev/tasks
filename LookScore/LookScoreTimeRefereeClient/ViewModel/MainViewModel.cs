using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Constants;
using LookScoreCommon.Enums;
using LookScoreServer.Model.Entity;
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

        private Game[] _games;
        private Game _selectedGame;
        private GameStatistics _currentGameStatistics;
        private Team _ballOwnerTeam;


        #region TIme Related Private Properties

        private int _seconds = 44 * 60 + 55;
        private int _extraSeconds;
        private int _extraMinute;
        private bool _isTimerStart;
        private bool _isExtraTimeStart;

        #endregion

        #endregion


        public MainViewModel()
        {
            var callback = new GameStatisticsCallback();
            callback.StatisticsChanged += OnStatisticsChanged;

            InstanceContext callbackLocation = new InstanceContext(callback);
            _statisticServiceChannel = new DuplexChannelFactory<IStatisticService>(callbackLocation, "StatisticService").CreateChannel();
            _statisticServiceChannel.JoinToChannel();

            ChannelFactory<IGameService> channelFactory = new ChannelFactory<IGameService>("GameService");
            IGameService gameService = channelFactory.CreateChannel();

            Games = gameService.FindAllGameDetails();
        }

        #region Public Properties

        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

        public GameStatistics CurrentGameStatistics
        {
            get => _currentGameStatistics;
            set => this.RaiseAndSetIfChanged(ref _currentGameStatistics, value);
        }

        public Team BallOwnerTeam
        {
            get => _ballOwnerTeam;
            set => this.RaiseAndSetIfChanged(ref _ballOwnerTeam, value);
        }

        #region Time Related Public Properties

        public int Seconds
        {
            get => _seconds;
            set => this.RaiseAndSetIfChanged(ref _seconds, value);
        }

        public int ExtraSeconds
        {
            get => _extraSeconds;
            set => this.RaiseAndSetIfChanged(ref _extraSeconds, value);
        }

        public bool IsExtraTimeStart
        {
            get => _isExtraTimeStart;
            set => this.RaiseAndSetIfChanged(ref _isExtraTimeStart, value);
        }

        public int ExtraMinute
        {
            get => _extraMinute;
            set => this.RaiseAndSetIfChanged(ref _extraMinute, value);
        }

        #endregion


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
                BallOwnerTeam = Team.HOME;
            }
            else
            {
                BallOwnerTeam = Team.GUEST;
            }

        }

        private void StartTimer()
        {
            _isTimerStart = true;

            Task.Run(async () =>
            {
                while (_isTimerStart)
                {
                    if (GameConstants.FIRST_HALF_IN_SECONDS == Seconds || GameConstants.BOTH_HALF_IN_SECONDS == Seconds)
                    {
                        _isTimerStart = false;
                        StartExtraTime();
                        return;
                    }

                    Seconds += 1;
                    await Task.Delay(1000);
                }
            });
        }

        private void StartExtraTime()
        {
            IsExtraTimeStart = true;

            Task.Run(async () =>
            {
                while (IsExtraTimeStart)
                {
                    ExtraSeconds += 1;
                    await Task.Delay(1000);
                }
            });
        }

        private void StopTimer()
        {
            _isTimerStart = false;
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


        #endregion

        #region Events

        protected virtual void OnStatisticsChanged(object source, StatisticEventArgs args)
        {
            CurrentGameStatistics = args.GameStatistics;
        }

        #endregion
    }
}
