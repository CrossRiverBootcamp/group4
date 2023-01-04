namespace Account.DTO
{
    public class OperationDto
    {
        public bool DebitOrCredit { get; set; }
        public int OtherSide { get; set; }
        public float Amount { get; set; }
        public float Balance { get; set; }
        public DateTime OperationTime { get; set; }

    }
}
