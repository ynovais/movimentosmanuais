using MovimentosManuais.Domain.Entities;
using System.Linq.Expressions;

namespace MovimentosManuais.Domain.Specifications;

public class ProdutoCosifByIdSpecification : BaseSpecification<ProdutoCosif>
{
    public ProdutoCosifByIdSpecification(string codProduto, string codCosif)
    {
        AddCriteria(pc => pc.CodProduto == codProduto && pc.CodCosif == codCosif);
        AddInclude(pc => pc.Produto);
    }
}

public class AllProdutoCosifsSpecification : BaseSpecification<ProdutoCosif>
{
    public AllProdutoCosifsSpecification()
    {
        AddInclude(pc => pc.Produto);
        ApplyOrderBy(pc => pc.CodProduto);
    }
}