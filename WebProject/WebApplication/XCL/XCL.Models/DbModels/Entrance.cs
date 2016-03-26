using System;
using System.Collections.Generic;
using System.Linq;
using XCL.Common.Enum;

namespace XCL.Models.DbModels
{
    public class Entrance
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int EntranceNumber { get; set; }
        public BuildingInfo BuildingInfo { get; set; }
        public ICollection<Flat> Flats { get; set; }
        public ICollection<Sensor> Sensors { get; set; }

        public Sensor GetSensorsOfType(SensorInstallationPositionType type)
        {
            return Sensors.Single(x => x.InstallationPosition == type);
        }
    }
}
