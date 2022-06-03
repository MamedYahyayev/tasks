using GalaSoft.MvvmLight.Command;
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
        private int _seconds = 0;
        private bool _isTimerStart;

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

        public int Seconds
        {
            get => _seconds;
            set => this.RaiseAndSetIfChanged(ref _seconds, value);
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
                    Seconds += 1;
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
