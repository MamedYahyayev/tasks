using LookScoreCommon.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace LookScoreCommon.View
{
    /// <summary>
    /// Interaction logic for ScoreBoardView.xaml
    /// </summary>
    public partial class ScoreBoardView : UserControl, INotifyPropertyChanged
    {
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

        private bool _toggleExtraTimeAddVisibility;
        public bool ToggleExtraTimeAddVisibility
        {
            get => _toggleExtraTimeAddVisibility;
            set
            {
                _toggleExtraTimeAddVisibility = value;
                OnPropertyChanged(nameof(ToggleExtraTimeAddVisibility));
            }

        }

        #endregion

        public ScoreBoardView()
        {
            InitializeComponent();
        }
    }
}
