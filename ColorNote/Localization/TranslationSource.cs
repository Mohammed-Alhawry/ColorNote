using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace ColorNote.Localization;

public class TranslationSource
    : INotifyPropertyChanged
{
    private static readonly TranslationSource _instance = new();

    public static TranslationSource Instance => _instance;


    private readonly ResourceManager _resManager = Resources.Strings.ResourceManager;
    private CultureInfo _currentCulture;

    public string this[string key] => _resManager.GetString(key, _currentCulture);
    public event PropertyChangedEventHandler PropertyChanged;

    public CultureInfo CurrentCulture
    {
        get { return _currentCulture; }
        set
        {
            if (_currentCulture != value)
            {
                _currentCulture = value;
                var @event = this.PropertyChanged;
                if (@event != null)
                {
                    @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                }
            }
        }
    }

}