﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebAtividadeEntrevista.DTOs;
using FI.AtividadeEntrevista.DAL;

namespace WebAtividadeEntrevista.DAO
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("Nome", beneficiario.Nome));
            list.Add(new SqlParameter("CPF", beneficiario.CPF));
            list.Add(new SqlParameter("ClienteId", beneficiario.ClienteId));
            DataSet dataSet = Consultar("FI_SP_IncBeneficiarioV2", list);
            long result = 0L;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dataSet.Tables[0].Rows[0][0].ToString(), out result);
            }

            return result;
        }

        internal Beneficiario Consultar(long Id)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("Id", Id));
            DataSet ds = Consultar("FI_SP_ConsBeneficiario", list);
            List<Beneficiario> source = Converter(ds);
            return source.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("CPF", CPF));
            DataSet dataSet = Consultar("FI_SP_VerificaBeneficiario", list);
            return dataSet.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("iniciarEm", iniciarEm));
            list.Add(new SqlParameter("quantidade", quantidade));
            list.Add(new SqlParameter("campoOrdenacao", campoOrdenacao));
            list.Add(new SqlParameter("crescente", crescente));
            DataSet dataSet = Consultar("FI_SP_PesqBeneficiario", list);
            List<Beneficiario> result = Converter(dataSet);
            int result2 = 0;
            if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
            {
                int.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out result2);
            }

            qtd = result2;
            return result;
        }

        internal List<Beneficiario> Listar()
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("Id", SqlDbType.BigInt));
            DataSet ds = Consultar("FI_SP_ConsBeneficiario", list);
            return Converter(ds);
        }

        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("Nome", beneficiario.Nome));
            list.Add(new SqlParameter("CPF", beneficiario.CPF));
            list.Add(new SqlParameter("ClienteId", beneficiario.ClienteId));
            list.Add(new SqlParameter("ID", beneficiario.Id));
            Executar("FI_SP_AltBeneficiario", list);
        }

        internal void Excluir(long Id)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("Id", Id));
            Executar("FI_SP_DelBeneficiario", list);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> list = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario beneficiario = new Beneficiario();
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.CPF = row.Field<string>("CPF");
                    beneficiario.Nome = row.Field<string>("Nome");
                    beneficiario.ClienteId = row.Field<long>("ClienteId");
                    list.Add(beneficiario);
                }
            }

            return list;
        }
    }
}