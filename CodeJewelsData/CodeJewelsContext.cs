namespace CodeJewelsData
{
    using System;
    using System.Data.Entity;
    using CodeJewelsModels;

    public class CodeJewelsContext : DbContext
    {
        public CodeJewelsContext()
            : base("CodeJewelsContext")
        {
        }

        public DbSet<CodeJewel> CodeJewels { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
