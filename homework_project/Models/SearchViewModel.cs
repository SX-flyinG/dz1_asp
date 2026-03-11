namespace homework_project.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<Product> Results { get; set; } = new List<Product>();
    }
}
