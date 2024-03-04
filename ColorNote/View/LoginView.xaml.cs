using System.Windows.Controls;

namespace ColorNote.View;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var areBothCorrect = true;
        if (userNameBox.Text != "")
        {
            userNameErrorNotifierBox.Visibility = System.Windows.Visibility.Visible;
            areBothCorrect = false;
        }
        else
            userNameErrorNotifierBox.Visibility = System.Windows.Visibility.Collapsed;

        if (passwordBox.Password != "")
        {
            passwordErrorNotifierBox.Visibility = System.Windows.Visibility.Visible;
            areBothCorrect = false;
        }
        else
            passwordErrorNotifierBox.Visibility = System.Windows.Visibility.Collapsed;

        if (areBothCorrect)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
            
        }

    }
}
