using System.Collections;
using System.ComponentModel;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Dictionary<string, List<string>> Errors = new();
        public bool HasErrors => Errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return Enumerable.Empty<string>();

            return Errors.TryGetValue(propertyName, out var error) ? error : Enumerable.Empty<string>();
        }

        protected void RemoveErrors(string propertyName)
        {
            Errors.Remove(propertyName);
        }

        protected void AddError(string propertyName, string errorMsg)
        {
            var propertyErrors = new List<string>();

            if (Errors.TryGetValue(propertyName, out var error))
                propertyErrors = error;
            else
                Errors.Add(propertyName, propertyErrors);

            propertyErrors.Add(errorMsg);
        }
        protected void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
