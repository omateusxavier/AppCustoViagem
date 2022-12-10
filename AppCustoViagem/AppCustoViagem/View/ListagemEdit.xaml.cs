using AppCustoViagem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemEdit : ContentPage
    {
        Viagens viagem_selecionada;
        ObservableCollection<Pedagio> colecao = new ObservableCollection<Pedagio>();

        public ListagemEdit()
        {
            InitializeComponent();

            viagem_selecionada = BindingContext as Viagens;

            lst_pedagios.ItemsSource = colecao;
        }

        protected override void OnAppearing()
        {
            Task.Run(async () =>
            {
                try
                {
                    List<Pedagio> tmp = await App.Database.GetAllByIdViagem(viagem_selecionada.IdViagem);

                    foreach(Pedagio p in tmp)
                    {
                        colecao.Add(p);
                    }                   
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                }
            });            
        }
    }
}