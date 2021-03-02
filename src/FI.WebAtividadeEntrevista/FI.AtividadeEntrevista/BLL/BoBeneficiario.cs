using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {

        // <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="cliente">Objeto de beneficiário</param>
        public long Incluir(Beneficiario beneficiario)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="cliente">Objeto de beneficiário</param>
        public void Alterar(Beneficiario beneficiario)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            beneficiarios.Alterar(beneficiario);
        }

        public void Salvar(List<Beneficiario> _beneficiarios)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            beneficiarios.Salvar(_beneficiarios);
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public Beneficiario Consultar(long id)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            beneficiarios.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiários
        /// </summary>
        public List<Beneficiario> Listar()
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.Listar();
        }

        /// <summary>
        /// Lista os beneficiário grid paginação
        /// </summary>
        public List<Beneficiario> Pesquisa(long IdCliente)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.Pesquisa(IdCliente);
        }

        /// <summary>
        /// Verifica se o beneficiário já existe cadastrado com o mesmo CPF
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(long Id, long IdCliente, string CPF)
        {
            DaoBeneficiarios beneficiarios = new DaoBeneficiarios();
            return beneficiarios.VerificarExistencia(Id, IdCliente,  CPF);
        }
    }
}
