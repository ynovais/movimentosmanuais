import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovimentoManualDto, MovimentoManualInputDto, Produto, ProdutoCosif  } from './movimento-manual.model';

@Injectable({
    providedIn: 'root'
})
export class MovimentoManualService {
    private apiUrlMovimentoManual = 'https://localhost:61373/api/MovimentoManual';
    private apiUrlProdutoCosif = 'https://localhost:61373/api/ProdutoCosif';
    private apiUrlProduto = 'https://localhost:61373/api/Produto';

    constructor(private http: HttpClient) { }

    getMovimentos(): Observable<MovimentoManualDto[]> {
        return this.http.get<MovimentoManualDto[]>(this.apiUrlMovimentoManual);
    }

    createMovimento(input: MovimentoManualInputDto): Observable<void> {
        return this.http.post<void>(this.apiUrlMovimentoManual, input);
    }

    getProdutos(): Observable<Produto[]> {
        return this.http.get<Produto[]>(`${this.apiUrlProduto}`);
    }
    getProdutosCosif(codProduto: string): Observable<ProdutoCosif[]> {
        return this.http.get<ProdutoCosif[]>(`${this.apiUrlProdutoCosif}/byProduto/` + codProduto);
    }
}