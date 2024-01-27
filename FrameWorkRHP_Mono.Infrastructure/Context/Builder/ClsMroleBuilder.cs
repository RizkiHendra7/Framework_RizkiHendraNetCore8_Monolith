using FrameWorkRHP_Mono.Core.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace FrameWorkRHP_Mono.Infrastructure.Context.Builder
{
    public class ClsMroleBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mrole>(entity =>
            {
                entity.HasKey(e => e.Introleid).HasName("mrole_pkey");

                entity.ToTable("mrole");

                entity.Property(e => e.Introleid).HasColumnName("introleid");
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
                entity.Property(e => e.Txtinsertedby)
                    .HasMaxLength(50)
                    .HasColumnName("txtinsertedby");
                entity.Property(e => e.Txtrolename)
                    .HasMaxLength(50)
                    .HasColumnName("txtrolename");
                entity.Property(e => e.Txtupdated)
                    .HasMaxLength(50)
                    .HasColumnName("txtupdated");
            });

        }
    }
}
