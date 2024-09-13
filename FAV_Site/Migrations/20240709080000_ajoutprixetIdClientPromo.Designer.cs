﻿// <auto-generated />
using System;
using FAV_Site.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FAV_Site.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240709080000_ajoutprixetIdClientPromo")]
    partial class ajoutprixetIdClientPromo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FAV_Site.Models.AdminModels", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mdp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Num_banque")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("FAV_Site.Models.CommandeModels", b =>
                {
                    b.Property<Guid>("Id_cmd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date_achat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_acheteur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_uti_vendeur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lieu_livraiseon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prix_a_payer")
                        .HasColumnType("float");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<string>("Ref_commande")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_cmd");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("FAV_Site.Models.CommentaireModels", b =>
                {
                    b.Property<Guid>("Id_com")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_utilisateur")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_com");

                    b.ToTable("Commentaires");
                });

            modelBuilder.Entity("FAV_Site.Models.GestionModels", b =>
                {
                    b.Property<Guid>("Id_gestion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Aout")
                        .HasColumnType("float");

                    b.Property<double>("Avril")
                        .HasColumnType("float");

                    b.Property<double>("Decembre")
                        .HasColumnType("float");

                    b.Property<double>("Fevrier")
                        .HasColumnType("float");

                    b.Property<string>("Id_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Janvier")
                        .HasColumnType("float");

                    b.Property<double>("Juillet")
                        .HasColumnType("float");

                    b.Property<double>("Juin")
                        .HasColumnType("float");

                    b.Property<double>("Mai")
                        .HasColumnType("float");

                    b.Property<double>("Mars")
                        .HasColumnType("float");

                    b.Property<double>("NbTotal")
                        .HasColumnType("float");

                    b.Property<double>("Novembre")
                        .HasColumnType("float");

                    b.Property<double>("Octobre")
                        .HasColumnType("float");

                    b.Property<double>("Septembre")
                        .HasColumnType("float");

                    b.HasKey("Id_gestion");

                    b.ToTable("Gestions");
                });

            modelBuilder.Entity("FAV_Site.Models.HistoriqueModels", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date_achat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_acheteur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_commande")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_vendeur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prix_a_payser")
                        .HasColumnType("float");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Historiques");
                });

            modelBuilder.Entity("FAV_Site.Models.Image_produitModels", b =>
                {
                    b.Property<Guid>("Id_Image")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Id_Produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image_1")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Image_2")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Image_3")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Image_4")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Image_Couv")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImgLocation1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLocation2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLocation3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLocation4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLocationCouv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Image");

                    b.ToTable("Image_Produits");
                });

            modelBuilder.Entity("FAV_Site.Models.Log_UtilisateurModels", b =>
                {
                    b.Property<Guid>("Id_Log")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Log");

                    b.ToTable("LoginUtilisateur");
                });

            modelBuilder.Entity("FAV_Site.Models.ProduitModels", b =>
                {
                    b.Property<Guid>("Id_produit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Autre_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categorie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date_pub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_client_prix_promo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_utilisateur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_vendeur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marque")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nb_produit_reste")
                        .HasColumnType("int");

                    b.Property<int>("Nb_total_prod")
                        .HasColumnType("int");

                    b.Property<string>("Nom_produit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prix")
                        .HasColumnType("float");

                    b.Property<double>("Prix_promo")
                        .HasColumnType("float");

                    b.Property<bool>("Promotion")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Val_ptix_promo")
                        .HasColumnType("float");

                    b.HasKey("Id_produit");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("FAV_Site.Models.PubliciteModels", b =>
                {
                    b.Property<Guid>("Id_pub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Autre_descri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descri_pub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_utilisateur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nb_reste")
                        .HasColumnType("int");

                    b.Property<int>("Nb_total")
                        .HasColumnType("int");

                    b.Property<string>("Nom_pub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prix")
                        .HasColumnType("float");

                    b.HasKey("Id_pub");

                    b.ToTable("Publicites");
                });

            modelBuilder.Entity("FAV_Site.Models.SuggestionModels", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_utilisateur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_produit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("FAV_Site.Models.Uti_vendeurModels", b =>
                {
                    b.Property<Guid>("Id_uti_ven")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Id_uti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LienVen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_Societe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefLogiciel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_uti_ven");

                    b.ToTable("Vendeurs");
                });

            modelBuilder.Entity("FAV_Site.Models.UtilisateurModels", b =>
                {
                    b.Property<Guid>("Id_ut")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contact_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationnelite_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pays_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Prenom_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type_ut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville_ut")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_ut");

                    b.ToTable("Utilisateurs");
                });
#pragma warning restore 612, 618
        }
    }
}
