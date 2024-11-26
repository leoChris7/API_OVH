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
        public virtual DbSet<UniteCapteur> UnitesCapteur { get; set; }
        public virtual DbSet<TypeSalle> TypesSalle { get; set; }
        public virtual DbSet<Unite> Unites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=SAE5_BD_OVH_LOCALE; uid=postgres; password=postgres;");
            }
        }

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
                entity.Property(e => e.SuperficieSalle).HasColumnName("SUPERFICIESALLE");

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
                entity.Property(e => e.IdCapteur).HasColumnName("IDCAPTEUR");
                entity.Property(e => e.IdSalle).HasColumnName("IDSALLE");
                entity.Property(e => e.NomCapteur).HasColumnName("NOMCAPTEUR")
                    .HasMaxLength(25);

                entity.Property(e => e.EstActif).HasColumnName("ESTACTIF")
                    .HasMaxLength(3)
                    .HasDefaultValue("NSP");

                entity.Property(e => e.XCapteur).HasColumnName("XCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.YCapteur).HasColumnName("YCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                entity.Property(e => e.ZCapteur).HasColumnName("ZCAPTEUR")
                    .HasColumnType("numeric(10,1)")
                    .HasDefaultValue(0);

                // Clés étrangères
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany(p => p.Capteurs)
                    .HasForeignKey(d => d.IdSalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CAPTEUR_REFERENCE_SALLE");

                // Contrainte addtionnelle

                entity.ToTable(t => t.HasCheckConstraint("chk_capteur_actif", "ESTACTIF IN ('OUI', 'NON', 'NSP')"));

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
            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                entity.HasKey(e => e.IdEquipement)
                    .HasName("pk_equip_id");
            });

            modelBuilder.Entity<Mur>(entity =>
            {
                entity.HasKey(e => e.IdMur)
                    .HasName("pk_mur_id");
            });

            modelBuilder.Entity<TypeEquipement>(entity =>
            {
                entity.HasKey(e => e.IdTypeEquipement)
                    .HasName("pk_typEquip_id");
            });

            modelBuilder.Entity<TypeSalle>(entity =>
            {
                entity.HasKey(e => e.IdTypeSalle)
                    .HasName("pk_typSalle_id");
            });

            modelBuilder.Entity<Unite>(entity =>
            {
                entity.HasKey(e => e.IdUnite)
                    .HasName("pk_unite");
            });

            modelBuilder.Entity<UniteCapteur>(entity =>
            {
                entity.HasKey(e => new { e.IdCapteur, e.IdUnite })
                    .HasName("pk_unite_capteur");

                entity.HasOne(d => d.CapteurNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(d => d.IdCapteur)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_unicapt_capt");

                entity.HasOne(d => d.UniteNavigation)
                    .WithMany(p => p.UnitesCapteur)
                    .HasForeignKey(d => d.IdUnite)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UNICAPT_UNI");
            });

            modelBuilder.Entity<ValeurEquipement>(entity =>
            {
                entity.HasKey(e => new { e.IdUnite, e.IdCaracteristique })
                    .HasName("pk_valUniteCaract");


            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
