 using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppCustoViagem.Model;
using SQLite;

namespace AppCustoViagem.Helper
{
    public class SQLiteDatabaseHelper
    {
        /**
         * 
         */
        readonly SQLiteAsyncConnection _conn;
        /**
         * 
         */

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); 
            _conn.CreateTableAsync<Viagens>().Wait();
            _conn.CreateTableAsync<Pedagio>().Wait();

        }


        /**
         * 
         */
        public int Insert(Viagens v)
        {
            int id_viagem_inserida = 0;

            _conn.InsertAsync(v);

            List<Viagens> lst = _conn.QueryAsync<Viagens>("SELECT IdViagem FROM Viagens ORDER BY IdViagem DESC LIMIT 1").Result;

            lst.ForEach(viagem =>
            {
                id_viagem_inserida = viagem.IdViagem;
            });

            return id_viagem_inserida;
        }

        public int InsertPedagio(Pedagio p)
        {
            int id_pedagio_inserido = 0;

            _conn.InsertAsync(p);

            List<Pedagio> lst = _conn.QueryAsync<Pedagio>("SELECT Id FROM Pedagio ORDER BY Id DESC LIMIT 1").Result;

            lst.ForEach(pedagio =>
            {
                id_pedagio_inserido = pedagio.Id;
            });

            return id_pedagio_inserido;
        }


        /**
         * 
         */

        public Task<List<Viagens>> Update(Viagens v)
        {
            string sql = "UPDATE Viagens SET Origem = ?,Destino = ?,Distancia = ?,Kml = ?,PrecoCombustivel = ?,TotalCombustivel = ?,ValorTotal = ? WHERE id = ?";
            return _conn.QueryAsync<Viagens>(sql, v.Origem, v.Destino, v.Distancia, v.Kml, v.PrecoCombustivel, v.TotalCombustivel, v.ValorTotal, v.IdViagem);
        }
        /**
         * 
         */
        //para fazer update preciso pegar um objeto em específico
        /*public Task<Viagens> getById(int id)
        {
            return new Viagens();
        }*/
        /**
         * 
         */
        //busca todos
        public List<Viagens> GetAll()
        {
            List<Viagens> tmp_viagens_com_pedagio = new List<Viagens>();

            List<Pedagio> tmp_pedagio = new List<Pedagio>();

            try
            {               
                List<Viagens> tmp_viagens = _conn.Table<Viagens>().ToListAsync().Result;

                tmp_viagens.ForEach(i =>
                {
                    tmp_pedagio = _conn.QueryAsync<Pedagio>("SELECT * FROM Pedagio WHERE IdViagem = ? ", i.IdViagem).Result;

                    //Console.WriteLine("______________________PEDAGIOS_________________");
                    //Console.WriteLine(tmp_pedagio.Count);

                    i.ListaPedagios = tmp_pedagio;

                    tmp_viagens_com_pedagio.Add(i);
                });                

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);   
            }
  
            return tmp_viagens_com_pedagio;

        }
        /**
         * 
         */
        public Task<List<Pedagio>> GetAllByIdViagem(int id_parametro)
        {
            //array das viagens gravadas
            return _conn.Table<Pedagio>().Where(i => i.IdVIagem == id_parametro).ToListAsync();
        }
        /**
         * 
         */
        public Task<int> Delete(int id)
        {
            return _conn.Table<Viagens>().DeleteAsync(i => i.IdViagem == id);
        }
        /*
         
         */

        public Task<List<Viagens>> Search(string g)
        {
            string sql = "SELECT * FROM Viagens WHERE Destino LIKE '%"+g+ "%' OR Origem LIKE '%" + g + "%' OR ValorTotal LIKE '%" + g + "%' OR TotalCombustivel LIKE '%" + g + "%'";
            return _conn.QueryAsync<Viagens>(sql);
        }

        /*9public Task<List<Viagens>> buscar_ultimo_inserido()
        {
            
        }*/
    }
}
