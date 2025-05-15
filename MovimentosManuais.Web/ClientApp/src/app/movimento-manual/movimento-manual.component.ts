import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MovimentoManualService } from './movimento-manual.service';
import { MovimentoManualDto, MovimentoManualInputDto, Produto, ProdutoCosif } from './movimento-manual.model';

@Component({
    selector: 'app-movimento-manual',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './movimento-manual.component.html',
    styleUrls: ['./movimento-manual.component.css']
})
export class MovimentoManualComponent implements OnInit {
    movimentos: MovimentoManualDto[] = [];
    produtos: Produto[] = [];
    produtoscosif: ProdutoCosif[] = [];
    input: MovimentoManualInputDto = {
        mes: 1,
        ano: new Date().getFullYear(),
        codProduto: '',
        codCosif: '',
        valor: 0,
        descricao: ''
    };
    isFormDisabled = false;

    constructor(private service: MovimentoManualService) { }

    ngOnInit(): void {
        this.loadMovimentos();
        this.loadProdutos();
    }

    loadMovimentos(): void {
        this.service.getMovimentos().subscribe({
            next: (data) => this.movimentos = data,
            error: (err) => console.error('Erro ao carregar movimentos', err)
        });
    }

    loadProdutos(): void {
        this.service.getProdutos().subscribe({
            next: (data) => this.produtos = data,
            error: (err) => console.error('Erro ao carregar produtos', err)
        });
    }

    loadProdutosCosif(codProduto: string): void {
        this.service.getProdutosCosif(codProduto).subscribe({
            next: (data) => this.produtoscosif = data,
            error: (err) => console.error('Erro ao carregar produtos Cosif', err)
        });
    }

    onProdutoChange(): void {
        // const produto = this.produtos.find(p => p.codProduto === this.input.codProduto);
        //this.input.codCosif = produto ? produto.codCosif : '';

        this.loadProdutosCosif(this.input.codProduto)
    }

    onProdutoCosifChange(): void {
        // const produto = this.produtos.find(p => p.codProduto === this.input.codProduto);
       // this.input.codCosif = produto ? produto.codCosif : '';

        // this.loadProdutosCosif(produto ? produto.codCosif : '')
    }
    onSubmit(): void {
        if (this.isFormDisabled) return;

        this.service.createMovimento(this.input).subscribe({
            next: () => {
                this.loadMovimentos();
                this.isFormDisabled = true;
            },
            error: (err) => console.error('Erro ao criar movimento', err)
        });
    }

    resetForm(): void {
        this.input = {
            mes: 1,
            ano: new Date().getFullYear(),
            codProduto: '',
            codCosif: '',
            valor: 0,
            descricao: ''
        };
        this.isFormDisabled = false;
    }
}