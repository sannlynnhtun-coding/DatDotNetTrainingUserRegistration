namespace DatDotNetTrainingUserRegistration.Dtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }

    }
}
