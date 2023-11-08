using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public partial class MusicPrnContext : DbContext
{
    public MusicPrnContext()
    {
    }

    public MusicPrnContext(DbContextOptions<MusicPrnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumDetail> AlbumDetails { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongArtist> SongArtists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-APTX486\\SQLEXPRESS;uid=sa;pwd=123456;database=MusicPRN;trustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__536C85E58AF14C03");

            entity.ToTable("Account");

            entity.Property(e => e.Username).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UserStatus)
                .HasMaxLength(20)
                .HasColumnName("User_status");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Album__5FBF259A791E9367");

            entity.ToTable("Album");

            entity.Property(e => e.AlbumId).HasColumnName("Album_ID");
            entity.Property(e => e.AlbumImg)
                .HasMaxLength(1000)
                .HasColumnName("Album_Img");
            entity.Property(e => e.AlbumName)
                .HasMaxLength(50)
                .HasColumnName("Album_Name");
            entity.Property(e => e.AlbumStatus)
                .HasMaxLength(15)
                .HasColumnName("Album_Status");
        });

        modelBuilder.Entity<AlbumDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AlbumId).HasColumnName("Album_ID");
            entity.Property(e => e.SongId).HasColumnName("Song_ID");

            entity.HasOne(d => d.Album).WithMany()
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlbumDeta__Album__412EB0B6");

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlbumDeta__Song___4222D4EF");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artist__1C44A9E77F407599");

            entity.ToTable("Artist");

            entity.Property(e => e.ArtistId).HasColumnName("Artist_ID");
            entity.Property(e => e.ArtistCountry)
                .HasMaxLength(50)
                .HasColumnName("Artist_Country");
            entity.Property(e => e.ArtistImg)
                .HasMaxLength(1000)
                .HasColumnName("Artist_Img");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(50)
                .HasColumnName("Artist_Name");
            entity.Property(e => e.ArtistStatus)
                .HasMaxLength(15)
                .HasColumnName("Artist_Status");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Favorite");

            entity.Property(e => e.SongId).HasColumnName("Song_ID");
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__Song_I__44FF419A");

            entity.HasOne(d => d.UsernameNavigation).WithMany()
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__Userna__440B1D61");
        });
        modelBuilder.Entity<Favorite>().HasKey(f => f.Username);
        modelBuilder.Entity<Favorite>().HasKey(f => f.SongId);
        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Song__3214EC27472295C9");

            entity.ToTable("Song");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FilePath)
                .HasMaxLength(1000)
                .HasColumnName("filePath");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(1000)
                .HasColumnName("imgUrl");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<SongArtist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Song_Artist");

            entity.Property(e => e.ArtistId).HasColumnName("Artist_ID");
            entity.Property(e => e.SongId).HasColumnName("Song_ID");

            entity.HasOne(d => d.Artist).WithMany()
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Song_Arti__Artis__3C69FB99");

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Song_Arti__Song___3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
