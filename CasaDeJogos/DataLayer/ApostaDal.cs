using CasaDeJogos.BusinessLayer;
using CasaDeJogos.DataLayer;
using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public class ApostaDal
    {
        #region AcessoaDados
        private Connection basededados;
        private int Insert(Aposta aposta)
        {
            int valor = 0;
            if (aposta.Sorteio)
                valor = 1;
            string strQuery = "Insert into tblAposta (dataAposta, idCodLoteria, sorteio, idCodResultado, IdAposta) values(GETDATE(), " + aposta.tipoLoteria + ", " + valor + ", " + aposta.Resultado;
            using (basededados = new Connection())
            {
                int cod = basededados.getCod("Select Max(idAposta) from tblAposta");
                strQuery += ", " + (cod + 1) + ")";
                basededados.InsertData(strQuery);
                return (cod + 1);
            }
        }
        private void Update(Aposta aposta)
        {
            string strQuery = "Update tblAposta set dataAposta = '" + aposta.dataDaAposta + "', idCodLoteria = " + aposta.tipoLoteria + ", idCodResultado = " + aposta.Resultado + ", quantAcertos = " + aposta.QuantidadeAcertos + ", idSorteio = " + aposta.CodSorteio + " Where IdAposta = " + aposta.CodAposta;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public void Delete(int cod)
        {
            string strQuery = "Delete From tblAposta where IdAposta = " + cod;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public List<Aposta> ListarDados(string param)
        {
            string strQuery = "";
            if (param == null)
                strQuery = "Select * From tblAposta";
            else
                strQuery = "Select * From tblAposta Where " + param;
            DataTable objTable;
            var aposta = new List<Aposta>();
            using (basededados = new Connection())
            {
                objTable = basededados.ConRetornaTable(strQuery);
            }
            if (objTable.Rows.Count <= 0)
                return null;
            foreach (DataRow row in objTable.Rows)
                aposta.Add(RowToModel(row));
            return aposta;
        }
        public Aposta RowToModel(DataRow row)
        {
            var obj = new Aposta();
            obj.dataDaAposta = Convert.ToDateTime(row["dataAposta"].ToString());
            obj.tipoLoteria = Convert.ToInt32(row["idCodLoteria"].ToString());
            obj.Resultado = Convert.ToInt32(row["idCodResultado"].ToString());
            if (row["quantAcertos"].ToString() != String.Empty)
                obj.QuantidadeAcertos = Convert.ToInt32(row["quantAcertos"].ToString());
            if (row["IdAposta"].ToString() != String.Empty)
                obj.CodAposta = Convert.ToInt32(row["IdAposta"].ToString());
            return obj;
        }
        #endregion
        public int Salvar(Aposta aposta)
        {
            if (aposta.CodAposta > 0)
            {
                Update(aposta);
                return 0;
            }
            else
            {
                return Insert(aposta);
            }
        }
    }
}