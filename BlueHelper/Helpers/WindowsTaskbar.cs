using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueHelper.Helpers
{
    public class WindowsTaskbar
    {
        // Must match AppBarEdge enum
        public enum TaskBarLocation { TOP, BOTTOM, LEFT, RIGHT }

        public TaskBarLocation GetTaskBarLocation()
        {
            TaskBarLocation taskBarLocation = TaskBarLocation.BOTTOM;
            bool taskBarOnTopOrBottom = (Screen.PrimaryScreen.WorkingArea.Width == Screen.PrimaryScreen.Bounds.Width);
            if (taskBarOnTopOrBottom)
            {
                if (Screen.PrimaryScreen.WorkingArea.Top > 0) taskBarLocation = TaskBarLocation.TOP;
            }
            else
            {
                if (Screen.PrimaryScreen.WorkingArea.Left > 0)
                {
                    taskBarLocation = TaskBarLocation.LEFT;
                }
                else
                {
                    taskBarLocation = TaskBarLocation.RIGHT;
                }
            }
            return taskBarLocation;
        }

        internal static double GetTaskbarHeight(TaskBarLocation location)
        {
            switch (location)
            {
                case TaskBarLocation.TOP:
                    return Screen.PrimaryScreen.Bounds.Top - Screen.PrimaryScreen.WorkingArea.Top;
                case TaskBarLocation.BOTTOM:
                    return Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom;
                case TaskBarLocation.LEFT:
                    return Screen.PrimaryScreen.Bounds.Left - Screen.PrimaryScreen.WorkingArea.Left;
                case TaskBarLocation.RIGHT:
                    return Screen.PrimaryScreen.Bounds.Right - Screen.PrimaryScreen.WorkingArea.Right;
            }
            return 0;
        }
    }
}
