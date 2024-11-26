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
        public virtual DbSet<TypeMesure> TypesMesure { get; set; }
        public virtual DbSet<TypeSalle> TypesSalle { get; set; }
        public virtual DbSet<Unite> Unites { get; set; }
        public virtual DbSet<ValeurEquipement> ValeursEquipement { get; set; }

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

                entity.HasKey(e => e.IdSalle)

                    .HasName("pk_salle_id");
            });

            modelBuilder.Entity<Batiment>(entity =>
            {
                entity.ToTable("batiment");

                entity.Property(e => e.IdBatiment).HasColumnName("batimentid");
                entity.Property(e => e.NomBatiment).HasColumnName("nombatiment");

                entity.HasKey(e => e.IdBatiment)
                    .HasName("pk_batiment_id");
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

                entity.Property(e => e.IdSalle)
                    .HasColumnName("idsalle");

                entity.Property(e => e.IdTypeMesure)
                    .HasColumnName("idtypemesure");

                entity.Property(e => e.NomCapteur)
                    .HasColumnName("nomtypecapteur")
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
                entity.HasOne(d => d.SalleNavigation)
                    .WithMany()
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("fk_capteur_reference_salle")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TypeMesureNavigation)
                    .WithMany()
                    .HasForeignKey(e => e.IdTypeMesure)
                    .HasConstraintName("fk_capteur_reference_type_mes")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_capteur_actif", "estactif IN ('OUI', 'NON', 'NSP')"));

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
                entity.ToTable(t => t.HasCheckConstraint("chk_direction", "LETTRES_DIRECTION IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')"));

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
                    .WithMany()
                    .HasForeignKey(e => e.IdSalle)
                    .HasConstraintName("fk_capteur_reference_salle")
                    .OnDelete(DeleteBehavior.Restrict);

                // Contraintes supplémentaires (CHECK)
                entity.ToTable(t => t.HasCheckConstraint("chk_equip_actif", "ESTACTIF IN ('OUI', 'NON', 'NSP')"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_long", "LONGUEUR >= 0"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_larg", "LARGEUR >= 0"));

                entity.ToTable(t => t.HasCheckConstraint("chk_equip_haut", "HAUTEUR >= 0"));
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

            modelBuilder.Entity<TypeMesure>(entity =>
            {
                entity.HasKey(e => e.IdTypeMesure)
                    .HasName("pk_typMesure_id");
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
