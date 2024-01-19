using System.Windows.Data;

namespace ColorNote.Localization;

public class LocExtension : Binding
{
    public LocExtension(string name) : base("[" + name + "]")
    {
        Mode = BindingMode.OneWay;
        Source = TranslationSource.Instance;
    }
}