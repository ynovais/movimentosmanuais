using MovimentosManuais.Domain.Entities;
using System.Linq.Expressions;

namespace MovimentosManuais.Domain.Specifications;

public class ProdutoByIdSpecification : BaseSpecification<Produto>
{
    public ProdutoByIdSpecification(string codProduto)
    {
        AddCriteria(p => p.CodProduto == codProduto);
    }
}

public class AllProdutosSpecification : BaseSpecification<Produto>
{
    public AllProdutosSpecification()
    {
        ApplyOrderBy(p => p.DesProduto);
    }
}