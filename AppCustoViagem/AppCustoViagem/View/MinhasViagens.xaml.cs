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
    public partial class MinhasViagens : ContentPage
    {
        ObservableCollection<Viagens> lista_minhas_viagens = new ObservableCollection<Viagens>();

        public MinhasViagens()
        {
            InitializeComponent();

            lst_minhas_viagens.ItemsSource = lista_minhas_viagens;
        }

        protected override void OnAppearing()
        {
            try
            {
                Task.Run(async () =>
                {
                    List<Viagens> temp = App.Database.GetAll();

                    lista_minhas_viagens.Clear();

                    temp.ForEach(item =>
                    {
                        Viagens v = new Viagens
                        {
                            Origem = item.Origem,
                            Destino = item.Origem,
                            ListaPedagios = item.ListaPedagios,
                            Distancia = item.Distancia,
                            Kml = item.Kml,
                            TotalCombustivel = item.TotalCombustivel,
                            IdViagem = item.IdViagem,
                            PrecoCombustivel = item.PrecoCombustivel
                        };
                        
                        lista_minhas_viagens.Add(v);

                    });

                    ref_carregando.IsRefreshing = false;
                });

            } catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "ok");
                Console.WriteLine(ex.StackTrace);
            }
        }

        private async void Remover(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;

            Viagens viagem_selecionada = (Viagens)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem certeza?", "Excluir Viagem?", "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.Delete(viagem_selecionada.IdViagem);
                lista_minhas_viagens.Remove(viagem_selecionada);
            }
        }

        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Viagens> temp = await App.Database.Search(buscou);

                lista_minhas_viagens.Clear();
                foreach (Viagens item in temp)
                {
                    lista_minhas_viagens.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }

        

        private void lst_minhas_viagens_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EditarViagem
            {
                try
                {
                    Task.Run(async () =>
                    {
                        List<Viagens> temp = App.Database.GetAll();

                        lista_minhas_viagens.Clear();

                        temp.ForEach(item =>
                        {
                            Viagens v = new Viagens
                            {
                                Origem = item.Origem,
                                Destino = item.Origem,
                                ListaPedagios = item.ListaPedagios,
                                Distancia = item.Distancia,
                                Kml = item.Kml,
                                TotalCombustivel = item.TotalCombustivel,
                                IdViagem = item.IdViagem,
                                PrecoCombustivel = item.PrecoCombustivel
                            };

                            lista_minhas_viagens.Add(v);

                        });

                        ref_carregando.IsRefreshing = false;
                    });

                }
                catch (Exception ex)
                {
                    DisplayAlert("Ops", ex.Message, "ok");
                    Console.WriteLine(ex.StackTrace);
                }

            //BindingContext = (Viagens)e.SelectedItem
            });
        }

        
    }
}