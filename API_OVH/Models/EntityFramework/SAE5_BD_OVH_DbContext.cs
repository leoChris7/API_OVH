using Microsoft.EntityFrameworkCore;

namespace API_OVH.Models.EntityFramework
{
    public partial class SAE5_BD_OVH_DbContext: DbContext
    {
        public SAE5_BD_OVH_DbContext() { }
        public SAE5_BD_OVH_DbContext(DbContextOptions<SAE5_BD_OVH_DbContext> options) : base(options) { }

        public virtual DbSet<Batiment> Batiments { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }
        public virtual DbSet<Capteur> Capteurs { get; set; }
        public virtual DbSet<Equipement> Equipements { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Mur> Murs { get; set; }
        public virtual DbSet<TypeEquipement> TypesEquipement { get; set; }
        public virtual DbSet<TypeSalle> TypesSalle { get; set; }
        public virtual DbSet<Unite> Unites { get; set; }
        public virtual DbSet<UniteCapteur> UnitesCapteur { get; set; }


        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    if (!optionsBuilder.IsConfigured)
                    {
                        optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=SAE5_BD_OVH; uid=gehdor; password=postgres;");
                    }
                }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salle>(entity =>
            {
                // Nom de la table
                entity.ToTable("salle");

                // Clé primaire
                entity.HasKey(e => e.IdSalle)
                    .HasName("pk_salle");

                entity.Property(e => e.IdSalle).HasColumnName("idsalle");
                entity.Property(e => e.IdTypeSalle).HasColumnName("idtypesalle");
                entity.Property(e => e.IdBatiment).HasColumnName("idbatiment");
                entity.Property(e => e.NomSalle).HasColumnName("nomsalle");

                // Colonnes
                entity.HasOne(d => d.TypeSalleNavigation)
                    .WithMany(p => p.Salles)
                    .HasForeignKey(d => d.IdTypeSalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_salle_est_quali_typesall");

                // Clés étrangères
                entity.HasOne(d => d.BatimentNavigation)
                   .WithMany(p => p.Salles)
                   .HasForeignKey(d => d.IdBatiment)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("fk_salle_se_trouve_batiment");
            });

            modelBuilder.Entity<Batiment>(entity =>
            {
                // Nom de la table
                entity.ToTable("batiment");

                // Clé primaire
                entity.HasKey(e => e.IdBatiment)
                    .HasName("pk_batiment");

                // Colonnes
                entity.Property(e => e.IdBatiment).HasColumnName("batimentid");
                entity.Property(e => e.NomBatiment).HasColumnName("nombatiment");
            });

            modelBuilder.Entity<Capteur>(entity =>
            {
                // Nom de la table
                entity.ToTable("capteur");

                // Clé primaire
                entity.HasKey(e => e.IdCapteur)
                    .HasName("pk_capteur");

                // Colonnes
                entity.Property(e => e.IdCapteur)
                    .HasColumnName("idcapteur")
                    .IsRequired();

                entity.Property(e => e.IdMur)
                    .HasColumnName("idmur");

                entity.Property(e => e.NomCapteur)
                    .HasColumnName("nomcapteur")
                    .HasMaxLength(25);

                entity.Property(e => e.EstActif)
                    .HasColumnName("estactif")
                    .HasMaxLength(3)
                    .HasDefaultValue("NSP");

                entity.Property(e => e.XCapteur)
                    .HasColumnName("xcapteur")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.YCapteur)
                    .HasColumnName("ycapteur")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.ZCapteur)
                    .HasColumnName("zcapteur")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                // Clés étrangères
                entity.HasOne(d => d.MurNavigation)
                    .WithMany(m => m.Capteurs)
                    .HasForeignKey(e => e.IdMur)
                    .HasConstraintName("fk_capteur_reference_mur")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_capteur_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')"));
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                // Nom de la table
                entity.ToTable("direction");

                // Clé primaire
                entity.HasKey(e => e.IdDirection)
                      .HasName("pk_direction");

                // Colonnes
                entity.Property(e => e.IdDirection)
                      .HasColumnName("iddirection");

                entity.Property(e => e.LettresDirection)
                      .HasColumnName("lettres_direction")
                      .HasColumnType("varchar(2)")
                      .IsRequired();

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_direction", "\"lettres_direction\" IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')"));
            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                // Nom de la table
                entity.ToTable("equipement");

                // Clé primaire
                entity.HasKey(e => e.IdEquipement)
                      .HasName("pk_equipement");

                // Colonnes
                entity.Property(e => e.IdEquipement)
                      .HasColumnName("idequipement");

                entity.Property(e => e.IdMur)
                      .HasColumnName("idmur")
                      .IsRequired();

                entity.Property(e => e.IdTypeEquipement)
                      .HasColumnName("idtypeequipement")
                      .IsRequired();

                entity.Property(e => e.NomEquipement)
                      .HasColumnName("nomequipement")
                      .HasColumnType("varchar(20)")
                      .IsRequired();

