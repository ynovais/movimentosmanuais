using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MovimentosManuais.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoCosif> ProdutosCosif { get; set; }
    public DbSet<MovimentoManual> MovimentosManuais { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("PRODUTO");
            entity.HasKey(p => p.CodProduto);
            entity.Property(p => p.CodProduto).HasColumnType("CHAR(4)").IsRequired().HasColumnName("COD_PRODUTO");
            entity.Property(p => p.DesProduto).HasColumnType("VARCHAR(30)").HasColumnName("DES_PRODUTO");
            entity.Property(p => p.StaStatus).HasColumnName("STA_STATUS")
            .HasColumnType("CHAR(1)")
            .HasConversion<string>()
            .IsRequired(false);

            entity.HasCheckConstraint("CHK_PRODUTO_STATUS", "[StaStatus] IN ('A', 'I')");
        });

        modelBuilder.Entity<ProdutoCosif>(entity =>
        {
            entity.ToTable("PRODUTO_COSIF");
            entity.HasKey(pc => new { pc.CodProduto, pc.CodCosif });
            entity.Property(pc => pc.CodProduto).HasColumnType("CHAR(4)").IsRequired().HasColumnName("COD_PRODUTO");
            entity.Property(pc => pc.CodCosif).HasColumnType("VARCHAR(11)").IsRequired().HasColumnName("COD_COSIF");
            entity.Property(pc => pc.CodClassificacao).HasColumnType("CHAR(6)").HasColumnName("COD_CLASSIFICACAO");
            entity.Property(pc => pc.StaStatus).HasColumnName("STA_STATUS")
            .HasColumnType("CHAR(1)")
            .HasConversion<string>()
            .IsRequired(false);

            entity.HasCheckConstraint("CHK_PRODUTO_COSIF_STATUS", "[StaStatus] IN ('A', 'I')");

            entity.HasOne(pc => pc.Produto)
                .WithMany(p => p.ProdutosCosif)
                .HasForeignKey(pc => pc.CodProduto)
                .HasConstraintName("FK_PRODUTO_COSIF_PRODUTO");
        });

        modelBuilder.Entity<MovimentoManual>(entity =>
        {
            entity.ToTable("MOVIMENTO_MANUAL");
            entity.HasKey(mm => new { mm.DatMes, mm.DatAno, mm.NumLancamento });
            entity.Property(mm => mm.DatMes).HasColumnType("INT").IsRequired().HasColumnName("DAT_MES");
            entity.Property(mm => mm.DatAno).HasColumnType("INT").IsRequired().HasColumnName("DAT_ANO"); ;
            entity.Property(mm => mm.NumLancamento).HasColumnType("BIGINT").IsRequired().HasColumnName("NUM_LANCAMENTO"); ;
            entity.Property(mm => mm.CodProduto).HasColumnType("CHAR(4)").IsRequired().HasColumnName("COD_PRODUTO"); ;
            entity.Property(mm => mm.CodCosif).HasColumnType("VARCHAR(11)").IsRequired().HasColumnName("COD_COSIF"); ;
            entity.Property(mm => mm.ValValor).HasColumnType("DECIMAL(18,2)").IsRequired().HasColumnName("VAL_VALOR"); ;
            entity.Property(mm => mm.DesDescricao).HasColumnType("VARCHAR(50)").IsRequired().HasColumnName("DES_DESCRICAO"); ;
            entity.Property(mm => mm.DatMovimento).HasColumnType("SMALLDATETIME").IsRequired().HasColumnName("DAT_MOVIMENTO"); ;
            entity.Property(mm => mm.CodUsuario).HasColumnType("VARCHAR(15)").IsRequired().HasColumnName("COD_USUARIO"); ;

            entity.HasOne(mm => mm.ProdutoCosif)
                .WithMany(pc => pc.MovimentosManuais)
                .HasForeignKey(mm => new { mm.CodProduto, mm.CodCosif })
                .HasConstraintName("FK_MOVIMENTO_MANUAL_PRODUTO_COSIF");
        });

        modelBuilder.Entity<Produto>().HasData(
            new Produto { CodProduto = "P001", DesProduto = "Produto A", StaStatus = "A" },
            new Produto { CodProduto = "P002", DesProduto = "Produto B", StaStatus = "A" }
        );

        modelBuilder.Entity<ProdutoCosif>().HasData(
            new ProdutoCosif { CodProduto = "P001", CodCosif = "COSIF001", CodClassificacao = "NORM01", StaStatus = "A" },
            new ProdutoCosif { CodProduto = "P002", CodCosif = "COSIF002", CodClassificacao = "NORM02", StaStatus = "A" }
        );

        modelBuilder.Entity<MovimentoManual>().HasData(
            new MovimentoManual
            {
                DatMes = 1,
                DatAno = 2023,
                NumLancamento = 1,
                CodProduto = "P001",
                CodCosif = "COSIF001",
                ValValor = 1000.50m,
                DesDescricao = "Movimento Inicial",
                DatMovimento = new DateTime(2023, 1, 1, 10, 0, 0),
                CodUsuario = "enclidato"
            },
            new MovimentoManual
            {
                DatMes = 1,
                DatAno = 2023,
                NumLancamento = 2,
                CodProduto = "P002",
                CodCosif = "COSIF002",
                ValValor = 2000.75m,
                DesDescricao = "Movimento Secundário",
                DatMovimento = new DateTime(2023, 1, 2, 12, 0, 0),
                CodUsuario = "enclidato"
            }
        );
    }
}