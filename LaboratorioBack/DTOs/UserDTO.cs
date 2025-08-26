using System.ComponentModel.DataAnnotations;

namespace LaboratorioBack.DTOs
{
    public class UserDTO
    {
        public required string Id { get; set; }
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class CredentialsUserDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }

    public class AuthenticationResponseDTO { 
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class EditClaimDTO
    {
        public required string Email { get; set; }
    }
}