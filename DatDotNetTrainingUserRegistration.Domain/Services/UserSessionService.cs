using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;

namespace DatDotNetTrainingUserRegistration.Domain.Services;

public class UserSessionService
{
    private readonly AppDbContext _db;

    public UserSessionService()
    {
    }

    public UserSessionService(AppDbContext db)
    {
        _db = db;
    }

    public bool IsSessionValid(Guid userId, Guid sessionId)
    {
        var session = _db.TblLogins
            .FirstOrDefault(x => x.UserId == userId && x.SessionId == sessionId);

        if (session == null)
            return false;

        if (session.SessionTime < DateTime.UtcNow)
            return false;

        return true;
    }
}
