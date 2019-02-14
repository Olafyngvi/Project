using AutoServis.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoServis.EF
{
    public class MojContext :DbContext
    {

        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        public MojContext()
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<BrojVrata> brojvrata { get; set; }
     
        public DbSet<Dio> dio { get; set; }
        public DbSet<Gorivo> gorivo { get; set; }
        public DbSet<Grad> grad { get; set; }
        public DbSet<Klijent> klijent { get; set; }
        public DbSet<Marka> marka { get; set; }
        public DbSet<Model> model { get; set; }
        public DbSet<NacinPlacanja> nacinplacanja { get; set; }
        public DbSet<Oprema> oprema { get; set; }
        public DbSet<Osoba> osoba { get; set; }
       public DbSet<Popravke> popravka { get; set; }
        public DbSet<Poslovnica> poslovnica { get; set; }
        public DbSet<Racun> racun { get; set; }  
        public DbSet<StavkeRacuna> stavkeracuna { get; set; }
        public DbSet<TipVozila> tipvozila { get; set; }
        public DbSet<Transmisija> transmisija { get; set; }
        public DbSet<Uposlenik> uposlenik { get; set; }
        public DbSet<VozilaPoslovnice> vozilaposlovnice { get; set; }
        public DbSet<VoziloPopravka> vozilopopravka { get; set; }
        public DbSet<VoziloProdaja> voziloprodaja { get; set; }
        public DbSet<VrstaUposlenika> vrstauposlenika { get; set; }
        public DbSet<Slike> slike { get; set; }
        public DbSet<UpitiVozila> upitivozila { get; set; }
        public DbSet<KontaktUpiti> KontaktUpiti { get; set; }
        public DbSet<AutoServis.Models.DioKategorija> DioKategorija { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Osoba>().HasOne(x => x.Uposlenik).WithOne(x => x.Osoba);
            modelBuilder.Entity<Osoba>().HasOne(x => x.Klijent).WithOne(x => x.Osoba);
            modelBuilder.Entity<Osoba>().HasIndex(e => e.KorisnickoIme).IsUnique();
            


        }





    }

}
