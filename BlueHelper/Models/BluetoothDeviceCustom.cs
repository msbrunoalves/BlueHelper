using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHelper.Models
{
    public class BluetoothDeviceCustom
    {
        public BluetoothDeviceInfo BluetoothDeviceInfo { get; set; }
        public string DeviceTypeString { get; set; }
        public bool DeviceTypeEdited { get; set; } = false;
        public int BateryLevel { get; set; } = -1;
        public string Description { get; set; }
        public bool DescriptionEdited { get; set; } = false;
    }

    /*
    public enum DeviceType
    {
        [Description("Default")]
        Default = 0,
        //devices
        [Description("Phone")]
        Phone = 1,
        [Description("Tablet")]
        Tablet = 2,
        [Description("Watch")]
        Watch = 3,
        //sound
        [Description("Earbuds")]
        Earbuds = 11,
        [Description("Headphones")]
        Headphones = 12,
        [Description("Speakers")]
        Speakers = 13,
        //peripherals
        [Description("Keyboard")]
        Keyboard = 21,
        [Description("Phone")]
        Mouse = 22 
    }
    */
}
