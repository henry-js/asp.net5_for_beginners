namespace EFCore_CodeFirst.DTO.PlayerInstruments
{
    public class CreatePlayerInstrumentRequest
    {
        public int InstrumentTypeId { get; set; }
        public string ModelName { get; set; }
        public string Level { get; set; }
    }
}