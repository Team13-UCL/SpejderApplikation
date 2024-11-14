﻿using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpejderApplikation.View
{
    /// <summary>
    /// Interaction logic for ScoutsMeetingView.xaml
    /// </summary>
    public partial class ScoutsProgramView : Window
    {
        ScoutsProgramViewModel vm;
        public ScoutsProgramView()
        {
            var SMRepo = new Repository.ScoutsMeetingRepository();
            vm = new ScoutsProgramViewModel(SMRepo);
            InitializeComponent();
            DataContext = vm;
        }
    }
}
