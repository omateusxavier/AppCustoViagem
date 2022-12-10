using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace AppCustoViagem.Model
{
    public class Viagens
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

        [Ignore]
        public double ValorTotal 
        {
            get
            {
                return TotalCombustivel + ListaPedagios.Sum(p => p.Valor);
            }
        }

        public List<Pedagio> _listaPedagio = new List<Pedagio>();

        [Ignore]
        public List<Pedagio> ListaPedagios 
        { 
            get => _listaPedagio; 
            set => _listaPedagio = value; 
        }

        [Ignore]
        public double ValorPedagios
        {
            get => ListaPedagios.Sum(p => p.Valor);
        }

    }
}
