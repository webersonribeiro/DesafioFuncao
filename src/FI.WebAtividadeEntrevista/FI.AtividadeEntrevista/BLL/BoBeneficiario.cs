using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Salvar beneficiários
        /// </summary>
        /// <param name="_beneficiarios"></param>
        public void Salvar(List<Beneficiario> _beneficiarios)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            beneficiarios.Salvar(_beneficiarios);
        }
        
        /// <summary>
        /// Lista os beneficiário grid paginação
        /// </summary>
        public List<Beneficiario> Pesquisa(long IdCliente)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.Pesquisa(IdCliente);
        }
    }
}
