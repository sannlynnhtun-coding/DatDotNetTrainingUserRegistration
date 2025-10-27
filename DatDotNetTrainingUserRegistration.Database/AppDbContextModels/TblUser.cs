using System;
using System.Collections.Generic;

namespace DatDotNetTrainingUserRegistration.Database.AppDbContextModels;

public partial class TblUser
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
