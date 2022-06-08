using LookScoreCommon.Constants;
using LookScoreCommon.Model;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LookScoreCommon.View
{
    /// <summary>
    /// Interaction logic for ScoreBoardView.xaml
    /// </summary>
    public partial class ScoreBoardView : UserControl, INotifyPropertyChanged
    {
        #region Private Properties

        private bool _isTimerStart;

        #endregion

        #region Dependency Properties

        public Game Game
        {
            get { return (Game)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(Game), typeof(ScoreBoardView));


        public GameStatistics GameStatistics
        {
            get { return (GameStatistics)GetValue(GameStatisticsProperty); }
            set { SetValue(GameStatisticsProperty, value); }
        }

        public static readonly DependencyProperty GameStatisticsProperty =
            DependencyProperty.Register("GameStatistics", typeof(GameStatistics), typeof(ScoreBoardView));


        public bool IsGameStart
        {
            get { return (bool)GetValue(IsGameStartProperty); }
            set { SetValue(IsGameStartProperty, value); }
        }

        public static readonly DependencyProperty IsGameStartProperty =
            DependencyProperty.Register("IsGameStart", typeof(bool), typeof(ScoreBoardView),
                new FrameworkPropertyMetadata()
                {
                    DefaultValue = false,
                    PropertyChangedCallback = (s, e) => { (s as ScoreBoardView).GameStart(); }
                });


        public bool IsGameStop
        {
            get { return (bool)GetValue(IsGameStopProperty); }
            set { SetValue(IsGameStopProperty, value); }
        }

        public static readonly DependencyProperty IsGameStopProperty =
            DependencyProperty.Register("IsGameStop", typeof(bool), typeof(ScoreBoardView),
                new FrameworkPropertyMetadata()
                {
                    DefaultValue = false,
                    PropertyChangedCallback = (s, e) => { (s as ScoreBoardView).GameStop(); }
                });


        #endregion

        #region Public Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }

        private int _extraSeconds;
        public int ExtraSeconds
        {
            get => _extraSeconds;
            set
            {
                _extraSeconds = value;
                OnPropertyChanged(nameof(ExtraSeconds));
            }
        }

        private bool _isExtraTimeStart;
        public bool IsExtraTimeStart
        {
            get => _isExtraTimeStart;
            set
            {
                _isExtraTimeStart = value;
                OnPropertyChanged(nameof(IsExtraTimeStart));
            }

        }

        private int _extraMinute;
        public int ExtraMinute
        {
            get => _extraMinute;
            set
            {
                _extraMinute = value;
                OnPropertyChanged(nameof(ExtraMinute));
            }
        }

        #endregion

        #region Constructors

        public ScoreBoardView()
        {
            InitializeComponent();



            if (IsGameStop)
            {
                StopTimer();
            }
        }

        #endregion

        #region Functions

        private void StartTimer()
        {
            if (_isTimerStart)
            {
                return;
            }

            _isTimerStart = true;

            if (ExtraSeconds > 0)
            {
                ResetExtraTime();
            }

            Task.Run(async () =>
            {
                while (_isTimerStart)
                {
                    Seconds += 1;
                    await Task.Delay(1000);

                    if (GameConstants.FIRST_HALF_IN_SECONDS == Seconds || GameConstants.BOTH_HALF_IN_SECONDS == Seconds)
                    {
                        _isTimerStart = false;
                        StartExtraTime();
                        return;
                    }
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

        private void ResetExtraTime()
        {
            IsExtraTimeStart = false;
            ExtraSeconds = 0;
        }

        private void StopTimer()
        {
            _isTimerStart = false;

            if (IsExtraTimeStart)
            {
                IsExtraTimeStart = false;
            }
        }

        private void GameStart()
        {
            if (IsGameStart)
            {
                StartTimer();
            }
        }

        private void GameStop()
        {
            if (IsGameStop)
            {
                StopTimer();
            }
        }

        #endregion
    }
}
