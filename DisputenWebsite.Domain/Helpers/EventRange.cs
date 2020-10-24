using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Helpers
{
    public static class EventRange
    {
        public static DateTime LowestEndDate => DateTime.Now.AddDays(-28);
        public static DateTime HighestStartDate => DateTime.Now.AddDays(56);
    }
}
