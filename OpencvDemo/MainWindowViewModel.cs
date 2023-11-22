using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.ComponentModel;

namespace OpencvDemo
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty] BitmapSource? _mainImage;

        [ObservableProperty] string _width = string.Empty;

        [ObservableProperty] string _height = string.Empty;

        [ObservableProperty] string _channel = string.Empty;
    }
}
