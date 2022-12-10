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
    public partial class DadosViagem : ContentPage
    {
        Viagens propriedades_viagem;

        public DadosViagem()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            frm_custo_viagem.IsVisible = true;
            btnGravar.IsVisible = true;
            string origem = txt_origem.Text;
            string destino = txt_destino.Text;
            double distancia = Convert.ToDouble(txt_distancia.Text);
            double preco_combustivel = Convert.ToDouble(txt_preco_combustivel.Text);
            double km_litro = Convert.ToDouble(txt_km_litro.Text);

            double custo_combustivel = (distancia / km_litro) * preco_combustivel;

            // Calculando valor do pedágio com LINQ
            double custo_pedagio = App.ListaPedagios.Sum(i => i.Valor);

            // Custo total da viagem
            double custo_viagem = custo_combustivel + custo_pedagio;

            // Mostrando o resultado
            spn_custo_combustivel.Text = custo_combustivel.ToString("C");
            spn_custo_pedagios.Text = custo_pedagio.ToString("C");
            lbl_custo_viagem.Text = custo_viagem.ToString("C");


            propriedades_viagem = new Viagens
            {
                Origem = origem,
                Destino = destino,
                Distancia = distancia,
                Kml = km_litro,
                PrecoCombustivel = preco_combustivel,
                //TotalCombustivel = custo_combustivel,
                TotalCombustivel = 36.2,
            };
        }

        /*private void ToolbarItem_Clicked_Pedagios(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Listagem());
        }*/

        private async void btnGravar_Clicked(object sender, EventArgs e)
        {
            try
            {
                /**
                 * Preenchendo o model Viagens com os dados informados na interface gráfica.
                 */


                /**
                 * Chamando o método insert da SQLiteDatabaseHelper para fazer a inseração do
                 * novo registro no arquivo db3 com os dados da model preenchida acima. O await
                 * denota que o código deve esperar o insert para prosseguir.
                 */
                int id_viagem = App.Database.Insert(propriedades_viagem);

                //BUSCAR ULTIMO ID INSERIDO
                //var ret = App.Database.buscar_ultimo_inserido();

                /**
                 * Avisando o usuário que deu certo.
                 */
                await DisplayAlert("Sucesso!", "Viagem Cadastrada", "OK");

                /**
                 * Navegando para a tela de pedagios. 
                 */
                //await Navigation.PushAsync(new MinhasViagens());
                await Navigation.PushAsync(new Listagem(id_viagem));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops BD", ex.Message, "OK");
            }
        }

        private void ToolbarItem_Clicked_Minhas_Viagens(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MinhasViagens());
        }
    }
}