                entity.Property(e => e.Longueur)
                      .HasColumnName("longueur")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Largeur)
                      .HasColumnName("largeur")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Hauteur)
                      .HasColumnName("hauteur")
                      .HasColumnType("numeric(5,2)")
                      .HasDefaultValue(0);

                entity.Property(e => e.XEquipement)
                      .HasColumnName("xequipement")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.YEquipement)
                      .HasColumnName("yequipement")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.ZEquipement)
                      .HasColumnName("zequipement")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.EstActif)
                      .HasColumnName("estactif")
                      .HasColumnType("char(3)")
                      .HasDefaultValue("NSP");

                // Clés étrangères
                entity.HasOne(d => d.MurNavigation)
                    .WithMany(p => p.Equipements)
                    .HasForeignKey(e => e.IdMur)
                    .HasConstraintName("fk_equipeme_lie_mur")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TypeEquipementNavigation)
                    .WithMany(p => p.Equipements)
                    .HasForeignKey(e => e.IdTypeEquipement)
                    .HasConstraintName("fk_equipeme_est_typeequi")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')"));
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_long", "\"longueur\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_larg", "\"largeur\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_haut", "\"hauteur\" >= 0"));
            });

            modelBuilder.Entity<Mur>(entity =>
            {
                // Nom de la table
                entity.ToTable("mur");

                // Clé primaire
                entity.HasKey(e => e.IdMur)
                      .HasName("pk_mur");

                // Colonnes
                entity.Property(e => e.IdMur)
                      .HasColumnName("idmur");

                entity.Property(e => e.IdDirection)
                      .HasColumnName("iddirection")
                      .IsRequired();

                entity.Property(e => e.IdSalle)
                      .HasColumnName("idsalle")
                      .IsRequired();

                entity.Property(e => e.Longueur)
                      .HasColumnName("longueur")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Hauteur)
                      .HasColumnName("hauteur")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Orientation)
                      .HasColumnName("orientation")
                      .HasColumnType("numeric(9,6)")
                      .HasDefaultValue(0);

                // Clés étrangères
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany(p => p.Murs)
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("fk_mur_contient_salle")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DirectionNavigation)
                    .WithMany(p => p.Murs)
                    .HasForeignKey(e => e.IdDirection)
                    .HasConstraintName("fk_mur_est_orien_directio")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes CHECK
                entity.ToTable(t => t.HasCheckConstraint("chk_longueur", "\"longueur\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_hauteur", "\"hauteur\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_orientation_degrees", "\"orientation\" >= 0 AND \"orientation\" <= 360"));

            });

            modelBuilder.Entity<TypeEquipement>(entity =>
            {
                // Nom de la table
                entity.ToTable("typeequipement");

                // Clé primaire
                entity.HasKey(e => e.IdTypeEquipement)
                      .HasName("pk_typeequipement");

                // Colonnes
                entity.Property(e => e.IdTypeEquipement)
                      .HasColumnName("idtypeequipement");

                entity.Property(e => e.NomTypeEquipement)
                      .HasColumnName("nomtypeequipement")
                      .HasColumnType("varchar(20)")
                      .IsRequired();
            });

            modelBuilder.Entity<TypeSalle>(entity =>
            {
                // Nom de la table
                entity.ToTable("typesalle");

                // Clé primaire
                entity.HasKey(e => e.IdTypeSalle)
                    .HasName("pk_typesalle");

                // Colonnes
                entity.Property(e => e.IdTypeSalle)
                      .HasColumnName("idtypesalle");

                entity.Property(e => e.NomTypeSalle)
                      .HasColumnName("nomtypesalle")
                      .HasColumnType("varchar(20)")
                      .IsRequired();


            });

            modelBuilder.Entity<Unite>(entity =>
            {
                // Nom de la table
                entity.ToTable("unite");
 
                // Clé primaire
                entity.HasKey(e => e.IdUnite)
                      .HasName("pk_unite");

                // Colonnes
                entity.Property(e => e.IdUnite)
                      .HasColumnName("idunite");

                entity.Property(e => e.NomUnite)
                      .HasColumnName("nomunite")
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.SigleUnite)
                      .HasColumnName("sigleunite")
                      .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<UniteCapteur>(entity =>
            {
                // Nom de la table
                entity.ToTable("unite_capteur");

                // Clé primaire
                entity.HasKey(e => new { e.IdCapteur, e.IdUnite })
                      .HasName("pk_unite_capteur");

                // Colonnes
                entity.Property(e => e.IdCapteur)
                    .HasColumnName("idcapteur");

                entity.Property(e => e.IdUnite)
                    .HasColumnName("idunite");

                // Clé étrangère
                entity.HasOne(d => d.CapteurNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(e => e.IdCapteur)
                    .HasConstraintName("fk_unicapt_capt")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.UniteNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(e => e.IdUnite)
                    .HasConstraintName("fk_unicapt_uni")
                    .OnDelete(DeleteBehavior.Restrict);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
