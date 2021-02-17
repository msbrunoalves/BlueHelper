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
        }

        private void StartupProcedures()
        {
            //Define initial window position (0,0) = top left corner
            Left = 0;
            Top = 0;
            //Next calculate the position to be on the corner of the taskbar
            PositionWindowRelativeToTaskbar();
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
        
    }
}
