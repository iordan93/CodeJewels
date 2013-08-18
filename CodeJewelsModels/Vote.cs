namespace CodeJewelsModels
{
    using System;

    public class Vote
    {
        public int Id { get; set; }

        public CodeJewel CodeJewel { get; set; }

        public int Value { get; set; }
    }
}
