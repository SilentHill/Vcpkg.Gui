using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Gui.App
{
    public class SettingItem : UserControl
    {
        public String ItemKey
        {
            get
            {
                return _keyTextBlock.Text;
            }
            set
            {
                _keyTextBlock.Text = value;
            }
        }
        private TextBlock _keyTextBlock { get; set; } = new TextBlock();
    }

    public class ISettingValueItem
    {

    }


    public class ToogleSettingItem : UserControl
    {
        public ToogleSettingItem()
        {

        }
        private ToggleSwitch _toggleSwitch;
    }
}
