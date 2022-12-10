using System;
using SQLite;

namespace AppCustoViagem.Model
{
    public class Pedagio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdVIagem { get; set; }

        string _localizacao;
        double _valor;

        public string Localizacao
        {
            get => _localizacao;
            set
            {
                if (value == null)
                    throw new Exception("Informe a localização");
                
                _localizacao = value;
            }
        }

        public double Valor
        {
            get => _valor;
            set
            {
                if (value == 0)
                    throw new Exception("Informe o valor");

                _valor = value;
            }
        }
    }
}
