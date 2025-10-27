namespace DatDotNetTrainingUserRegistration.Dtos
{
    public class LoginDto
    {
        public class LoginRequestDto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponseDto
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
            
        }
    }
}
