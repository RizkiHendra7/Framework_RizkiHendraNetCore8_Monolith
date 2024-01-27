using Microsoft.EntityFrameworkCore;
using FrameWorkRHP_Mono.Core.Models.EF;


namespace FrameWorkRHP_Mono.Infrastructure.Context.Builder
{
    public class ClsMuserBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Muser>(entity =>
            {
                entity.HasKey(e => e.Intuserid).HasName("muser_pkey");

                entity.ToTable("muser");

                entity.Property(e => e.Intuserid).HasColumnName("intuserid");
                entity.Property(e => e.Bitactive)
                    .HasDefaultValue(false)
                    .HasColumnName("bitactive");
                entity.Property(e => e.Dtinserted)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dtinserted");
                entity.Property(e => e.Dtupdated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dtupdated");
                entity.Property(e => e.Txtfullname)
                    .HasMaxLength(100)
                    .HasColumnName("txtfullname");
                entity.Property(e => e.Txtinsertedby)
                    .HasMaxLength(50)
                    .HasColumnName("txtinsertedby");
                entity.Property(e => e.Txtpassword)
                    .HasMaxLength(100)
                    .HasColumnName("txtpassword");
                entity.Property(e => e.Txtupdated)
                    .HasMaxLength(50)
                    .HasColumnName("txtupdated");
                entity.Property(e => e.Txtusername)
                    .HasMaxLength(50)
                    .HasColumnName("txtusername");
            });

        }
    }
}
