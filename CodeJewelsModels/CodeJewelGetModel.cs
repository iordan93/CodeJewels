namespace CodeJewelsModels
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name="codeJewel")]
    public class CodeJewelGetModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name="category")]
        public string Category { get; set; }

        [DataMember(Name = "authorEmail")]
        public string AuthorEmail { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "sourceCode")]
        public string SourceCode { get; set; }
    }
}
