using Toolkit;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Bindings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MyObservable<int> CountPage1 { get; }
        public MyObservable<int> CountVM1 { get; }
        public MyObservable<int> CountClassA { get; }
        public MainPage()
        {
            this.InitializeComponent();
            CountPage1 = new MyObservable<int>(0);
            CountVM1 = new MyObservable<int>(0);
            CountClassA = new MyObservable<int>(0);
            Current = this;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            content.GoBack();
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            content.GoForward();
        }

        
        private void NavigateToPage1(object sender, RoutedEventArgs e)
        {
            content.Navigate(typeof(Page1));
        }
        private void NavigateToPage2(object sender, RoutedEventArgs e)
        {
            content.Navigate(typeof(Page2));
        }

        private void GC(object sender, RoutedEventArgs e)
        {
            System.GC.Collect();
        }

        public static MainPage Current { get; private set; }
    }
}
