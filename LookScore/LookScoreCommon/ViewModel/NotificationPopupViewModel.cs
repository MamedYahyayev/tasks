using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace LookScoreCommon.ViewModel
{
    public class NotificationPopupViewModel : ReactiveObject
    {

        public NotificationPopupViewModel()
        {
        }

        #region Public Properties

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        private string _iconColor;
        public string IconColor
        {
            get => _iconColor;
            set => this.RaiseAndSetIfChanged(ref _iconColor, value);
        }

        #endregion

        #region Functions

        public void GetSuccessDesign(string title, int visibleTimeInSeconds)
        {
            IsPopupOpen = true;
            Title = title;
            Icon = "Check";
            IconColor = "Green";

            ClosePopupAfterDelay(visibleTimeInSeconds);
        }

        public void GetFailedDesign(string title, int visibleTimeInSeconds)
        {
            IsPopupOpen = true;
            Title = title;
            Icon = "Close";
            IconColor = "Red";

            ClosePopupAfterDelay(visibleTimeInSeconds);
        }

        private void ClosePopupAfterDelay(int seconds)
        {
            Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                IsPopupOpen = false;
            });
        }

        #endregion

    }
}
