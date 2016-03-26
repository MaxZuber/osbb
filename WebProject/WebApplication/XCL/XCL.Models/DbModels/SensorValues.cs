using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCL.Models.DbModels
{
    public class SensorValues
    {
        public DateTime DateTime { get; set; }
        public int SensorId { get; set; }
        public float Value { get; set; }

        public Sensor Sensor { get; set; }
    }
}
