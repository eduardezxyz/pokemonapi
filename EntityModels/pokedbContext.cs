using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demoapi.EntityModels
{
    public partial class pokedbContext : DbContext
    {
        public pokedbContext()
        {
        }

        public pokedbContext(DbContextOptions<pokedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MoveVsType> MoveVsTypes { get; set; } = null!;
        public virtual DbSet<MyPokeMove> MyPokeMoves { get; set; } = null!;
        public virtual DbSet<MyPokemon> MyPokemons { get; set; } = null!;
        public virtual DbSet<PokeMove> PokeMoves { get; set; } = null!;
        public virtual DbSet<PokeParty> PokeParties { get; set; } = null!;
        public virtual DbSet<PokeTrainer> PokeTrainers { get; set; } = null!;
        public virtual DbSet<PokeType> PokeTypes { get; set; } = null!;
        public virtual DbSet<Pokedetail> Pokedetails { get; set; } = null!;
        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;
        public virtual DbSet<Trainerpokevw> Trainerpokevws { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=pokedb;Username=eduard;Password=Cowboys2325_");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveVsType>(entity =>
            {
                entity.HasKey(e => new { e.MoveId, e.TypeId })
                    .HasName("move_vs_type_pkey");

                entity.ToTable("move_vs_type");

                entity.Property(e => e.MoveId).HasColumnName("move_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Ratio).HasColumnName("ratio");

                entity.HasOne(d => d.Move)
                    .WithMany(p => p.MoveVsTypes)
                    .HasForeignKey(d => d.MoveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("move_vs_type_move_id_fkey");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.MoveVsTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("move_vs_type_type_id_fkey");
            });

            modelBuilder.Entity<MyPokeMove>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("my_poke_moves");

                entity.Property(e => e.MoveId).HasColumnName("move_id");

                entity.Property(e => e.MyPokeId).HasColumnName("my_poke_id");

                entity.HasOne(d => d.Move)
                    .WithMany()
                    .HasForeignKey(d => d.MoveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("my_poke_moves_move_id_fkey");

                entity.HasOne(d => d.MyPoke)
                    .WithMany()
                    .HasForeignKey(d => d.MyPokeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("my_poke_moves_my_poke_id_fkey");
            });

            modelBuilder.Entity<MyPokemon>(entity =>
            {
                entity.ToTable("my_pokemon");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hp).HasColumnName("hp");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(20)
                    .HasColumnName("nickname");

                entity.Property(e => e.PokeLvl).HasColumnName("poke_lvl");

                entity.Property(e => e.PokedexEntry).HasColumnName("pokedex_entry");

                entity.HasOne(d => d.PokedexEntryNavigation)
                    .WithMany(p => p.MyPokemons)
                    .HasForeignKey(d => d.PokedexEntry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("my_pokemon_pokedex_entry_fkey");
            });

            modelBuilder.Entity<PokeMove>(entity =>
            {
                entity.ToTable("poke_move");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MoveName)
                    .HasMaxLength(25)
                    .HasColumnName("move_name");
            });

            modelBuilder.Entity<PokeParty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("poke_party");

                entity.HasIndex(e => e.MyPokeId, "const_my_poke_id")
                    .IsUnique();

                entity.Property(e => e.MyPokeId).HasColumnName("my_poke_id");

                entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

                entity.HasOne(d => d.MyPoke)
                    .WithOne()
                    .HasForeignKey<PokeParty>(d => d.MyPokeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("poke_party_my_poke_id_fkey");

                entity.HasOne(d => d.Trainer)
                    .WithMany()
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("poke_party_trainer_id_fkey");
            });

            modelBuilder.Entity<PokeTrainer>(entity =>
            {
                entity.ToTable("poke_trainer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TrainerName)
                    .HasMaxLength(20)
                    .HasColumnName("trainer_name");
            });

            modelBuilder.Entity<PokeType>(entity =>
            {
                entity.ToTable("poke_type");

                entity.HasIndex(e => e.Id, "const_id")
                    .IsUnique();

                entity.HasIndex(e => e.PokeTypeName, "const_poke_type_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PokeTypeName)
                    .HasMaxLength(12)
                    .HasColumnName("poke_type_name");
            });

            modelBuilder.Entity<Pokedetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("pokedetails");

                entity.Property(e => e.HeightMeters)
                    .HasPrecision(4, 1)
                    .HasColumnName("height_meters");

                entity.Property(e => e.MoveList).HasColumnName("move_list");

                entity.Property(e => e.PokeName)
                    .HasMaxLength(15)
                    .HasColumnName("poke_name");

                entity.Property(e => e.PokedexEntry).HasColumnName("pokedex_entry");

                entity.Property(e => e.Type1)
                    .HasMaxLength(12)
                    .HasColumnName("type1");

                entity.Property(e => e.Type2)
                    .HasMaxLength(12)
                    .HasColumnName("type2");

                entity.Property(e => e.WeightKg)
                    .HasPrecision(4, 1)
                    .HasColumnName("weight_kg");
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.PokedexEntry)
                    .HasName("pokemon_pkey");

                entity.ToTable("pokemon");

                entity.HasIndex(e => e.PokeName, "const_poke_name")
                    .IsUnique();

                entity.HasIndex(e => e.PokedexEntry, "ux_poke_pokedex")
                    .IsUnique();

                entity.Property(e => e.PokedexEntry)
                    .ValueGeneratedNever()
                    .HasColumnName("pokedex_entry");

                entity.Property(e => e.HeightMeters)
                    .HasPrecision(4, 1)
                    .HasColumnName("height_meters");

                entity.Property(e => e.PokeName)
                    .HasMaxLength(15)
                    .HasColumnName("poke_name");

                entity.Property(e => e.PokeType1Id).HasColumnName("poke_type1_id");

                entity.Property(e => e.PokeType2Id).HasColumnName("poke_type2_id");

                entity.Property(e => e.WeightKg)
                    .HasPrecision(4, 1)
                    .HasColumnName("weight_kg");

                entity.HasOne(d => d.PokeType1)
                    .WithMany(p => p.PokemonPokeType1s)
                    .HasForeignKey(d => d.PokeType1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pokemon_poke_type1_id_fkey");

                entity.HasOne(d => d.PokeType2)
                    .WithMany(p => p.PokemonPokeType2s)
                    .HasForeignKey(d => d.PokeType2Id)
                    .HasConstraintName("pokemon_poke_type2_id_fkey");

                entity.HasMany(d => d.Moves)
                    .WithMany(p => p.PokedexEntries)
                    .UsingEntity<Dictionary<string, object>>(
                        "MoveXref",
                        l => l.HasOne<PokeMove>().WithMany().HasForeignKey("MoveId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("move_xref_move_id_fkey"),
                        r => r.HasOne<Pokemon>().WithMany().HasForeignKey("PokedexEntry").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("move_xref_pokedex_entry_fkey"),
                        j =>
                        {
                            j.HasKey("PokedexEntry", "MoveId").HasName("move_xref_pkey");

                            j.ToTable("move_xref");

                            j.IndexerProperty<int>("PokedexEntry").HasColumnName("pokedex_entry");

                            j.IndexerProperty<int>("MoveId").HasColumnName("move_id");
                        });
            });

            modelBuilder.Entity<Trainerpokevw>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("trainerpokevw");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nickname).HasColumnName("nickname");

                entity.Property(e => e.TrainerName)
                    .HasMaxLength(20)
                    .HasColumnName("trainer_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
