namespace CodeJewelsModels
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name="codeJewel")]
    public class CodeJewelPostModel
    {
        [DataMember(Name="category")]
        public string Category { get; set; }

        [DataMember(Name = "authorEmail")]
        public string AuthorEmail { get; set; }

        [DataMember(Name = "sourceCode")]
        public string SourceCode { get; set; }
    }
}
