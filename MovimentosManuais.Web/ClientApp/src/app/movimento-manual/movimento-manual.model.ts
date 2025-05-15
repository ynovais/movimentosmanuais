export interface MovimentoManualDto {
    mes: number;
    ano: number;
    codigoProduto: string;
    descricaoProduto: string;
    numeroLancamento: number;
    descricao: string;
    valor: number;
}

export interface MovimentoManualInputDto {
    mes: number;
    ano: number;
    codProduto: string;
    codCosif: string;
    valor: number;
    descricao: string;
}

export interface Produto {
    codProduto: string;
    codCosif: string;
    desProduto: string;
}
export interface ProdutoCosif {
    
    codCosif: string;
    codProduto: string;
    codClassificacao: string;
}