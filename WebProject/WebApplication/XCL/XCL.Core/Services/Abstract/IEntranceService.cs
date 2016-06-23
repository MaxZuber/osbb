using System;
using System.Collections.Generic;
using XCL.ViewModels;

namespace XCL.Core.Services.Abstract
{
    public interface IEntranceService
    {
        List<EntranceViewModel> GetEntranceSensorValues(DateTime date, int? entranceId);
    }
}
