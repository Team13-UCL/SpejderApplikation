﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged // Abstraktion som implementere INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // [CallerMemberName] gør at man ikke behøver at skrive property name, når man kalder metoden.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
