// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SynoDs.Universal.Dtos;

namespace SynoDs.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginData = new LoginDto
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password,
                RememberMe = RememberMeSwitch.IsOn,
                Url = IpTextBox.Text,
                UseSsl = UseSslSwitch.IsOn
            };

        }
    }
}
