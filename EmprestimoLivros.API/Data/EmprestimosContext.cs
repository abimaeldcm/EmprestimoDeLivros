using System;
using EmprestimoLivros.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EmprestimoLivros.API.Data
{
    public partial class EmprestimosContext : DbContext
    {
        public EmprestimosContext(DbContextOptions<EmprestimosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Emprestimo> Emprestimos { get; set; }
        public virtual DbSet<Livro> Livros { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DataBase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.Property(e => e.EmprestimoId).HasColumnName("EmprestimoID");

                entity.Property(e => e.DataDevolucao).HasColumnType("date");

                entity.Property(e => e.DataEmprestimo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LivroId).HasColumnName("LivroID");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Livro)
                    .WithMany(p => p.Emprestimos)
                    .HasForeignKey(d => d.LivroId)
                    .HasConstraintName("FK__Emprestim__Livro__3E52440B");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Emprestimos)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Emprestim__Usuar__3F466844");
            });

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.Property(e => e.LivroId).HasColumnName("LivroID");

                entity.Property(e => e.Autor).HasMaxLength(100);

                entity.Property(e => e.Disponivel).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefone).HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
