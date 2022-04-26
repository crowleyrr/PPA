using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PPA.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class CalendarBaseViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

       /* public bool IsBusy
        {
            get { return IsBusy; }
            set
            {
                IsBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        } */

        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
