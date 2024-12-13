﻿// <auto-generated />
using System;
using API_OVH.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_OVH.Migrations
{
    [DbContext(typeof(SAE5_BD_OVH_DbContext))]
    [Migration("20241210085850_mg5")]
    partial class mg5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Batiment", b =>
                {
                    b.Property<int>("IdBatiment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("batimentid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdBatiment"));

                    b.Property<string>("NomBatiment")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nombatiment");

                    b.HasKey("IdBatiment")
                        .HasName("pk_batiment");

                    b.ToTable("batiment", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Capteur", b =>
                {
                    b.Property<int>("IdCapteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idcapteur");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCapteur"));

                    b.Property<string>("EstActif")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3)
                        .HasColumnType("char(3)")
                        .HasDefaultValue("NSP")
                        .HasColumnName("estactif");

                    b.Property<int?>("IdMur")
                        .HasColumnType("integer")
                        .HasColumnName("idmur");

                    b.Property<string>("NomCapteur")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("nomcapteur");

                    b.Property<decimal>("XCapteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("xcapteur");

                    b.Property<decimal>("YCapteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("ycapteur");

                    b.Property<decimal>("ZCapteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("zcapteur");

                    b.HasKey("IdCapteur")
                        .HasName("pk_capteur");

                    b.HasIndex("IdMur");

                    b.ToTable("capteur", null, t =>
                        {
                            t.HasCheckConstraint("chk_capteur_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')");
                        });
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Direction", b =>
                {
                    b.Property<short>("IdDirection")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("iddirection");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("IdDirection"));

                    b.Property<string>("LettresDirection")
                        .IsRequired()
                        .HasColumnType("varchar(2)")
                        .HasColumnName("lettres_direction");

                    b.HasKey("IdDirection")
                        .HasName("pk_direction");

                    b.ToTable("direction", null, t =>
                        {
                            t.HasCheckConstraint("chk_direction", "\"lettres_direction\" IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')");
                        });
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Equipement", b =>
                {
                    b.Property<int>("IdEquipement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idequipement");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdEquipement"));

                    b.Property<string>("EstActif")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(3)")
                        .HasDefaultValue("NSP")
                        .HasColumnName("estactif");

                    b.Property<decimal>("Hauteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(5,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("hauteur");

                    b.Property<int>("IdMur")
                        .HasColumnType("integer")
                        .HasColumnName("idmur");

                    b.Property<int>("IdTypeEquipement")
                        .HasColumnType("integer")
                        .HasColumnName("idtypeequipement");

                    b.Property<decimal>("Largeur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m)
                        .HasColumnName("largeur");

                    b.Property<decimal>("Longueur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m)
                        .HasColumnName("longueur");

                    b.Property<string>("NomEquipement")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nomequipement");

                    b.Property<decimal>("XEquipement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("xequipement");

                    b.Property<decimal>("YEquipement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("yequipement");

                    b.Property<decimal>("ZEquipement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,1)")
                        .HasDefaultValue(0m)
                        .HasColumnName("zequipement");

                    b.HasKey("IdEquipement")
                        .HasName("pk_equipement");

                    b.HasIndex("IdMur");

                    b.HasIndex("IdTypeEquipement");

                    b.ToTable("equipement", null, t =>
                        {
                            t.HasCheckConstraint("chk_equip_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')");

                            t.HasCheckConstraint("chk_equip_haut", "\"hauteur\" >= 0");

                            t.HasCheckConstraint("chk_equip_larg", "\"largeur\" >= 0");

                            t.HasCheckConstraint("chk_equip_long", "\"longueur\" >= 0");
                        });
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Mur", b =>
                {
                    b.Property<int>("IdMur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idmur");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdMur"));

                    b.Property<decimal>("Hauteur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m)
                        .HasColumnName("hauteur");

                    b.Property<short>("IdDirection")
                        .HasColumnType("smallint")
                        .HasColumnName("iddirection");

                    b.Property<int>("IdSalle")
                        .HasColumnType("integer")
                        .HasColumnName("idsalle");

                    b.Property<decimal>("Longueur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m)
                        .HasColumnName("longueur");

                    b.Property<decimal>("Orientation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(9,6)")
                        .HasDefaultValue(0m)
                        .HasColumnName("orientation");

                    b.HasKey("IdMur")
                        .HasName("pk_mur");

                    b.HasIndex("IdDirection");

                    b.HasIndex("IdSalle");

                    b.ToTable("mur", null, t =>
                        {
                            t.HasCheckConstraint("chk_hauteur", "\"hauteur\" > 0");

                            t.HasCheckConstraint("chk_longueur", "\"longueur\" > 0");

                            t.HasCheckConstraint("chk_orientation_degrees", "\"orientation\" >= 0 AND \"orientation\" <= 360");
                        });
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Salle", b =>
                {
                    b.Property<int>("IdSalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idsalle");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdSalle"));

                    b.Property<int>("IdBatiment")
                        .HasColumnType("integer")
                        .HasColumnName("idbatiment");

                    b.Property<int>("IdTypeSalle")
                        .HasColumnType("integer")
                        .HasColumnName("idtypesalle");

                    b.Property<string>("NomSalle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nomsalle");

                    b.HasKey("IdSalle")
                        .HasName("pk_salle");

                    b.HasIndex("IdBatiment");

                    b.HasIndex("IdTypeSalle");

                    b.ToTable("salle", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.TypeEquipement", b =>
                {
                    b.Property<int>("IdTypeEquipement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idtypeequipement");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdTypeEquipement"));

                    b.Property<string>("NomTypeEquipement")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nomtypeequipement");

                    b.HasKey("IdTypeEquipement")
                        .HasName("pk_typeequipement");

                    b.ToTable("typeequipement", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.TypeSalle", b =>
                {
                    b.Property<int>("IdTypeSalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idtypesalle");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdTypeSalle"));

                    b.Property<string>("NomTypeSalle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nomtypesalle");

                    b.HasKey("IdTypeSalle")
                        .HasName("pk_typesalle");

                    b.ToTable("typesalle", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Unite", b =>
                {
                    b.Property<int>("IdUnite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idunite");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUnite"));

                    b.Property<string>("NomUnite")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nomunite");

                    b.Property<string>("SigleUnite")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("sigleunite");

                    b.HasKey("IdUnite")
                        .HasName("pk_unite");

                    b.ToTable("unite", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.UniteCapteur", b =>
                {
                    b.Property<int>("IdCapteur")
                        .HasColumnType("integer")
                        .HasColumnName("idcapteur");

                    b.Property<int>("IdUnite")
                        .HasColumnType("integer")
                        .HasColumnName("idunite");

                    b.HasKey("IdCapteur", "IdUnite")
                        .HasName("pk_unite_capteur");

                    b.HasIndex("IdUnite");

                    b.ToTable("unite_capteur", (string)null);
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Capteur", b =>
                {
                    b.HasOne("API_OVH.Models.EntityFramework.Mur", "MurNavigation")
                        .WithMany("Capteurs")
                        .HasForeignKey("IdMur")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_capteur_reference_mur");

                    b.Navigation("MurNavigation");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Equipement", b =>
                {
                    b.HasOne("API_OVH.Models.EntityFramework.Mur", "MurNavigation")
                        .WithMany("Equipements")
                        .HasForeignKey("IdMur")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_equipeme_lie_mur");

                    b.HasOne("API_OVH.Models.EntityFramework.TypeEquipement", "TypeEquipementNavigation")
                        .WithMany("Equipements")
                        .HasForeignKey("IdTypeEquipement")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_equipeme_est_typeequi");

                    b.Navigation("MurNavigation");

                    b.Navigation("TypeEquipementNavigation");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Mur", b =>
                {
                    b.HasOne("API_OVH.Models.EntityFramework.Direction", "DirectionNavigation")
                        .WithMany("Murs")
                        .HasForeignKey("IdDirection")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_mur_est_orien_directio");

                    b.HasOne("API_OVH.Models.EntityFramework.Salle", "SalleNavigation")
                        .WithMany("Murs")
                        .HasForeignKey("IdSalle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_mur_contient_salle");

                    b.Navigation("DirectionNavigation");

                    b.Navigation("SalleNavigation");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Salle", b =>
                {
                    b.HasOne("API_OVH.Models.EntityFramework.Batiment", "BatimentNavigation")
                        .WithMany("Salles")
                        .HasForeignKey("IdBatiment")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_salle_se_trouve_batiment");

                    b.HasOne("API_OVH.Models.EntityFramework.TypeSalle", "TypeSalleNavigation")
                        .WithMany("Salles")
                        .HasForeignKey("IdTypeSalle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_salle_est_quali_typesall");

                    b.Navigation("BatimentNavigation");

                    b.Navigation("TypeSalleNavigation");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.UniteCapteur", b =>
                {
                    b.HasOne("API_OVH.Models.EntityFramework.Capteur", "CapteurNavigation")
                        .WithMany("UnitesCapteur")
                        .HasForeignKey("IdCapteur")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_unicapt_capt");

                    b.HasOne("API_OVH.Models.EntityFramework.Unite", "UniteNavigation")
                        .WithMany("UnitesCapteur")
                        .HasForeignKey("IdUnite")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_unicapt_uni");

                    b.Navigation("CapteurNavigation");

                    b.Navigation("UniteNavigation");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Batiment", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Capteur", b =>
                {
                    b.Navigation("UnitesCapteur");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Direction", b =>
                {
                    b.Navigation("Murs");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Mur", b =>
                {
                    b.Navigation("Capteurs");

                    b.Navigation("Equipements");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Salle", b =>
                {
                    b.Navigation("Murs");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.TypeEquipement", b =>
                {
                    b.Navigation("Equipements");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.TypeSalle", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("API_OVH.Models.EntityFramework.Unite", b =>
                {
                    b.Navigation("UnitesCapteur");
                });
#pragma warning restore 612, 618
        }
    }
}
