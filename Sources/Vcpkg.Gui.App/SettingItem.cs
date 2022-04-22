using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Gui.App
{
    // 简单的配值对
    public enum SettingItemType
    {
        Text,
        Toggle,
    }
    public class SettingItem : UserControl
    {
        public SettingItem(SettingItemType itemType, String keyName, object defaultValue)
        {
            KeyItem = new SettingKeyItem()
            {
                ItemKey = keyName,
            };
            switch (itemType)
            {
                case SettingItemType.Text:
                    ValueItem = new TextSettingValueItem();
                    ValueItem.ItemValue = defaultValue;

                    break;
                case SettingItemType.Toggle:
                    ValueItem = new ToggleSettingValueItem();
                    ValueItem.ItemValue = defaultValue;
                    break;
            }

            var rootPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    KeyItem, ValueItem
                }
            };
            Content = rootPanel;
        }

        public SettingKeyItem KeyItem { get; }
        public ISettingValueItem ValueItem { get; }
    }



    public class SettingKeyItem : UserControl
    {
        public SettingKeyItem()
        {
            _keyTextBlock = new TextBlock()
            {
                Padding = new Thickness(4),
                Width = 192,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Content = _keyTextBlock;
        }
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
        private TextBlock _keyTextBlock { get; }
    }
    public interface ISettingValueItem : IControl
    {
        public object ItemValue { get; set; }
    }

    public class ToggleSettingValueItem : UserControl, ISettingValueItem
    {
        public ToggleSettingValueItem()
        {
            Padding = new Thickness(4);
            _toggleSwitch = new ToggleSwitch();
            Content = _toggleSwitch;
        }
        public object ItemValue
        {
            get
            {
                return _toggleSwitch.IsChecked;
            }
            set
            {
                _toggleSwitch.IsChecked = (bool)value;
            }
        }

        private ToggleSwitch _toggleSwitch;
    }
    public class TextSettingValueItem : UserControl, ISettingValueItem
    {
        public TextSettingValueItem()
        {
            Padding = new Thickness(4);
            _textBox = new TextBox()
            {
                Width = 256
            };
            Content = _textBox;
        }
        public object ItemValue
        {
            get
            {
                return _textBox.IsEnabled;
            }
            set
            {
                _textBox.Text = (String)value;
            }
        }

        private TextBox _textBox;
    }
    public class ComboSettingValueItem : UserControl, ISettingValueItem
    {
        public object ItemValue
        {
            get
            {
                return _comboBox.IsEnabled;
            }
            set
            {
                _comboBox.SelectedItem = value;
            }
        }

        private ComboBox _comboBox;
    }
}
