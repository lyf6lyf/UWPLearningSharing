// ------------------------------------------------------
// Copyright (C) Microsoft. All rights reserved.
// ------------------------------------------------------

using Toolkit;
using Windows.UI.Xaml;

namespace Bindings
{
    public class PageVM1 : DependencyObject
    {
        public PageVM1()
        {
            MainPage.Current.CountVM1.Value++;
            A = new MyObservable<ClassA>(new ClassA());
        }
        public MyObservable<ClassA> A { get; set; }

        ~PageVM1()
        {
            MainPage.Current.CountVM1.Value--;
        }
    }

    public class ClassA
    {
        public ClassA()
        {
            MainPage.Current.CountClassA.Value++;
        }

        ~ClassA()
        {
            MainPage.Current.CountClassA.Value--;
        }
    }
}