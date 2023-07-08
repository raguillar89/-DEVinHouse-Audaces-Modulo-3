using Escola.Models;
using Microsoft.EntityFrameworkCore;

namespace Escola.Context
{
    public class EscolaDbContext : DbContext
    {
        public EscolaDbContext() { }

        public EscolaDbContext(DbContextOptions<EscolaDbContext> options) : base(options) { }
        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<Turma> Turmas { get; set; }
        public virtual DbSet<Boletim> Boletins { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<NotasMateria> NotasMaterias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>().ToTable("AlunoTB");
            modelBuilder.Entity<Aluno>().HasKey(x => x.Id).HasName("PK_ALUNO_ID");
            modelBuilder.Entity<Aluno>().Property(x => x.Id).HasColumnName("PK_ID").HasColumnType("INT");
            modelBuilder.Entity<Aluno>().Property(x => x.Nome).IsRequired().HasColumnName("NOME").HasColumnType("VARCHAR").HasMaxLength(150);
            modelBuilder.Entity<Aluno>().Property(x => x.Sobrenome).IsRequired().HasColumnName("SOBRENOME").HasColumnType("VARCHAR").HasMaxLength(150);
            modelBuilder.Entity<Aluno>().Property(x => x.Idade).IsRequired().HasColumnName("IDADE").HasColumnType("INT");
            modelBuilder.Entity<Aluno>().Property(x => x.Genero).IsRequired().HasColumnName("GENERO").HasColumnType("VARCHAR").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.Telefone).IsRequired().HasColumnName("TELEFONE").HasColumnType("VARCHAR").HasMaxLength(30);
            modelBuilder.Entity<Aluno>().Property(x => x.DataNascimento).IsRequired().HasColumnName("DATA_NASCIMENTO").HasColumnType("DATETIME2");

            modelBuilder.Entity<Turma>().ToTable("TurmaTB");
            modelBuilder.Entity<Turma>().HasKey(x => x.Id).HasName("PK_TURMA_ID");
            modelBuilder.Entity<Turma>().Property(x => x.Id).HasColumnName("PK_ID").HasColumnType("INT");
            modelBuilder.Entity<Turma>().Property(x => x.Curso).IsRequired().HasColumnName("CURSO").HasColumnType("VARCHAR").HasMaxLength(50);
            modelBuilder.Entity<Turma>().Property(x => x.Nome).IsRequired().HasColumnName("NOME").HasColumnType("VARCHAR").HasMaxLength(50);
            modelBuilder.Entity<Turma>().HasIndex(x => x.Nome).IsUnique();

            modelBuilder.Entity<Boletim>().ToTable("BoletimTB");
            modelBuilder.Entity<Boletim>().HasKey(x => x.Id).HasName("PK_BOLETIM_ID");
            modelBuilder.Entity<Boletim>().Property(x => x.Id).HasColumnName("PK_ID").HasColumnType("INT");
            modelBuilder.Entity<Boletim>().Property(x => x.OrderDate).IsRequired().HasColumnName("ORDER_DATA").HasColumnType("DATETIME2");
            modelBuilder.Entity<Boletim>().Property(x => x.IdAluno).IsRequired().HasColumnName("FK_ALUNO_ID").HasColumnType("INT");
            modelBuilder.Entity<Boletim>().HasOne(typeof(Aluno)).WithMany().HasForeignKey("PK_ID");            

            modelBuilder.Entity<Materia>().ToTable("MateriaTB");
            modelBuilder.Entity<Materia>().HasKey(x => x.Id).HasName("PK_MATERIA_ID");
            modelBuilder.Entity<Materia>().Property(x => x.Id).HasColumnName("PK_ID").HasColumnType("INT");
            modelBuilder.Entity<Materia>().Property(x => x.Nome).IsRequired().HasColumnName("NOME").HasColumnType("VARCHAR").HasMaxLength(50);

            modelBuilder.Entity<NotasMateria>().ToTable("NotasMateriaTB");
            modelBuilder.Entity<NotasMateria>().HasKey(x => x.Id).HasName("PK_NOTAS_MATERIA_ID");
            modelBuilder.Entity<NotasMateria>().Property(x => x.Id).HasColumnName("PK_ID").HasColumnType("INT");
            modelBuilder.Entity<NotasMateria>().Property(x => x.Nota).IsRequired().HasColumnName("NOTA").HasColumnType("INT");
            modelBuilder.Entity<NotasMateria>().Property(x => x.IdMateria).IsRequired().HasColumnName("FK_MATERIA_ID").HasColumnType("INT");
            modelBuilder.Entity<NotasMateria>().Property(x => x.IdBoletim).IsRequired().HasColumnName("FK_BOLETIM_ID").HasColumnType("INT");
            modelBuilder.Entity<NotasMateria>().HasOne(typeof(Materia)).WithMany().HasForeignKey("FK_MATERIA_ID");
            modelBuilder.Entity<NotasMateria>().HasOne(typeof(Boletim)).WithMany().HasForeignKey("FK_BOLETIM_ID");
        }
    }
}
