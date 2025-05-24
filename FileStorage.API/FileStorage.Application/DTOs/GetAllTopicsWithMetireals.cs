namespace FileStorage.Application.DTOs
{
    public class GetAllTopicsWithMetireals
    {
        public int Id { get; set; } // topic Id
        public string Name { get; set; } // topic name
        public List<MetirealDTO> Items { get; set; } // metireal collection
    }
}
