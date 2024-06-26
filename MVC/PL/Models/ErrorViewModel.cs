namespace PL.Models
{
    public class ErrorViewModel
    {
        public string? Id { get; set; }

        public bool ShowId => !string.IsNullOrEmpty(Id);
    }
}
