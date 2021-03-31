using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Toolkit
{
    [DebuggerDisplay("Value = {Value}")]
    public class MyObservable<T> : DependencyObject, IObservable<T>
    {
        private T _value;

        public MyObservable(T value) => _value = value;

        public event PropertyChangedEventHandler PropertyChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _ = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                      {
                          _value = value;
                          PropertyChanged?.Invoke(this, Observables.ValuePropertyChangedEventArgs);
                      });

                }
            }
        }
    }

    public interface IObservable<T> : IReadOnlyObservable<T>
    {
        new T Value { get; set; }
    }

    public interface IReadOnlyObservable<out T> : INotifyPropertyChanged
    {
        T Value { get; }
    }

    public static class Observables
    {
        internal static readonly PropertyChangedEventArgs ValuePropertyChangedEventArgs =
            new PropertyChangedEventArgs(nameof(IReadOnlyObservable<string>.Value));
    }
}
