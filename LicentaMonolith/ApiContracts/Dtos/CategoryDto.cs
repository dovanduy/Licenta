namespace ApiContracts.Dtos
{
    public class CategoryDto
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public int? RowVersion { get; set; }
    }
}
