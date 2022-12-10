using AppCustoViagem.Model;
using AppCustoViagem.View;
using Xamarin.Forms;
using System.Globalization;
using System.Threading;
using System.Collections.ObjectModel;
using AppCustoViagem.Helper;
using System.IO;
using System;

namespace AppCustoViagem
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper database;
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                if(database == null)
                {
                    //app ainda nao encontrou arquivo banco
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "arquivo.db3"
                        );
                    database = new SQLiteDatabaseHelper(path);
                }
                return database;
            }
        }
        public static ObservableCollection<Pedagio> ListaPedagios = new ObservableCollection<Pedagio>();

        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            MainPage = new NavigationPage(new DadosViagem());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
