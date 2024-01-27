using Microsoft.EntityFrameworkCore;
using FrameWorkRHP_Mono.Core.Models.EF;


namespace FrameWorkRHP_Mono.Infrastructure.Context.Builder
{
    public class ClsMuserroleBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Muserrole>(entity =>
            {
                entity.HasKey(e => e.Intuserroleid).HasName("muserrole_pkey");

                entity.ToTable("muserrole");

                entity.Property(e => e.Intuserroleid).HasColumnName("intuserroleid");
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
                entity.Property(e => e.Introleid).HasColumnName("introleid");
                entity.Property(e => e.Intuserid).HasColumnName("intuserid");
                entity.Property(e => e.Txtinsertedby)
                    .HasMaxLength(50)
                    .HasColumnName("txtinsertedby");
                entity.Property(e => e.Txtupdated)
                    .HasMaxLength(50)
                    .HasColumnName("txtupdated");

                entity.HasOne(d => d.Introle).WithMany(p => p.Muserroles)
                    .HasForeignKey(d => d.Introleid)
                    .HasConstraintName("muserrole_rolefk");

                entity.HasOne(d => d.Intuser).WithMany(p => p.Muserroles)
                    .HasForeignKey(d => d.Intuserid)
                    .HasConstraintName("muserrole_fk");
            });
        }
    }
}
