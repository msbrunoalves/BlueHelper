using BlueHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Bluetooth;
using System.Diagnostics;
using System.Threading;
using BlueHelper.Models;
using System.Collections.ObjectModel;

namespace BlueHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartupProcedures();
            FillDeviceTreeView();
        }

        private void StartupProcedures()
        {
            //Define initial window position (0,0) = top left corner
            Left = 0;
            Top = 0;
            //Next calculate the position to be on the corner of the taskbar
            PositionWindowRelativeToTaskbar();
        }

        //Run this on wait (async), takes a while
        private BluetoothDeviceInfo[] DiscoverBluetoothDevices()
        {
            //preencher o Datagrid com a lista de dispositivos bluetooth
            BluetoothClient client = new BluetoothClient();
            List<string> items = new List<string>();
            return client.DiscoverDevices().ToArray();
        }

        private void PositionWindowRelativeToTaskbar()
        {
            WindowsTaskbar taskbar = new WindowsTaskbar();
            switch (taskbar.GetTaskBarLocation())
            {
                case WindowsTaskbar.TaskBarLocation.LEFT:
                    Left = -WindowsTaskbar.GetTaskbarHeight(taskbar.GetTaskBarLocation());
                    Top = SystemParameters.PrimaryScreenHeight - Height;
                    break;
                case WindowsTaskbar.TaskBarLocation.RIGHT:
                    Left = SystemParameters.PrimaryScreenWidth - (Width + WindowsTaskbar.GetTaskbarHeight(taskbar.GetTaskBarLocation()));
                    Top = SystemParameters.PrimaryScreenHeight - Height;
                    break;
                case WindowsTaskbar.TaskBarLocation.TOP:
                    Left = SystemParameters.PrimaryScreenWidth - Width;
                    Top = -WindowsTaskbar.GetTaskbarHeight(taskbar.GetTaskBarLocation());
                    break;
                case WindowsTaskbar.TaskBarLocation.BOTTOM:
                    Left = SystemParameters.PrimaryScreenWidth - Width;
                    Top = SystemParameters.PrimaryScreenHeight - (Height + WindowsTaskbar.GetTaskbarHeight(taskbar.GetTaskBarLocation()));
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsTaskbar taskbar = new WindowsTaskbar();

            PrimaryScreenHeightValue.Content = SystemParameters.PrimaryScreenHeight.ToString();
            PrimaryScreenWidthValue.Content = SystemParameters.PrimaryScreenWidth.ToString();
            WorkAreaHeightValue.Content = SystemParameters.WorkArea.Top.ToString();
            WorkAreaWidthValue.Content = SystemParameters.WorkArea.Left.ToString();
            TopValue.Content = Top.ToString();
            LeftValue.Content = Left.ToString();
            TaskbarHeight.Content = WindowsTaskbar.GetTaskbarHeight(taskbar.GetTaskBarLocation()).ToString();
            TaskbarPosition.Content = taskbar.GetTaskBarLocation().ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            Close();
        }

        protected override void OnActivated(EventArgs e)
        {
            //StartupProcedures(); For debug only
            this.BringIntoView();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            FillDeviceTreeView();
        }

        private async void FillDeviceTreeView()
        {
            //Bloquear o botão de refresh para evitar spam de threads
            refreshBtn.IsEnabled = false;
            refreshProgressBar.IsIndeterminate = true;            //necessário para parar a progressbar de usar CPU mesmo escondida
            refreshProgressBar.Visibility = Visibility.Visible;
            BluetoothDeviceInfo[] devices = await Task.Factory.StartNew(() => DiscoverBluetoothDevices(), TaskCreationOptions.LongRunning);
            //Limpar primeiro o grid view
            deviceGridView.Items.Clear();
            foreach (BluetoothDeviceInfo d in devices)
            {
                //Colocar o dispositivo no Grid View
                BluetoothDeviceCustom bluetoothDevice = new BluetoothDeviceCustom()
                {
                    BluetoothDeviceInfo = d,
                    DeviceTypeString = d.ClassOfDevice.MajorDevice.ToString(),
                    BateryLevel = 69,
                };
                deviceGridView.Items.Add(bluetoothDevice);
            }

            //Ativar novamente o botão de refresh
            refreshBtn.IsEnabled = true;
            refreshProgressBar.IsIndeterminate = false;            //necessário para parar a progressbar de usar CPU mesmo escondida
            refreshProgressBar.Visibility = Visibility.Hidden;
        }
    }
}
