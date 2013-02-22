using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Twainsoft.VSSettingsSwitcher.BLL.Models.Settings
{
    public class SettingsCategoryViewModel : INotifyPropertyChanged
    {
        bool? _isChecked = false;
        public SettingsCategoryViewModel _parent;

        public List<SettingsCategoryViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; set; }
        public string Name { get; private set; }
        public string RegisteredName { get; set; }

        public SettingsCategoryViewModel(string name, string registeredName)
            : this(name)
        {
            this.RegisteredName = registeredName;
        }

        public SettingsCategoryViewModel(string name)
        {
            this.Name = name;
            this.Children = new List<SettingsCategoryViewModel>();
        }

        public void Initialize()
        {
            foreach (SettingsCategoryViewModel child in this.Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                this.Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null)
                _parent.VerifyCheckState();

            this.OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Children.Count; ++i)
            {
                bool? current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

        public string GetFullRegisteredName()
        {
            if (_parent != null)
            {
                if (String.IsNullOrEmpty(_parent.GetFullRegisteredName()))
                {
                    return RegisteredName;
                }
                else
                {
                    return _parent.GetFullRegisteredName() + @"\" + RegisteredName;
                }
            }
            else
            {
                return "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
