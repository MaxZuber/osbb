using System.Collections.Generic;

namespace XCL.Models.DbModels
{
    public class Flat
    {
        public int Id { get; set; }
        public int EntranceId { get; set; }
        public int FlatNumber { get; set; }
        public float Square { get; set; }
        public int RegisteredPeopleCount { get; set; }

        public Entrance Entrance { get; set; }
        public ICollection<Account> Accounts { get; set; } 
    }
}
