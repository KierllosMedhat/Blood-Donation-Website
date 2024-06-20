namespace Blood_Donation_Website.Models
{
    public class ErrorViewModel
    {
        public string? Id { get; set; }

        public bool ShowId => !string.IsNullOrEmpty(Id);
    }
}
