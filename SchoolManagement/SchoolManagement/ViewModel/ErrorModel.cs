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

        //public bool HasError {get; set;}
        //public string ErrorMessage {get; set;}

        #endregion


        #region Functions

        public void ClearError()
        {
            ErrorMessage = string.Empty;
            HasError = false;
        }

        #endregion

    }
}
