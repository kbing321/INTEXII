namespace INTEXII.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}