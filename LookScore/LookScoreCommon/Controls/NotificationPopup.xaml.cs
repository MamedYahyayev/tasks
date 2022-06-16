using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for NotificationPopup.xaml
    /// </summary>
    public partial class NotificationPopup : UserControl
    {
        public NotificationPopup()
        {
            InitializeComponent();
        }


        #region Dependency Properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NotificationPopup), new PropertyMetadata(""));


        public string IconColor
        {
            get { return (string)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor", typeof(string), typeof(NotificationPopup), new PropertyMetadata(""));


        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(NotificationPopup), new PropertyMetadata(""));



        #endregion

    }
}
