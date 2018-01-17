using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public class NumeroApostadoDal
    {
        #region AcessoaDados
        private Connection basededados;
        private int Insert(NumeroApostado numeroApostado)
        {
            string strQuery = "Insert into tblNumeroApostado (codAposta, numeroNumeroApostado, IdNumeroApostado) values(" + numeroApostado.CodAposta + ", " + numeroApostado.numeroApostado;
            using (basededados = new Connection())
            {
                int cod = basededados.getCod("Select Max(IdNumeroApostado) from tblNumeroApostado");
                strQuery += ", " + (cod + 1) + ")";
                basededados.InsertData(strQuery);
                return (cod + 1);
            }
        }
        private void Update(NumeroApostado numeroApostado)
        {
            string strQuery = "Update tblNumeroApostado set codAposta = '" + numeroApostado.CodAposta + "', numeroNumeroApostado = " + numeroApostado.numeroApostado + " Where IdNumeroApostado = " + numeroApostado.CodNumeroApostado;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public void Delete(int cod)
        {
            string strQuery = "Delete From tblNumeroApostado where IdNumeroApostado = " + cod;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public List<NumeroApostado> ListarDados(string param)
        {
            string strQuery = "";
            if (param == null)
                strQuery = "Select * From tblNumeroApostado";
            else
                strQuery = "Select * From tblNumeroApostado Where " + param;
            DataTable objTable;
            var numeroApostado = new List<NumeroApostado>();
            using (basededados = new Connection())
            {
                objTable = basededados.ConRetornaTable(strQuery);
            }
            if (objTable.Rows.Count <= 0)
                return null;
            foreach (DataRow row in objTable.Rows)
                numeroApostado.Add(RowToModel(row));
            return numeroApostado;
        }
        public NumeroApostado RowToModel(DataRow row)
        {
            var obj = new NumeroApostado();
            obj.CodAposta = Convert.ToInt32(row["codAposta"].ToString());
            obj.numeroApostado = Convert.ToInt32(row["numeroNumeroApostado"].ToString());
            if (row["IdNumeroApostado"].ToString() != String.Empty)
                obj.CodAposta = Convert.ToInt32(row["IdNumeroApostado"].ToString());
            return obj;
        }
        public int Salvar(NumeroApostado numeroApostado)
        {
            return Insert(numeroApostado);
        }
        #endregion
    }
}