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
    public partial class EditarViagem : ContentPage
    {
        public EditarViagem()
        {
            InitializeComponent();
        }

        private async void Salvar(object sender, EventArgs e)
        {
            try
            {
                Viagens viagem_anexado = BindingContext as Viagens;
                /**
                 * Preenchendo o model Viagens com os dados informados na interface gráfica.
                 */
                Viagens v = new Viagens();
                {
                    v.IdViagem = viagem_anexado.IdViagem;
                    v.Origem = txt_origem.Text;
                    v.Destino = txt_destino.Text;
                    v.Distancia = Convert.ToInt16(txt_distancia.Text);
                    v.Kml = Convert.ToDouble(txt_km_litro.Text);
                    v.PrecoCombustivel = Convert.ToDouble(txt_preco_combustivel.Text);
                    v.TotalCombustivel = Convert.ToDouble(spn_custo_combustivel.Text);
                    //v.ValorTotal = Convert.ToDouble(lbl_custo_viagem.Text);
                };


                /**
                 * Chamando o método insert da SQLiteDatabaseHelper para fazer a inseração do
                 * novo registro no arquivo db3 com os dados da model preenchida acima. O await
                 * denota que o código deve esperar o insert para prosseguir.
                 */
                await App.Database.Update(v);


                /**
                 * Avisando o usuário que deu certo.
                 */
                await DisplayAlert("Sucesso!", "Produto Editado", "OK");


                /**
                 * Navegando para a tela de listagem. 
                 */
                await Navigation.PushAsync(new MinhasViagens());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void NovaSoma(object sender, EventArgs e)
        {
            btnSalvar.IsVisible = true;

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
        }

        private void Pedagios(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;

            Viagens viagem_selecionada = (Viagens)disparador.BindingContext;
            
            Navigation.PushAsync(new ListagemEdit
            {
                BindingContext = viagem_selecionada
            });

        }
    }
}