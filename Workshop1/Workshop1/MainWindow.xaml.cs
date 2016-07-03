using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
            try
            {
                api.getSessionID();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        RLApi api = new RLApi();

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string body = api.getPopulation();
                //ChampTexte.Text = body;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<TextBlock> mmrList = new List<TextBlock>();
                mmrList.Add(MMR1v1);
                mmrList.Add(MMR2v2);
                mmrList.Add(MMR3v3solo);
                mmrList.Add(MMR3v3);

                List<TextBlock> divList = new List<TextBlock>();
                divList.Add(Division1v1);
                divList.Add(Division2v2);
                divList.Add(Division3v3solo);
                divList.Add(Division3v3);

                string idSteam = SteamID.Text;

                //string body = api.getPlayerRankingSteam("76561198079418655", RLApi.GameTypes.OVO);
                //body += api.getPlayerRankingSteam("76561198027350858", RLApi.GameTypes.TVT);
                //body += api.getPlayerRankingSteam("76561198027350858", RLApi.GameTypes.THVTH);
                //body += api.getPlayerRankingSteam("76561198027350858", RLApi.GameTypes.THVTHSOLO);

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    try
                    {
                        idSteam = Classes.Steam.getIdByName(idSteam);
                        PlayerStats[][] body = api.getAllPlayerRankingSteam(idSteam);
                        int size = body.Count();
                        for (int i = 0; i < body.Count(); i++)
                        {
                            Dispatcher.BeginInvoke(new Action(delegate () { 
                                (mmrList.ToArray())[i].Text = body[i][0].MMR.ToString();
                                (divList.ToArray())[i].Text = body[i][0].Division.ToString();
                            }));
                            Thread.Sleep(1);
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }).Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RLApi.GameTypes toGet = (RLApi.GameTypes)(gametype.SelectedIndex + 10);
                DataGridTest.Items.Clear();

                int j = 1;
                int i = 1;
                int nbMax = 0;
                if (Steam.IsChecked == false && PSN.IsChecked == true)
                    i = 101;
                if (Steam.IsChecked == true && PSN.IsChecked == false)
                    nbMax = 103;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    try
                    {
                        List<PlayerStats> body = api.getAllRanking(toGet);
                        if (nbMax == 0)
                            nbMax = body.ToArray().Count();
                        for (i = i; i < nbMax - 2; i++)
                        {
                            if (j == 101)
                                j = 1;
                            (body.ToArray())[i].rank = j;
                            var data = (body.ToArray())[i];
                            Dispatcher.BeginInvoke(new Action(delegate () { DataGridTest.Items.Add(data); }));
                            j++;
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }).Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Une erreur est survenue", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
