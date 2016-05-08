﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Workshop1.Classes;

namespace Workshop1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RLApi api = new RLApi();

//            string body = api.getPlayerRankingSteam("76561198027350858", RLApi.GameTypes.TVT);
            string body = api.getPopulation();
//            string body = api.getAllRanking(RLApi.GameTypes.TVT);

            ChampTexte.Text = body;
        }
    }
}