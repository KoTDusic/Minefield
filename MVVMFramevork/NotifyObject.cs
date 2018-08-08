﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVMFramevork.Annotations;

namespace MVVMFramevork
{
    public class NotifyObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
