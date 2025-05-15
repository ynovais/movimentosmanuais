using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;
namespace MovimentosManuais.Domain.Specifications;
public class ProdutoCosifByProdutoSpecification : BaseSpecification<ProdutoCosif>
{
    public ProdutoCosifByProdutoSpecification(string codProduto)
    {
        AddCriteria(pc => pc.CodProduto == codProduto);
        AddInclude(pc => pc.Produto);
        ApplyOrderBy(pc => pc.CodCosif);
    }
}