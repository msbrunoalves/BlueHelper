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

namespace BlueHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Delegate Signature
        public delegate void updateTreeView();

        ThreadStart threadStart;
        Thread updateThread;

        public MainWindow()
        {
            InitializeComponent();
            StartupProcedures();
            SetupAndStartBluetoothUpdateThread();
        }

        private void StartupProcedures()
        {
            //Define initial window position (0,0) = top left corner
            Left = 0;
            Top = 0;
            //Next calculate the position to be on the corner of the taskbar
            PositionWindowRelativeToTaskbar();
        }

        private void SetupAndStartBluetoothUpdateThread()
        {
            //Instantiate thread start object and pass in what Method it will call
            threadStart = new ThreadStart(StartBluetoothUpdateThread);
            //instantiate the thread and pass in the thread start object
            updateThread = new Thread(threadStart);
            //Name the thread
            updateThread.Name = "Bluetooth refresh";
            //Start the thread
            updateThread.Start();
        }

        //Method that the thread for refreshing bluetooth calls when starting
        private void StartBluetoothUpdateThread()
        {
            //Create a delegate variable and pass in what Method this delegate will call.
            updateTreeView UpdateTreeView = new updateTreeView(FillTreeView);
            this.Dispatcher.Invoke(UpdateTreeView);
        }

        private void FillTreeView()
        {
            //Limpar o TreeView primeiro
            deviceTreeView.Items.Clear();
            //preencher o Datagrid com a lista de dispositivos bluetooth
            BluetoothClient client = new BluetoothClient();
            List<string> items = new List<string>();
            BluetoothDeviceInfo[] devices = client.DiscoverDevices().ToArray();
            
            foreach (BluetoothDeviceInfo d in devices)
            {
                TreeViewItem newChild = new TreeViewItem();
                newChild.Header = d.DeviceName;
                newChild.ItemsSource = 
                    new string[] { 
                        "Address: " + d.DeviceAddress.ToString(),
                        "Connected: " + d.Connected.ToString(),
                        "Authenticated " + d.Authenticated.ToString()};
                deviceTreeView.Items.Add(newChild);
            }
            
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
            SetupAndStartBluetoothUpdateThread();
        }
    }
}
