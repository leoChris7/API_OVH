using Microsoft.EntityFrameworkCore;

namespace API_OVH.Models.EntityFramework
{
    public partial class SAE5_BD_OVH_DbContext: DbContext
    {
        public SAE5_BD_OVH_DbContext() { }
        public SAE5_BD_OVH_DbContext(DbContextOptions<SAE5_BD_OVH_DbContext> options) : base(options) { }

        public virtual DbSet<Batiment> Batiment { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }
        public virtual DbSet<Capteur> Capteurs { get; set; }
        public virtual DbSet<Equipement> Equipements { get; set; }
        public virtual DbSet<CaracteristiqueEquipement> CaracteristiqueEquipements { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Mur> Murs { get; set; }
        public virtual DbSet<TypeEquipement> TypesEquipement { get; set; }
        public virtual DbSet<TypeMesure> TypesMesure { get; set; }
        public virtual DbSet<TypeSalle> TypesSalles { get; set; }
        public virtual DbSet<Unite> Unites { get; set; }
        public virtual DbSet<ValeurEquipement> ValeurEquipements { get; set; }

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
                entity.HasKey(e => e.Idsalle)
                    .HasName("pk_salle_id");
            });

            modelBuilder.Entity<Batiment>(entity =>
            {
                entity.HasKey(e => e.Idbatiment)
                    .HasName("pk_batiment_id");
            });

            modelBuilder.Entity<Capteur>(entity =>
            {
                entity.HasKey(e => e.IdCapteur)
                    .HasName("pk_capteur_id");
            });

            modelBuilder.Entity<CaracteristiqueEquipement>(entity =>
            {
                entity.HasKey(e => e.Idcaracteristique)
                    .HasName("pk_caractEquip_id");

            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.Iddirection)
                    .HasName("pk_direction_id");
            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                entity.HasKey(e => e.Idequipement)
                    .HasName("pk_equip_id");
            });

            modelBuilder.Entity<Mur>(entity =>
            {
                entity.HasKey(e => e.IdMur)
                    .HasName("pk_mur_id");
            });

            modelBuilder.Entity<Salle>(entity =>
            {
                entity.HasKey(e => e.Idsalle)
                    .HasName("pk_salle_id");
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
                entity.HasKey(e => new { e.Idunite, e.Idcaracteristique })
                    .HasName("pk_valUniteCaract");
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
