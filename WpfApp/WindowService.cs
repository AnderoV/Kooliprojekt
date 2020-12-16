using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp
{   
        public interface IWindowService
        {
            void ShowWindow<T>(object DataContext) where T : Window, new();
            bool ShowDialog<T>(object DataContext) where T : Window, new();
        }

        public class WindowService : IWindowService
        {
            public void ShowWindow<T>(object DataContext) where T : Window, new()
            {
                var window = new T();
                window.DataContext = DataContext;
                window.Show();
            }

            public bool ShowDialog<T>(object DataContext) where T : Window, new()
            {
                var window = new T();
                window.DataContext = DataContext;
                var result = window.ShowDialog();
                return (result.HasValue && result.Value);
            }
        }
}
