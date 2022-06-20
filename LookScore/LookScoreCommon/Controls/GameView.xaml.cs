using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LookScoreCommon.Controls
{
    /// <summary>
    /// Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }


        #region Dependency Properties

        public Game Game
        {
            get { return (Game)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(Game), typeof(GameView), new PropertyMetadata(new Game()));


        public GameStatistics GameStatistics
        {
            get { return (GameStatistics)GetValue(GameStatisticsProperty); }
            set { SetValue(GameStatisticsProperty, value); }
        }

        public static readonly DependencyProperty GameStatisticsProperty =
            DependencyProperty.Register("GameStatistics", typeof(GameStatistics), typeof(GameView), new PropertyMetadata(new GameStatistics()));

        #endregion


    }
}
