namespace DatDotNetTrainingUserRegistration.Dtos
{
    public class ProfileRequestDto
    {
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
    }
    public class ProfileResponseDto
    {
        public bool IsSuccess { get; set; }
        public bool IsValidationError { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
