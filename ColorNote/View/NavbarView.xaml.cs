using System.Threading;
using System.Windows.Controls;

namespace ColorNote.View
{
    /// <summary>
    /// Interaction logic for Navbar.xaml
    /// </summary>
    public partial class NavbarView : UserControl
    {
        public NavbarView()
        {
            InitializeComponent();
        }

        
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
            
            Thread.Sleep(250);
        }
    }
}
