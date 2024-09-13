using FAV_Site.Models;
using Microsoft.EntityFrameworkCore;

namespace FAV_Site.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AdminModels> Admins { get; set; }
        public DbSet<CommandeModels> Commandes { get; set; }
        public DbSet<CommentaireModels> Commentaires { get; set; }
        public DbSet<PanierModels> PanierModels {  get; set; }
        public DbSet<HistoriqueModels> Historiques { get; set; }
        public DbSet<ImageModel> ImageProduit { get; set; }
        public DbSet<StatMois> Mois { get; set; }
        public DbSet<Log_UtilisateurModels> LoginUtilisateur {  get; set; }
        public DbSet<ProduitModels> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProduitModels>()
                .Property(e => e.Date_pub)
                .HasColumnType("date");
            modelBuilder.Entity<PubliciteModels>()
                .Property(p => p.Date_pub)
                .HasColumnType("date");
            modelBuilder.Entity<CommandeModels>()
                .Property(p => p.Date_pub)
                .HasColumnType("date");
            modelBuilder.Entity<HistoriqueModels>()
                .Property(p => p.Date_achat)
                .HasColumnType("date");
        }
        public DbSet<PubliciteModels> Publicites { get; set; }
        public DbSet<Uti_vendeurModels> Vendeurs { get; set; }
        public DbSet<UtilisateurModels> Utilisateurs { get; set; }
    }
}
