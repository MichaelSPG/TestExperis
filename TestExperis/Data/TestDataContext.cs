namespace TestExperis.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestDataContext : DbContext
    {
        public TestDataContext()
            : base("name=TestDataContextStrConn")
        {
        }

        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Interview> Interview { get; set; }
        public virtual DbSet<InterviewMode> InterviewMode { get; set; }
        public virtual DbSet<RecruiterUser> RecruiterUser { get; set; }
        public virtual DbSet<Technology> Technology { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_street)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_suite)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_city)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_zipcode)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_geo_lat)
                .HasPrecision(8, 4);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.address_geo_lng)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.company_name)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.company_catchPhrase)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.company_bs)
                .IsUnicode(false);

            modelBuilder.Entity<Applicant>()
                .HasMany(e => e.Interview)
                .WithRequired(e => e.Applicant)
                .HasForeignKey(e => e.Interview_Applicant_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Category_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Interview)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.Interview_Category_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Technology)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.Technology_Category_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InterviewMode>()
                .Property(e => e.InterviewMode_Name)
                .IsUnicode(false);

            modelBuilder.Entity<InterviewMode>()
                .HasMany(e => e.Interview)
                .WithRequired(e => e.InterviewMode)
                .HasForeignKey(e => e.Interview_InterMode_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RecruiterUser>()
                .Property(e => e.RecruiterUser_Names)
                .IsUnicode(false);

            modelBuilder.Entity<RecruiterUser>()
                .Property(e => e.RecruiterUser_Surnames)
                .IsUnicode(false);

            modelBuilder.Entity<RecruiterUser>()
                .Property(e => e.RecruiterUser_User)
                .IsUnicode(false);

            modelBuilder.Entity<RecruiterUser>()
                .Property(e => e.RecruiterUser_Password)
                .IsUnicode(false);

            modelBuilder.Entity<RecruiterUser>()
                .HasMany(e => e.Interview)
                .WithRequired(e => e.RecruiterUser)
                .HasForeignKey(e => e.Interview_RecruiterUser_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Technology>()
                .Property(e => e.Technology_Name)
                .IsUnicode(false);
        }
    }
}
