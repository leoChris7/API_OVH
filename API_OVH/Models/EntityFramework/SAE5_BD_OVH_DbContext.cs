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
                entity.ToTable("SALLE");

                // Clé primaire
                entity.HasKey(e => e.IdSalle)
                    .HasName("PK_SALLE");

                entity.Property(e => e.IdSalle).HasColumnName("IDSALLE");
                entity.Property(e => e.IdTypeSalle).HasColumnName("IDTYPESALLE");
                entity.Property(e => e.IdBatiment).HasColumnName("IDBATIMENT");
                entity.Property(e => e.NomSalle).HasColumnName("NOMSALLE");

                // Colonnes
                entity.HasOne(d => d.TypeSalleNavigation)
                    .WithMany(p => p.Salles)
                    .HasForeignKey(d => d.IdTypeSalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SALLE_EST_QUALI_TYPESALL");

                // Clés étrangères
                entity.HasOne(d => d.BatimentNavigation)
                   .WithMany(p => p.Salles)
                   .HasForeignKey(d => d.IdBatiment)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_SALLE_SE_TROUVE_BATIMENT");

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_salle_superficie", "\"SUPERFICIESALLE\" >= 0"));
            });

            modelBuilder.Entity<Batiment>(entity =>
            {
                // Nom de la table
                entity.ToTable("BATIMENT");

                // Clé primaire
                entity.HasKey(e => e.IdBatiment)
                    .HasName("PK_BATIMENT");

                // Colonnes
                entity.Property(e => e.IdBatiment).HasColumnName("BATIMENTID");
                entity.Property(e => e.NomBatiment).HasColumnName("NOMBATIMENT");
            });

            modelBuilder.Entity<Capteur>(entity =>
            {
                // Nom de la table
                entity.ToTable("CAPTEUR");

                // Clé primaire
                entity.HasKey(e => e.IdCapteur)
                    .HasName("PK_CAPTEUR");

                // Colonnes
                entity.Property(e => e.IdCapteur)
                    .HasColumnName("idcapteur")
                    .IsRequired();

                entity.Property(e => e.IdSalle)
                    .HasColumnName("IDSALLE");

                entity.Property(e => e.NomCapteur)
                    .HasColumnName("NOMTYPECAPTEUR")
                    .HasMaxLength(25);

                entity.Property(e => e.EstActif)
                    .HasColumnName("ESTACTIF")
                    .HasMaxLength(3)
                    .HasDefaultValue("NSP");

                entity.Property(e => e.XCapteur)
                    .HasColumnName("XCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.YCapteur)
                    .HasColumnName("YCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.ZCapteur)
                    .HasColumnName("ZCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                // Clés étrangères
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany()
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("FK_CAPTEUR_REFERENCE_SALLE")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_capteur_actif", "\"ESTACTIF\" IN ('OUI', 'NON', 'NSP')"));

            });

            modelBuilder.Entity<Direction>(entity =>
            {
                // Nom de la table
                entity.ToTable("DIRECTION");

                // Clé primaire
                entity.HasKey(e => e.IdDirection)
                      .HasName("PK_DIRECTION");

                // Colonnes
                entity.Property(e => e.IdDirection)
                      .HasColumnName("IDDIRECTION");

                entity.Property(e => e.LettresDirection)
                      .HasColumnName("LETTRES_DIRECTION")
                      .HasColumnType("varchar(2)")
                      .IsRequired();

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_direction", "\"LETTRES_DIRECTION\" IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')"));

            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                // Nom de la table
                entity.ToTable("EQUIPEMENT");

                // Clé primaire
                entity.HasKey(e => e.IdEquipement)
                      .HasName("PK_EQUIPEMENT");

                // Colonnes
                entity.Property(e => e.IdEquipement)
                      .HasColumnName("IDEQUIPEMENT");

                entity.Property(e => e.IdSalle)
                      .HasColumnName("IDSALLE")
                      .IsRequired();

                entity.Property(e => e.IdTypeEquipement)
                      .HasColumnName("IDTYPEEQUIPEMENT")
                      .IsRequired();

                entity.Property(e => e.NomEquipement)
                      .HasColumnName("NOMEQUIPEMENT")
                      .HasColumnType("varchar(20)")
                      .IsRequired();

                entity.Property(e => e.Longueur)
                      .HasColumnName("LONGUEUR")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Largeur)
                      .HasColumnName("LARGEUR")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Hauteur)
                      .HasColumnName("HAUTEUR")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.XEquipement)
                      .HasColumnName("XEQUIPEMENT")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.YEquipement)
                      .HasColumnName("YEQUIPEMENT")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.ZEquipement)
                      .HasColumnName("ZEQUIPEMENT")
                      .HasColumnType("numeric(10,1)")
                      .HasDefaultValue(0);

                entity.Property(e => e.EstActif)
                      .HasColumnName("ESTACTIF")
                      .HasColumnType("char(3)")
                      .HasDefaultValue("NSP");

                // Clés étrangères
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany(p => p.Equipements)
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("FK_EQUIPEME_EST_DANS_SALLE")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TypeEquipementNavigation)
                    .WithMany(p => p.Equipements)
                    .HasForeignKey(e => e.IdTypeEquipement)
                    .HasConstraintName("FK_EQUIPEME_EST_TYPEEQUI")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_actif", "\"ESTACTIF\" IN ('OUI', 'NON', 'NSP')"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_long", "\"LONGUEUR\" >= 0"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_larg", "\"LARGEUR\" >= 0"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_haut", "\"HAUTEUR\" >= 0"));

            });

            modelBuilder.Entity<Mur>(entity =>
            {
                // Nom de la table
                entity.ToTable("MUR");

                // Clé primaire
                entity.HasKey(e => e.IdMur) 
                      .HasName("PK_MUR");

                // Colonnes
                entity.Property(e => e.IdMur)
                      .HasColumnName("IDMUR");

                entity.Property(e => e.IdDirection)
                      .HasColumnName("IDDIRECTION")
                      .IsRequired();

                entity.Property(e => e.IdSalle)
                      .HasColumnName("IDSALLE")
                      .IsRequired();

                entity.Property(e => e.Longueur)
                      .HasColumnName("LONGUEUR")
                      .HasColumnType("numeric")
                      .HasDefaultValue(0);

                entity.Property(e => e.Hauteur)
                      .HasColumnName("HAUTEUR")
                      .HasColumnType("numeric(4,2)")
                      .HasDefaultValue(0);

                entity.Property(e => e.Orientation)
                      .HasColumnName("ORIENTATION")
                      .HasColumnType("numeric(8,6)")
                      .HasDefaultValue(0);

                // Clés étrangères
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany(p => p.Murs)
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("FK_MUR_CONTIENT_SALLE")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DirectionNavigation)
                    .WithMany(p => p.Murs)
                    .HasForeignKey(e => e.IdDirection)
                    .HasConstraintName("FK_MUR_EST_ORIEN_DIRECTIO")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes CHECK
                entity.ToTable(t => t.HasCheckConstraint("chk_longueur", "\"LONGUEUR\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_hauteur", "\"HAUTEUR\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("chk_orientation_degrees", "\"ORIENTATION\" >= 0 AND \"ORIENTATION\" <= 360"));

            });

            modelBuilder.Entity<TypeEquipement>(entity =>
            {
                // Nom de la table
                entity.ToTable("TYPEEQUIPEMENT");

                // Clé primaire
                entity.HasKey(e => e.IdTypeEquipement)
                      .HasName("PK_TYPEEQUIPEMENT");

                // Colonnes
                entity.Property(e => e.IdTypeEquipement)
                      .HasColumnName("IDTYPEEQUIPEMENT");

                entity.Property(e => e.NomTypeEquipement)
                      .HasColumnName("NOMTYPEEQUIPEMENT")
                      .HasColumnType("varchar(20)")
                      .IsRequired();
            });

            modelBuilder.Entity<TypeSalle>(entity =>
            {
                // Nom de la table
                entity.ToTable("TYPESALLE");

                // Clé primaire
                entity.HasKey(e => e.IdTypeSalle)
                    .HasName("PK_TYPESALLE");

                // Colonnes
                entity.Property(e => e.IdTypeSalle)
                      .HasColumnName("IDTYPESALLE");

                entity.Property(e => e.NomTypeSalle)
                      .HasColumnName("NOMTYPESALLE")
                      .HasColumnType("varchar(20)")
                      .IsRequired();


            });

            modelBuilder.Entity<Unite>(entity =>
            {
                // Nom de la table
                entity.ToTable("UNITE");
 
                // Clé primaire
                entity.HasKey(e => e.IdUnite)
                      .HasName("PK_UNITE");

                // Colonnes
                entity.Property(e => e.IdUnite)
                      .HasColumnName("IDUNITE");

                entity.Property(e => e.NomUnite)
                      .HasColumnName("NOMUNITE")
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.SigleUnite)
                      .HasColumnName("SIGLEUNITE")
                      .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<UniteCapteur>(entity =>
            {
                // Nom de la table
                entity.ToTable("UNITE_CAPTEUR");

                // Clé primaire
                entity.HasKey(e => new { e.IdCapteur, e.IdUnite })
                      .HasName("PK_UNITE_CAPTEUR");

                // Colonnes
                entity.Property(e => e.IdCapteur)
                    .HasColumnName("IDCAPTEUR");

                entity.Property(e => e.IdUnite)
                    .HasColumnName("IDUNITE");

                // Clé étrangère
                entity.HasOne(d => d.CapteurNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(e => e.IdCapteur)
                    .HasConstraintName("FK_UNICAPT_CAPT")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.UniteNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(e => e.IdUnite)
                    .HasConstraintName("FK_UNICAPT_UNI")
                    .OnDelete(DeleteBehavior.Restrict);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
