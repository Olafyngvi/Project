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
    [Migration("20180522141831_inicijalno2")]
    partial class inicijalno2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("AutoServis.Models.Racun", b =>
                {
                    b.Property<int>("RacunId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojRacuna");

                    b.Property<DateTime>("Datum");

                    b.Property<bool>("Online");

                    b.Property<double>("Ukupno");

                    b.HasKey("RacunId");

                    b.ToTable("racun");
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

            modelBuilder.Entity("AutoServis.Models.Osoba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresa");

                    b.Property<int>("GradId");

                    b.Property<string>("Ime");

                    b.Property<string>("KorisnickoIme");

                    b.Property<string>("Lozinka");

                    b.Property<string>("Prezime");

                    b.Property<string>("Telefon");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("osoba");
                });

            modelBuilder.Entity("AutoServis.Models.Poslovnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresa");

                    b.Property<int>("GradId");

                    b.Property<string>("Naziv");

                    b.Property<string>("Telefon");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("poslovnica");
                });

            modelBuilder.Entity("AutoServis.Models.Uposlenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<DateTime>("DatumZaposljavanja");

                    b.Property<string>("JMBG");

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

            modelBuilder.Entity("AutoServis.Models.VrstaUposlenika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.ToTable("vrstauposlenika");
                });

            modelBuilder.Entity("AutoServis.Models.Dio", b =>
                {
                    b.HasOne("AutoServis.Models.Model", "model")
                        .WithMany()
                        .HasForeignKey("ModelID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Model", b =>
                {
                    b.HasOne("AutoServis.Models.Marka", "marka")
                        .WithMany()
                        .HasForeignKey("MarkaID")
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

            modelBuilder.Entity("AutoServis.Models.Klijent", b =>
                {
                    b.HasOne("AutoServis.Models.Osoba", "Osoba")
                        .WithOne("Klijent")
                        .HasForeignKey("AutoServis.Models.Klijent", "OsobaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Osoba", b =>
                {
                    b.HasOne("AutoServis.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AutoServis.Models.Poslovnica", b =>
                {
                    b.HasOne("AutoServis.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
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
#pragma warning restore 612, 618
        }
    }
}
