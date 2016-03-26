using System.Collections.Generic;
using System.Linq;
using XCL.Common.Enum;

namespace XCL.Models.DbModels
{
    public class BuildingInfo
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }

        public ICollection<Entrance> Entrances { get; set; }
    }
}
