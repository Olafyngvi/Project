﻿// <auto-generated />
using AutoServis.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AutoServis.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20180727115206_test1")]
    partial class test1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoServis.Models.BrojVrata", b =>
                {
                    b.Property<int>("BrojVrataId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("BrojVrataId");

                    b.ToTable("brojvrata");
                });

            modelBuilder.Entity("AutoServis.Models.DetaljiProdaje", b =>
                {
                    b.Property<int>("DetaljiProdajeId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<DateTime>("DatumProdaje");

                    b.Property<int>("KlijentID");

                    b.Property<int>("NacinPlacanjaID");

                    b.Property<int>("UposlenikID");

                    b.Property<int>("VoziloProdajaID");

                    b.HasKey("DetaljiProdajeId");

                    b.HasIndex("KlijentID");

                    b.HasIndex("NacinPlacanjaID");

                    b.HasIndex("UposlenikID");

                    b.HasIndex("VoziloProdajaID");

                    b.ToTable("detaljiprodaje");
                });

            modelBuilder.Entity("AutoServis.Models.Dio", b =>
                {
                    b.Property<int>("DioId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<int>("ModelID");

                    b.Property<string>("Naziv");

                    b.Property<string>("Sifra");

                    b.HasKey("DioId");

                    b.HasIndex("ModelID");

                    b.ToTable("dio");
                });

            modelBuilder.Entity("AutoServis.Models.Gorivo", b =>
                {
                    b.Property<int>("GorivoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("GorivoId");

                    b.ToTable("gorivo");
                });

            modelBuilder.Entity("AutoServis.Models.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("grad");
                });

            modelBuilder.Entity("AutoServis.Models.Klijent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRegistracije");

                    b.Property<int>("OsobaId");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId")
                        .IsUnique();

                    b.ToTable("klijent");
                });

            modelBuilder.Entity("AutoServis.Models.Marka", b =>
                {
                    b.Property<int>("MarkaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nazvi");

                    b.HasKey("MarkaId");

                    b.ToTable("marka");
                });

            modelBuilder.Entity("AutoServis.Models.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MarkaID");

                    b.Property<string>("Naziv");

                    b.HasKey("ModelId");

                    b.HasIndex("MarkaID");

                    b.ToTable("model");
                });

            modelBuilder.Entity("AutoServis.Models.NacinPlacanja", b =>
                {
                    b.Property<int>("NacinPlacanjaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("VrstaPlacanja");

                    b.HasKey("NacinPlacanjaId");

                    b.ToTable("nacinplacanja");
                });

            modelBuilder.Entity("AutoServis.Models.Oprema", b =>
                {
                    b.Property<int>("OpremaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.HasKey("OpremaId");

                    b.ToTable("oprema");
                });

            modelBuilder.Entity("AutoServis.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime>("DatumNarudzbe");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("Ukupno");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AutoServis.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<int>("DioId");

                    b.Property<int>("Kolicina");

                    b.Property<int>("OrderId");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("DioId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("AutoServis.Models.Osoba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresa")
                        .IsRequired();

                    b.Property<int>("GradId");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("KorisnickoIme")
                        .IsRequired();

                    b.Property<string>("Lozinka")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<string>("Telefon");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.HasIndex("KorisnickoIme")
                        .IsUnique();

                    b.ToTable("osoba");
                });

            modelBuilder.Entity("AutoServis.Models.Popravke", b =>
                {
                    b.Property<int>("PopravkeId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CijenaPopravke");

                    b.Property<DateTime>("DatumPopravke");

                    b.Property<string>("OpisKvara");

                    b.Property<int>("PoslovnicaID");

                    b.Property<int>("UposlenikID");

                    b.HasKey("PopravkeId");

                    b.HasIndex("PoslovnicaID");

                    b.HasIndex("UposlenikID");

                    b.ToTable("popravka");
                });

            modelBuilder.Entity("AutoServis.Models.Poslovnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresa");

                    b.Property<int>("GradId");

                    b.Property<string>("Naziv");

                    b.Property<string>("Telefon");

                    b.Property<bool>("Zatvorena");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("poslovnica");
                });

            modelBuilder.Entity("AutoServis.Models.Racun", b =>
                {
                    b.Property<int>("RacunId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojRacuna");

                    b.Property<DateTime>("Datum");

                    b.Property<int>("NacinPlacanjaID");

                    b.Property<bool>("Online");

                    b.Property<double>("Ukupno");

                    b.HasKey("RacunId");

                    b.HasIndex("NacinPlacanjaID");

                    b.ToTable("racun");
                });

            modelBuilder.Entity("AutoServis.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("DioId");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("DioId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("AutoServis.Models.StavkeRacuna", b =>
                {
                    b.Property<int>("StavkeRacunaId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<int>("DioID");

                    b.Property<int>("Kolicina");

                    b.Property<int>("PopravkeID");

                    b.Property<int>("RacunID");

                    b.HasKey("StavkeRacunaId");

                    b.HasIndex("DioID");

                    b.HasIndex("PopravkeID");

                    b.HasIndex("RacunID");

                    b.ToTable("stavkeracuna");
                });

            modelBuilder.Entity("AutoServis.Models.StavkeRacunaOnline", b =>
                {
                    b.Property<int>("StavkeRacunaOnlineId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<int>("DioID");

                    b.Property<int>("KlijentID");

                    b.Property<int>("Kolicina");

                    b.Property<int>("RacunID");

                    b.HasKey("StavkeRacunaOnlineId");

                    b.HasIndex("DioID");

                    b.HasIndex("KlijentID");

                    b.HasIndex("RacunID");

                    b.ToTable("stavkeracunaonline");
                });

            modelBuilder.Entity("AutoServis.Models.TipVozila", b =>
                {
                    b.Property<int>("TipVozilaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("TipVozilaId");

                    b.ToTable("tipvozila");
                });

            modelBuilder.Entity("AutoServis.Models.Transmisija", b =>
                {
                    b.Property<int>("TransmisijaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("TransmisijaId");

                    b.ToTable("transmisija");
                });

            modelBuilder.Entity("AutoServis.Models.Uposlenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<DateTime>("DatumZaposljavanja");

                    b.Property<string>("JMBG");

                    b.Property<bool>("Neaktivan");

                    b.Property<int?>("OsobaId");

                    b.Property<int>("PoslovnicaId");

                    b.Property<int>("VrstaUposlenikaId");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId")
                        .IsUnique()
                        .HasFilter("[OsobaId] IS NOT NULL");

                    b.HasIndex("PoslovnicaId");

                    b.HasIndex("VrstaUposlenikaId");

                    b.ToTable("uposlenik");
                });

            modelBuilder.Entity("AutoServis.Models.VozilaPoslovnice", b =>
                {
                    b.Property<int>("VozilaPoslovniceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumUvoza");

                    b.Property<int>("PoslovnicaID");

                    b.Property<int>("VoziloProdajaID");

                    b.HasKey("VozilaPoslovniceId");

                    b.HasIndex("PoslovnicaID");

                    b.HasIndex("VoziloProdajaID");

                    b.ToTable("vozilaposlovnice");
                });

            modelBuilder.Entity("AutoServis.Models.VoziloPopravka", b =>
                {
                    b.Property<int>("VoziloPopravkaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojMotora");

                    b.Property<string>("BrojSasije");

                    b.Property<int>("GodinaProizvodnje");

                    b.Property<int>("GorivoID");

                    b.Property<int>("KlijentID");

                    b.Property<int>("ModelID");

                    b.Property<int>("PopravkeID");

                    b.Property<string>("RegistracijskaOznaka");

                    b.Property<int>("SnagaMotora");

                    b.HasKey("VoziloPopravkaID");

                    b.HasIndex("GorivoID");

                    b.HasIndex("KlijentID");

                    b.HasIndex("ModelID");

                    b.HasIndex("PopravkeID");

                    b.ToTable("vozilopopravka");
                });

            modelBuilder.Entity("AutoServis.Models.VoziloProdaja", b =>
                {
                    b.Property<int>("VoziloProdajaID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojVrataID");

                    b.Property<double>("Cijena");

                    b.Property<DateTime>("DatumProizvodnje");

                    b.Property<int>("GorivoID");

                    b.Property<string>("Kilometraza");

                    b.Property<string>("Kubikaza");

                    b.Property<int>("ModelID");

                    b.Property<int>("OpremaID");

                    b.Property<string>("SifraAutomobila");

                    b.Property<string>("SnagaMotora");

                    b.Property<int>("TipVozilaID");

                    b.Property<int>("TransmisijaID");

                    b.Property<bool>("isDeleted");

                    b.HasKey("VoziloProdajaID");

                    b.HasIndex("BrojVrataID");

                    b.HasIndex("GorivoID");

                    b.HasIndex("ModelID");

                    b.HasIndex("OpremaID");

                    b.HasIndex("TipVozilaID");

                    b.HasIndex("TransmisijaID");

                    b.ToTable("voziloprodaja");
                });

            modelBuilder.Entity("AutoServis.Models.VrstaUposlenika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.ToTable("vrstauposlenika");
                });

            modelBuilder.Entity("AutoServis.Models.DetaljiProdaje", b =>
                {
                    b.HasOne("AutoServis.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.NacinPlacanja", "NacinPlacanja")
                        .WithMany()
                        .HasForeignKey("NacinPlacanjaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Uposlenik", "Uposlenik")
                        .WithMany()
                        .HasForeignKey("UposlenikID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.VoziloProdaja", "Vozilo")
                        .WithMany()
                        .HasForeignKey("VoziloProdajaID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Dio", b =>
                {
                    b.HasOne("AutoServis.Models.Model", "model")
                        .WithMany()
                        .HasForeignKey("ModelID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Klijent", b =>
                {
                    b.HasOne("AutoServis.Models.Osoba", "Osoba")
                        .WithOne("Klijent")
                        .HasForeignKey("AutoServis.Models.Klijent", "OsobaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Model", b =>
                {
                    b.HasOne("AutoServis.Models.Marka", "marka")
                        .WithMany()
                        .HasForeignKey("MarkaID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.OrderDetails", b =>
                {
                    b.HasOne("AutoServis.Models.Dio", "Dio")
                        .WithMany()
                        .HasForeignKey("DioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Osoba", b =>
                {
                    b.HasOne("AutoServis.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Popravke", b =>
                {
                    b.HasOne("AutoServis.Models.Poslovnica", "Poslovnica")
                        .WithMany()
                        .HasForeignKey("PoslovnicaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Uposlenik", "Uposlenik")
                        .WithMany()
                        .HasForeignKey("UposlenikID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Poslovnica", b =>
                {
                    b.HasOne("AutoServis.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Racun", b =>
                {
                    b.HasOne("AutoServis.Models.NacinPlacanja", "nacinPlacanja")
                        .WithMany()
                        .HasForeignKey("NacinPlacanjaID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("AutoServis.Models.Dio", "Dio")
                        .WithMany()
                        .HasForeignKey("DioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.StavkeRacuna", b =>
                {
                    b.HasOne("AutoServis.Models.Dio", "Dio")
                        .WithMany()
                        .HasForeignKey("DioID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Popravke", "Popravke")
                        .WithMany()
                        .HasForeignKey("PopravkeID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Racun", "Racun")
                        .WithMany()
                        .HasForeignKey("RacunID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.StavkeRacunaOnline", b =>
                {
                    b.HasOne("AutoServis.Models.Dio", "Dio")
                        .WithMany()
                        .HasForeignKey("DioID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Racun", "Racun")
                        .WithMany()
                        .HasForeignKey("RacunID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Uposlenik", b =>
                {
                    b.HasOne("AutoServis.Models.Osoba", "Osoba")
                        .WithOne("Uposlenik")
                        .HasForeignKey("AutoServis.Models.Uposlenik", "OsobaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Poslovnica", "Poslovnica")
                        .WithMany()
                        .HasForeignKey("PoslovnicaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.VrstaUposlenika", "VrstaUposlenika")
                        .WithMany()
                        .HasForeignKey("VrstaUposlenikaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.VozilaPoslovnice", b =>
                {
                    b.HasOne("AutoServis.Models.Poslovnica", "Poslovnica")
                        .WithMany()
                        .HasForeignKey("PoslovnicaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.VoziloProdaja", "VoziloProdaja")
                        .WithMany()
                        .HasForeignKey("VoziloProdajaID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.VoziloPopravka", b =>
                {
                    b.HasOne("AutoServis.Models.Gorivo", "Gorivo")
                        .WithMany()
                        .HasForeignKey("GorivoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Popravke", "Popravke")
                        .WithMany()
                        .HasForeignKey("PopravkeID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.VoziloProdaja", b =>
                {
                    b.HasOne("AutoServis.Models.BrojVrata", "BrojVrata")
                        .WithMany()
                        .HasForeignKey("BrojVrataID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Gorivo", "Gorivo")
                        .WithMany()
                        .HasForeignKey("GorivoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Oprema", "Oprema")
                        .WithMany()
                        .HasForeignKey("OpremaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.TipVozila", "TipVozila")
                        .WithMany()
                        .HasForeignKey("TipVozilaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AutoServis.Models.Transmisija", "Transmisija")
                        .WithMany()
                        .HasForeignKey("TransmisijaID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
