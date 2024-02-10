using FrameWorkRHP_Mono.Core.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace FrameWorkRHP_Mono.Infrastructure.Context.Builder
{
    public class ClsMRoleXMenuBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MRoleXMenu>(entity =>
            {
                entity.HasKey(e => e.Intmrolexmenuid).HasName("mrolexmenu_pkey");

                entity.ToTable("mrolexmenu");

                entity.Property(e => e.Intmrolexmenuid).HasColumnName("intmrolexmenuid");
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
                entity.Property(e => e.Intmenuid).HasColumnName("intmenuid");
                entity.Property(e => e.Introleid).HasColumnName("introleid");
                entity.Property(e => e.Txtinsertedby)
                    .HasMaxLength(50)
                    .HasColumnName("txtinsertedby");
                entity.Property(e => e.Txtupdated)
                    .HasMaxLength(50)
                    .HasColumnName("txtupdated");

                entity.HasOne(d => d.Intmenu).WithMany(p => p.Mrolexmenus)
                    .HasForeignKey(d => d.Intmenuid)
                    .HasConstraintName("mrolexmenu_intmenuid_fkey");

                entity.HasOne(d => d.Introle).WithMany(p => p.Mrolexmenus)
                    .HasForeignKey(d => d.Introleid)
                    .HasConstraintName("mrolexmenu_introleid_fkey");
            });
        }
    }
}
