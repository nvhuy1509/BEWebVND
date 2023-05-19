using System;
namespace Activity.DAL.Entity.GameGuild
{
    public class StepnReport
    {
        //var param = {
        //    'tokenTotal': tokenTotal,
        //    'efficiency': efficiency,
        //    'comfort':comfort,
        //    'resilience':resilience,
        //    'luck':luck,
        //    'level':level,
        //    'date':date,
        //    'tokenRun':tokenRun,
        //    'km': km,
        //    'time':time,
        //    'steps':steps,
        //    'energy':energy,
        //    'durability':durability,
        //    }
        public string idShoes { get; set; }
        public string quality { get; set; }
        public string type { get; set; }
        public float tokenTotal { get; set; }
        public float efficiency { get; set; }
        public float comfort { get; set; }
        public float resilience { get; set; }
        public float luck { get; set; }
        public float level { get; set; }
        public DateTime date { get; set; }
        public float tokenRun { get; set; }
        public float km { get; set; }
        public string time { get; set; }
        public float steps { get; set; }
        public float energy { get; set; }
        public float durability { get; set; }
    }
}
