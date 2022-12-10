using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppCustoViagem.Model
{
    public class ViagemPedagios
    {
        [PrimaryKey, AutoIncrement]
        public int IdViagem { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double Distancia { get; set; }
        public double Kml { get; set; }
        public double PrecoCombustivel { get; set; }
        public double TotalCombustivel { get; set; }
        //public double TotalPedagio { get; set; }
        public double ValorTotal { get; set; }
        //pedagio
        public int IdPedagio { get; set; }
        public string _localizacao;
        public double _valor;

    }
}
