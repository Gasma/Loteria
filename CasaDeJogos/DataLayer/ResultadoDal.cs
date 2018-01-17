using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public class ResultadoDal
    {
        #region AcessoaDados
        private Connection basededados;
        private int Insert(Resultado resultado)
        {
            string strQuery = "Insert into tblResultado (NomeResultado) values('" + resultado.NomeResultado + "')";
            using (basededados = new Connection())
            {
                return basededados.InsertData(strQuery);
            }
        }
        private void Update(Resultado resultado)
        {
            string strQuery = "Update tblResultado set NomeResultado = '" + resultado.NomeResultado + "' Where IdResultado = " + resultado.CodResultado;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public void Delete(int cod)
        {
            string strQuery = "Delete From tblResultado where IdResultado = " + cod;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        private List<Resultado> ListarDados(string param)
        {
            string strQuery = "";
            if (param == null)
                strQuery = "Select * From tblResultado";
            else
                strQuery = "Select * From tblResultado Where " + param;
            DataTable objTable;
            var resultado = new List<Resultado>();
            using (basededados = new Connection())
            {
                objTable = basededados.ConRetornaTable(strQuery);
            }
            if (objTable.Rows.Count <= 0)
                return null;
            foreach (DataRow row in objTable.Rows)
                resultado.Add(RowToModel(row));
            return resultado;
        }
        public Resultado RowToModel(DataRow row)
        {
            var obj = new Resultado();
            obj.NomeResultado = row["NomeResultado"].ToString();
            if (row["IdResultado"].ToString() != String.Empty)
                obj.CodResultado = Convert.ToInt32(row["IdResultado"].ToString());
            return obj;
        }
        public int Salvar(Resultado resultado)
        {
            if (resultado.CodResultado > 0)
            {
                Update(resultado);
                return 0;
            }
            else
            {
                return Insert(resultado);
            }
        }
        #endregion
    }
}