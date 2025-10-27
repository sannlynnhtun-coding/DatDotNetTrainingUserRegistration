using System;
using System.Collections.Generic;

namespace DatDotNetTrainingUserRegistration.Database.AppDbContextModels;

public partial class TblLogin
{
    public Guid LoginId { get; set; }

    public Guid UserId { get; set; }

    public Guid SessionId { get; set; }

    public DateTime SessionTime { get; set; }
}
