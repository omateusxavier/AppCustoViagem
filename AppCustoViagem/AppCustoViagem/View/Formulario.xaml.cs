using AppCustoViagem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        int id_viagem_selecionada;
        public Formulario(int id_viagem)
        {
            InitializeComponent();
            id_viagem_selecionada = id_viagem;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Pedagio p = new Pedagio
                {

                    Localizacao = txt_localizacao.Text,
                    Valor = Convert.ToDouble(txt_valor.Text),
                    IdVIagem = id_viagem_selecionada

                };

                //inserir no banco
                try
                {
                    /**
                     * Preenchendo o model Pedagios com os dados informados na interface gráfica.
                     */


                    /**
                     * Chamando o método insert da SQLiteDatabaseHelper para fazer a inseração do
                     * novo registro no arquivo db3 com os dados da model preenchida acima. O await
                     * denota que o código deve esperar o insert para prosseguir.
                     */
                    int id_pedagio = App.Database.InsertPedagio(p);
                    App.ListaPedagios.Add(p);
                    /**
                     * Avisando o usuário que deu certo.
                     */
                    await DisplayAlert("Sucesso!", "Pedagio Cadastrado", "OK");

                    /**
                     * Navegando para a tela de pedagios. 
                     */
                    //await Navigation.PushAsync(new MinhasViagens());
                    await Navigation.PushAsync(new Listagem(id_viagem_selecionada));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                }

                /*App.ListaPedagios.Add(p);

                //PropriedadesApp.ListaPedagios.Add(p);

                await DisplayAlert("Deu Certo!", "Pedágio Adicionado", "OK");

                await Navigation.PopAsync();*/

            } catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}