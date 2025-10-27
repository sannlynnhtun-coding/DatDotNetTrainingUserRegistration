namespace DatDotNetTrainingUserRegistration.Dtos
{
    public class RegisterDto
    {
        
    }

    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
    
    public class RegisterResponseDto
    {
        public bool IsSuccess { get; set; }
        public bool IsValidationError { get; set; }
        public string Message { get; set; }
    }
}
