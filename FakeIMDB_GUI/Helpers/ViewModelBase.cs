using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace FakeIMDB_GUI.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, object> properties = new();

        public event PropertyChangedEventHandler PropertyChanged;

        private void Raise(PropertyChangedEventHandler eventHandler, string propertyName = null, [CallerMemberName] string autoPropertyName = null)
        {
            if (eventHandler == null)
            {
                return;
            }

            if (propertyName == null)
            {
                propertyName = autoPropertyName;
            }
            try
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (IndexOutOfRangeException)
            {
                // needed to avoid problems because of lazy binding in case of collection was expected
                // but not in place yet
            }
        }

        protected void RaisePropertyChanged(string propertyName = null, [CallerMemberName] string autoPropertyName = null)
        {
            if (propertyName == null)
            {
                propertyName = autoPropertyName;
            }
            Raise(PropertyChanged, propertyName);
        }

        protected bool SetBackingValue<TProperty>(TProperty value, Func<TProperty, IEnumerable<string>> validator = null, [CallerMemberName] string autoPropertyName = null)
        {
            if (autoPropertyName == null)
            {
                throw new ArgumentNullException(nameof(autoPropertyName));
            }

            if (!properties.TryGetValue(autoPropertyName, out object existing) || !EqualityComparer<TProperty>.Default.Equals((TProperty)existing, value))
            {
                properties[autoPropertyName] = value;
                RaisePropertyChanged(autoPropertyName);
                
                if(validator != null)
                {
                    ClearErrors(autoPropertyName);
                    var listOfErrorThrown = validator(value);

                    foreach (var error in listOfErrorThrown)
                        AddError(autoPropertyName, error);
                }
                return true;
            }
            return false;
        }

        protected TProperty GetBackingValue<TProperty>([CallerMemberName] string autoPropertyName = null)
        {
            if (autoPropertyName == null)
            {
                throw new ArgumentNullException(nameof(autoPropertyName));
            }

            return properties.TryGetValue(autoPropertyName, out var value) ? (TProperty)value : default;
        }


        #region INotifyDataErrorInfo implementation
        public bool HasErrors => errorsByPropertyName.Any();

        private readonly Dictionary<string, List<string>> errorsByPropertyName = new();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorsByPropertyName.ContainsKey(propertyName) ? errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName)
        {
            if (errorsByPropertyName.ContainsKey(propertyName))
            {
                errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        private void AddError(string propertyName, string error)
        {
            if (!errorsByPropertyName.ContainsKey(propertyName))
                errorsByPropertyName[propertyName] = new List<string>();

            if (!errorsByPropertyName[propertyName].Contains(error))
            {
                errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
        #endregion INotifyDataErrorInfo implementation
    }
}