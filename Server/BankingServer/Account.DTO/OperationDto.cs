namespace Account.DTO
{
    public class OperationDto
    {
        public bool DebitOrCredit { get; set; }
        public int OtherSide { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public DateTime OperationTime { get; set; }

    }
}
