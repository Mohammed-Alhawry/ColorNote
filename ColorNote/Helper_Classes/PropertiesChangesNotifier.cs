using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ColorNote;

public class PropertiesChangesNotifier : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}