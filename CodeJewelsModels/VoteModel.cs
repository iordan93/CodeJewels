namespace CodeJewelsModels
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name="vote")]
    public class VoteModel
    {
        [DataMember(Name="codeJewel")]
        public CodeJewelGetModel CodeJewel { get; set; }

        [DataMember(Name="value")]
        public int Value { get; set; }
    }
}
