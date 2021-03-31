using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Bindings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        private PageVM1 vm;

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //vm = null;
            //Bindings.StopTracking();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (vm is null)
            {
                vm = new PageVM1();
                Bindings.Initialize();
            }
        }
        public Page1()
        {
            this.InitializeComponent();
            MainPage.Current.CountPage1.Value++;
            vm = new PageVM1();
        }


        ~Page1()
        {
            MainPage.Current.CountPage1.Value--;
        }
    }
}
