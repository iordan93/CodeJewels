namespace CodeJewelsModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CodeJewel
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public string AuthorEmail { get; set; }

        public int Rating { get; set; }

        [Column(TypeName = "ntext")]
        public string SourceCode { get; set; }
    }
}
