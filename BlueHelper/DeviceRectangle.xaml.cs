using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueHelper
{
    /// <summary>
    /// Interaction logic for DeviceRectangle.xaml
    /// </summary>
    public partial class DeviceRectangle : UserControl
    {


        public object BluetoothDevice
        {
            get { return (object)GetValue(BluetoothDeviceProperty); }
            set { SetValue(BluetoothDeviceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BluetoothDevice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BluetoothDeviceProperty =
            DependencyProperty.Register("BluetoothDevice", typeof(object), typeof(DeviceRectangle), new PropertyMetadata(0));


        public DeviceRectangle()
        {
            InitializeComponent();
        }
    }
}
