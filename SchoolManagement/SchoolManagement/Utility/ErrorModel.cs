using ReactiveUI;

namespace SchoolManagement.ViewModel
{
    public class ErrorModel : ReactiveObject
    {
        #region Public Properties

        private bool _hasError;
        public bool HasError
        {
            get => _hasError;
            set => this.RaiseAndSetIfChanged(ref _hasError, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        #endregion
    }
}
