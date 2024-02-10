using FrameWorkRHP_Mono.Core.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace FrameWorkRHP_Mono.Infrastructure.Context.Builder
{
    public  class ClsMmenuBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MMenu>(entity =>
            {
                entity.HasKey(e => e.Intmenuid).HasName("mmenu_pkey");

                entity.ToTable("mmenu");

                entity.Property(e => e.Intmenuid).HasColumnName("intmenuid");
                entity.Property(e => e.Bitactive)
                    .HasDefaultValue(true)
                    .HasColumnName("bitactive");
                entity.Property(e => e.Dtinserted)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dtinserted");
                entity.Property(e => e.Dtupdated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dtupdated");
                entity.Property(e => e.Intparentmenuid).HasColumnName("intparentmenuid");
                entity.Property(e => e.Txtinsertedby)
                    .HasMaxLength(50)
                    .HasColumnName("txtinsertedby");
                entity.Property(e => e.Txtmenudisplay)
                    .HasMaxLength(100)
                    .HasColumnName("txtmenudisplay");
                entity.Property(e => e.Txtmenuicon)
                    .HasMaxLength(100)
                    .HasColumnName("txtmenuicon");
                entity.Property(e => e.Txtmenuname)
                    .HasMaxLength(100)
                    .HasColumnName("txtmenuname");
                entity.Property(e => e.Txtupdated)
                    .HasMaxLength(50)
                    .HasColumnName("txtupdated");
                entity.Property(e => e.Txturl)
                    .HasMaxLength(100)
                    .HasColumnName("txturl");
            });

        }
    }
}
