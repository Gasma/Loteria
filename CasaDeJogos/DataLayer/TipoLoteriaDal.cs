using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public class TipoLoteriaDal
    {
        #region AcessoaDados
        private Connection basededados;
        private int Insert(TipoLoteria tipoloteria)
        {
            string strQuery = "Insert into tblTipoLoteria (nomeLoteria) values('" + tipoloteria.NomeLoteria + "')";
            using (basededados = new Connection())
            {
                return basededados.InsertData(strQuery);
            }
        }
        private void Update(TipoLoteria tipoloteria)
        {
            string strQuery = "Update tblTipoLoteria set nomeLoteria = '" + tipoloteria.NomeLoteria + "' Where IdLoteria = " + tipoloteria.CodTipoLoteria;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        public void Delete(int cod)
        {
            string strQuery = "Delete From tblTipoLoteria where IdLoteria = " + cod;
            using (basededados = new Connection())
            {
                basededados.ExecutaComando(strQuery);
            }
        }
        private List<TipoLoteria> ListarDados(string param)
        {
            string strQuery = "";
            if (param == null)
                strQuery = "Select * From tblTipoLoteria";
            else
                strQuery = "Select * From tblTipoLoteria Where " + param;
            DataTable objTable;
            var tipoloteria = new List<TipoLoteria>();
            using (basededados = new Connection())
            {
                objTable = basededados.ConRetornaTable(strQuery);
            }
            if (objTable.Rows.Count <= 0)
                return null;
            foreach (DataRow row in objTable.Rows)
                tipoloteria.Add(RowToModel(row));
            return tipoloteria;
        }
        public TipoLoteria RowToModel(DataRow row)
        {
            var obj = new TipoLoteria();
            obj.NomeLoteria = row["nomeLoteria"].ToString();
            if (row["IdLoteria"].ToString() != String.Empty)
                obj.CodTipoLoteria = Convert.ToInt32(row["IdLoteria"].ToString());
            return obj;
        }
        public int Salvar(TipoLoteria tipoloteria)
        {
            if (tipoloteria.CodTipoLoteria > 0)
            {
                Update(tipoloteria);
                return 0;
            }
            else
            {
                return Insert(tipoloteria);
            }
        }
        #endregion
    }
}