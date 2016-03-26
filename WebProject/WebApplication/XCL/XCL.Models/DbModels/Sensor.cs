using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCL.Common.Enum;

namespace XCL.Models.DbModels
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Dimension { get; set; }
        public string Description { get; set; }
        public SensorInstallationPositionType InstallationPosition { get; set; }

        public int EntranceId { get; set; }
        public Entrance Entrance { get; set; }

        public ICollection<SensorValues> SensorValues { get; set; } 
    }
}
