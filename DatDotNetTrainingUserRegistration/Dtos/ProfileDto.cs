namespace DatDotNetTrainingUserRegistration.Dtos
{
    public class ProfileDto
    {
        public class ProfileRequestDto
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }
        public class ProfileResponseDto
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
        }
    }
}
