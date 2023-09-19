﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoInterdisciplinarII.Models.Data;

#nullable disable

namespace ProjetoInterdisciplinarII.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PostagemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PostagemId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Curtida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PostagemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PostagemId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Curtidas");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Postagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ativo")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Postagens");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Comentario", b =>
                {
                    b.HasOne("ProjetoInterdisciplinarII.Models.Postagem", null)
                        .WithMany("Comentarios")
                        .HasForeignKey("PostagemId");

                    b.HasOne("ProjetoInterdisciplinarII.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Curtida", b =>
                {
                    b.HasOne("ProjetoInterdisciplinarII.Models.Postagem", null)
                        .WithMany("Curtidas")
                        .HasForeignKey("PostagemId");

                    b.HasOne("ProjetoInterdisciplinarII.Models.Usuario", "Usuario")
                        .WithMany("Curtidas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Postagem", b =>
                {
                    b.HasOne("ProjetoInterdisciplinarII.Models.Usuario", null)
                        .WithMany("Postagens")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Postagem", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Curtidas");
                });

            modelBuilder.Entity("ProjetoInterdisciplinarII.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Curtidas");

                    b.Navigation("Postagens");
                });
#pragma warning restore 612, 618
        }
    }
}