using MovimentosManuais.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace MovimentosManuais.Domain.Specifications;

public class MovimentoManualByDateRangeSpecification : BaseSpecification<MovimentoManual>
{
    public MovimentoManualByDateRangeSpecification(int? mes = null, int? ano = null)
    {
        if (mes.HasValue)
            AddCriteria(mm => mm.DatMes == mes.Value);
        if (ano.HasValue)
            AddCriteria(mm => mm.DatAno == ano.Value);

        AddInclude(mm => mm.ProdutoCosif);
        ApplyOrderBy(mm => mm.DatMovimento);
    }
}

public class AllMovimentoManuaisSpecification : BaseSpecification<MovimentoManual>
{
    public AllMovimentoManuaisSpecification()
    {
        AddInclude(mm => mm.ProdutoCosif);
        ApplyOrderBy(mm => mm.DatMovimento);
    }
}