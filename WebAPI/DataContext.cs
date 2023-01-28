using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DbSets

        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Beoordeling> Beoordelingen { get; set; }
        public DbSet<Beoordelingscriteria> Beoordelingscriterium { get; set; }
        public DbSet<Beoordelingsdimensie> Beoordelingsdimensies { get; set; }
        public DbSet<Beoordelingsmodel> Beoordelingsmodellen { get; set; }
        public DbSet<Beoordelingsonderdeel> Beoordelingsonderdelen { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Klas> Klassen { get; set; }
        public DbSet<Leerdoel> Leerdoelen { get; set; }
        public DbSet<Leeruitkomst> Leeruitkomsten { get; set; }
        public DbSet<Les> Lessen { get; set; }
        public DbSet<Lesmateriaal> Lesmaterialen { get; set; }
        public DbSet<LesmateriaalInhoud> LesmateriaalInhoud { get; set; }
        public DbSet<LesmateriaalType> LesmateriaalTypes { get; set; }
        public DbSet<Onderwijseenheid> Onderwijseenheden { get; set; }
        public DbSet<Onderwijsmodule> Onderwijsmodules { get; set; }
        public DbSet<Onderwijsuitvoering> Onderwijsuitvoeringen { get; set; }
        public DbSet<Opleiding> Opleidingen { get; set; }
        public DbSet<Opleidingsprofiel> Opleidingsprofielen { get; set; }
        public DbSet<Planning> Planningen { get; set; }
        public DbSet<Rol> Rollen { get; set; }
        public DbSet<Tentamen> Tentamens { get; set; }
        public DbSet<TentamenVanStudent> TentamenVanStudenten { get; set; }
        public DbSet<Toetsinschrijving> Toetsinschrijvingen { get; set; }
        public DbSet<Vorm> Vormen { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auteur>(auteur =>
            {
                auteur
                    .HasIndex(l => l.Naam)
                    .IsUnique();
            });

            modelBuilder.Entity<Beoordeling>(beoordeling =>
            {
                beoordeling
                    .HasOne(b => b.TentamenVanStudent)
                    .WithOne(t => t.Beoordeling)
                    .HasForeignKey<Beoordeling>(t => t.TentamenVanStudentId);
                beoordeling
                    .HasOne(b => b.Docent)
                    .WithMany(d => d.DocentBeoordelingen)
                    .HasForeignKey(b => b.DocentId)
                    .OnDelete(DeleteBehavior.NoAction);
                beoordeling
                    .HasOne(b => b.Beoordelingsmodel)
                    .WithOne()
                    .HasForeignKey<Beoordeling>(b => b.BeoordelingsmodelId);
                beoordeling
                    .Property(b => b.TentamenVanStudentId)
                    .IsRequired(false);

            });

            modelBuilder.Entity<Beoordelingscriteria>(beoordelingscriteria =>
            {
                beoordelingscriteria
                    .HasIndex(b => b.Criteria)
                    .IsUnique();
                beoordelingscriteria
                    .HasOne(b => b.Beoordelingsonderdeel)
                    .WithMany(b => b.Beoordelingscriterium)
                    .HasForeignKey(b => b.BeoordelingsonderdeelId);
                beoordelingscriteria
                    .HasOne(b => b.Leeruitkomst)
                    .WithMany(l => l.Beoordelingscriterium)
                    .HasForeignKey(b => b.LeeruitkomstId);
                beoordelingscriteria
                    .Property(b => b.LeeruitkomstId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Beoordelingsdimensie>(beoordelingsdimensie =>
            {
                beoordelingsdimensie
                    .HasIndex(b => b.Titel)
                    .IsUnique();
                beoordelingsdimensie
                    .HasOne(b => b.Beoordelingscriteria)
                    .WithMany(b => b.Beoordelingsdimensies)
                    .HasForeignKey(b => b.BeoordelingscriteriaId);
            });

            modelBuilder.Entity<Beoordelingsmodel>(beoordelingsmodel =>
            {
                beoordelingsmodel
                    .HasIndex(b => b.Naam)
                    .IsUnique();
                beoordelingsmodel
                    .HasOne(b => b.Tentamen)
                    .WithOne(t => t.Beoordelingsmodel)
                    .HasForeignKey<Beoordelingsmodel>(b => b.TentamenId);
                beoordelingsmodel
                    .HasOne(b => b.Docent)
                    .WithMany(u => u.Beoordelingsmodellen)
                    .HasForeignKey(b => b.DocentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Beoordelingsonderdeel>(beoordelingsonderdeel =>
            {
                beoordelingsonderdeel
                    .HasIndex(b => b.Titel)
                    .IsUnique();
                beoordelingsonderdeel
                    .HasOne(b => b.Beoordelingsmodel)
                    .WithMany(b => b.Beoordelingsonderdelen)
                    .HasForeignKey(b => b.BeoordelingsmodelId);
            });

            modelBuilder.Entity<Gebruiker>(gebruiker =>
            {
                gebruiker
                    .HasIndex(g => g.Email)
                    .IsUnique();
                gebruiker
                    .HasOne(g => g.Rol)
                    .WithMany()
                    .HasForeignKey(g => g.RolId);
                gebruiker
                    .HasOne(g => g.Opleiding)
                    .WithMany()
                    .HasForeignKey(g => g.OpleidingId);
                gebruiker
                    .HasOne(g => g.Opleidingsprofiel)
                    .WithMany()
                    .HasForeignKey(g => g.OpleidingsprofielId);
                gebruiker
                    .HasMany(g => g.Onderwijsmodules)
                    .WithMany(o => o.Docenten)
                    .UsingEntity(eb => eb.ToTable("Gebruiker_Onderwijsmodule"));
                gebruiker
                    .Property(g => g.OpleidingId)
                    .IsRequired(false);
                gebruiker
                    .Property(g => g.OpleidingsprofielId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Klas>(klas =>
            {
                klas
                    .HasIndex(k => k.Klasnaam)
                    .IsUnique();
                klas
                    .HasMany(k => k.Gebruikers)
                    .WithMany(u => u.Klassen)
                    .UsingEntity(eb => eb.ToTable("Klas_Gebruiker"));
                klas
                    .HasMany(k => k.Onderwijsuitvoeringen)
                    .WithMany(u => u.Klassen)
                    .UsingEntity(eb => eb.ToTable("Klas_Onderwijsuitvoering"));
            });

            modelBuilder.Entity<Leerdoel>(leerdoel =>
            {
                leerdoel
                    .HasIndex(l => l.Naam)
                    .IsUnique();
                leerdoel
                    .HasOne(l => l.Onderwijseenheid)
                    .WithMany(o => o.Leerdoelen)
                    .HasForeignKey(l => l.OnderwijseenheidId);
            });

            modelBuilder.Entity<Leeruitkomst>(leeruitkomst =>
            {
                leeruitkomst
                    .HasIndex(l => l.Naam)
                    .IsUnique();
                leeruitkomst
                    .HasOne(l => l.Leerdoel)
                    .WithMany(l => l.Leeruitkomsten)
                    .HasForeignKey(l => l.LeerdoelId);
                leeruitkomst
                    .HasMany(l => l.Tentamens)
                    .WithMany(t => t.Leeruitkomsten)
                    .UsingEntity(eb => eb.ToTable("Leeruitkomst_Tentamen"));
                leeruitkomst
                    .HasMany(l => l.Lessen)
                    .WithMany(t => t.Leeruitkomsten)
                    .UsingEntity(eb => eb.ToTable("Leeruitkomst_Les"));
            });

            modelBuilder.Entity<Les>(les =>
            {
                les
                    .HasMany(l => l.Planningen)
                    .WithMany(p => p.Lessen)
                    .UsingEntity(eb => eb.ToTable("Les_Planning"));
                les
                    .HasMany(l => l.Lesmaterialen)
                    .WithMany(l => l.Lessen)
                    .UsingEntity(eb => eb.ToTable("Les_Lesmateriaal"));
            });

            modelBuilder.Entity<Lesmateriaal>(lesmateriaal =>
            {
                lesmateriaal
                    .HasIndex(l => l.Naam)
                    .IsUnique();
                lesmateriaal
                    .HasOne(l => l.LesmateriaalType)
                    .WithMany()
                    .HasForeignKey(l => l.LesmateriaaltypeId);
                lesmateriaal
                    .HasOne(l => l.Auteur)
                    .WithMany()
                    .HasForeignKey(l => l.AuteurId);
            });

            modelBuilder.Entity<LesmateriaalInhoud>(lesmateriaalInhoud =>
            {
                lesmateriaalInhoud
                    .HasOne(l => l.Lesmateriaal)
                    .WithMany(l => l.LesmateriaalInhoud)
                    .HasForeignKey(l => l.LesmateriaalId);
            });

            modelBuilder.Entity<LesmateriaalType>(lesmateriaalType =>
            {
                lesmateriaalType
                    .HasIndex(l => l.Naam)
                    .IsUnique();
            });

            modelBuilder.Entity<Onderwijseenheid>(onderwijseenheid =>
            {
                onderwijseenheid
                    .HasIndex(o => o.Naam)
                    .IsUnique();
                onderwijseenheid
                    .HasMany(o => o.Onderwijsmodules)
                    .WithMany(o => o.Onderwijseenheden)
                    .UsingEntity(eb => eb.ToTable("Onderwijseenheid_Onderwijsmodule"));
            });

            modelBuilder.Entity<Onderwijsmodule>(onderwijsmodule =>
            {
                onderwijsmodule
                    .HasIndex(f => f.Naam)
                    .IsUnique();
                onderwijsmodule
                    .HasOne(o => o.Opleiding)
                    .WithMany(o => o.Onderwijsmodules)
                    .HasForeignKey(o => o.OpleidingId);
            });

            modelBuilder.Entity<Onderwijsuitvoering>(onderwijsuitvoering =>
            {
                onderwijsuitvoering
                    .HasIndex(o => new { o.Jaartal, o.Periode })
                    .IsUnique();
                onderwijsuitvoering
                    .HasOne(o => o.Onderwijsmodule)
                    .WithMany(o => o.Onderwijsuitvoeringen)
                    .HasForeignKey(o => o.OnderwijsmoduleId);
                onderwijsuitvoering
                    .HasOne(o => o.Docent)
                    .WithMany(d => d.Onderwijsuitvoeringen)
                    .HasForeignKey(o => o.DocentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Opleiding>(opleiding =>
            {
                opleiding
                    .HasIndex(o => o.Naam)
                    .IsUnique();
                opleiding
                    .HasOne(o => o.Vorm)
                    .WithMany()
                    .HasForeignKey(o => o.VormId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Opleidingsprofiel>(opleidingsprofiel =>
            {
                opleidingsprofiel
                    .HasIndex(o => o.Profielnaam)
                    .IsUnique();
                opleidingsprofiel
                    .HasOne(o => o.Opleiding)
                    .WithMany(o => o.Opleidingsprofielen)
                    .HasForeignKey(o => o.OpleidingId);
            });

            modelBuilder.Entity<Planning>(planning =>
            {
                planning
                    .HasIndex(p => new { p.Datum, p.Weeknummer })
                    .IsUnique();
                planning
                    .HasOne(p => p.Onderwijsuitvoering)
                    .WithMany(o => o.Planningen)
                    .HasForeignKey(p => p.OnderwijsuitvoeringId);
            });

            modelBuilder.Entity<Rol>(rol =>
            {
                rol
                    .HasIndex(r => r.Naam)
                    .IsUnique();
            });

            modelBuilder.Entity<Tentamen>(tentamen =>
            {
                tentamen
                    .HasIndex(t => t.Naam)
                    .IsUnique();
                tentamen
                    .HasOne(t => t.Vorm)
                    .WithMany()
                    .HasForeignKey(t => t.VormId)
                    .OnDelete(DeleteBehavior.NoAction);
                tentamen
                    .HasMany(t => t.Planningen)
                    .WithMany(p => p.Tentamens)
                    .UsingEntity(eb => eb.ToTable("Tentamen_Planning"));
            });

            modelBuilder.Entity<TentamenVanStudent>(tentamenVanStudent =>
            {
                tentamenVanStudent
                    .HasOne(t => t.Student)
                    .WithMany(s => s.TentamensVanStudent)
                    .HasForeignKey(t => t.StudentId);
            });

            modelBuilder.Entity<Toetsinschrijving>(toetsinschrijving =>
            {
                toetsinschrijving
                    .HasIndex(t => new { t.StudentId, t.TentamenId, t.PlanningId })
                    .IsUnique();
                toetsinschrijving
                    .HasOne(t => t.Student)
                    .WithMany(s => s.Toetsinschrijvingen)
                    .HasForeignKey(t => t.StudentId);
                toetsinschrijving
                    .HasOne(t => t.Tentamen)
                    .WithMany()
                    .HasForeignKey(t => t.TentamenId);
                toetsinschrijving
                    .HasOne(t => t.Planning)
                    .WithMany()
                    .HasForeignKey(t => t.PlanningId);
            });

            modelBuilder.Entity<Vorm>(vorm =>
            {
                vorm
                    .HasIndex(s => s.Naam)
                    .IsUnique();
            });
        }
    }
}
