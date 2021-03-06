using System;

namespace DisputenPWA.Domain.Helpers
{
    public static class EventRange
    {
        public static DateTime LowestEndDate => DateTime.Now.AddDays(-31);
        public static DateTime HighestStartDate => DateTime.Now.AddDays(62);
    }
}
