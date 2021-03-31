using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Toolkit;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FileBenchmark
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly string[] PropertyNames = new[]
        {
            FilePropertyNames.Duration,
            FilePropertyNames.SubTitle,
            FilePropertyNames.Year,
            FilePropertyNames.AlbumArtist,
            FilePropertyNames.AlbumTitle,
            FilePropertyNames.Artist,
            FilePropertyNames.Genre,
            FilePropertyNames.TrackNumber,
            FilePropertyNames.Title,
            FilePropertyNames.BitRate,
            FilePropertyNames.ItemType,
            FilePropertyNames.FrameRate,
            FilePropertyNames.FrameHeight,
            FilePropertyNames.FrameWidth,
            FilePropertyNames.ChannelCount,
        };

        private int loop = 100;
        private string _path = @"C:\Users\yifli\Music\Taylor Swift - Enchanted.mp3";

        public MyObservable<string> ResultWithNewTask { get; set; }
        public MyObservable<string> ResultWithoutNewTask { get; set; }
        public MyObservable<string> AwaitResultWithNewTask { get; set; }
        public MyObservable<string> AwaitResultWithoutNewTask { get; set; }
        public MainPage()
        {
            ResultWithNewTask = new MyObservable<string>("Results");
            ResultWithoutNewTask = new MyObservable<string>("Results");
            AwaitResultWithNewTask = new MyObservable<string>("Results");
            AwaitResultWithoutNewTask = new MyObservable<string>("Results");
            this.InitializeComponent();

            VM = new ViewModel
            {
                MyName = new MyObservable<string>("name")
            };
        }

        public ViewModel VM { get; }

        private void GetMetadataWithNewTask(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            var elapsed = new long[loop];
            for (var i = 0; i < loop; i++)
            {
                sw.Restart();
                GetMetadataWithNewTaskAsync();
                elapsed[i] = sw.ElapsedMilliseconds;
                
            }

            ResultWithNewTask.Value = $"avg: {elapsed.Average()}, min: {elapsed.Min()}, max: {elapsed.Max()}";
        }

        private void GetMetadataWithoutNewTask(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            var elapsed = new long[loop];
            for (var i = 0; i < loop; i++)
            {
                sw.Restart();
                _ = GetMetadataWithoutNewTaskAsync();
                elapsed[i] = sw.ElapsedMilliseconds;
                
            }

            ResultWithoutNewTask.Value = $"avg: {elapsed.Average()}, min: {elapsed.Min()}, max: {elapsed.Max()}";
        }

        private async void GetMetadataWithNewTaskAsync(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            var elapsed = new long[loop];
            for (var i = 0; i < loop; i++)
            {
                sw.Restart();
                await GetMetadataWithNewTaskAsync();
                elapsed[i] = sw.ElapsedMilliseconds;
                
            }

            AwaitResultWithNewTask.Value = $"avg: {elapsed.Average()}, min: {elapsed.Min()}, max: {elapsed.Max()}";
        }

        private async void GetMetadataWithoutNewTaskAsync(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            var elapsed = new long[loop];
            for (var i = 0; i < loop; i++)
            {
                sw.Restart();
                await GetMetadataWithoutNewTaskAsync();
                elapsed[i] = sw.ElapsedMilliseconds;
                
            }

            AwaitResultWithoutNewTask.Value = $"avg: {elapsed.Average()}, min: {elapsed.Min()}, max: {elapsed.Max()}";
        }

        public Task<IDictionary<string, object>> GetMetadataWithNewTaskAsync() => Task.Run(async () =>
        {
            var storageFile = await StorageFile.GetFileFromPathAsync(_path)
                .AsTask().ConfigureAwait(false);
            var properties = await storageFile.Properties.RetrievePropertiesAsync(PropertyNames)
                .AsTask().ConfigureAwait(false);
            return properties;
        });

        public async Task<IDictionary<string, object>> GetMetadataWithoutNewTaskAsync()
        {
            var storageFile = await StorageFile.GetFileFromPathAsync(_path)
                .AsTask().ConfigureAwait(false);
            var properties = await storageFile.Properties.RetrievePropertiesAsync(PropertyNames)
                .AsTask().ConfigureAwait(false);
            return properties;
        }

        public Task<IDictionary<string, object>> GetMetadataWithNewTaskAsync_Mock() => Task.Run(async () =>
        {
            await Task.Delay(10);
            return (IDictionary<string, object>)null;
        });

        public async Task<IDictionary<string, object>> GetMetadataWithoutNewTaskAsync_Mock()
        {
            await Task.Delay(10);
            return (IDictionary<string, object>)null;
        }

    }
}
