using System;
using CardExploration.DatabaseContext;
using CardExploration.models;

namespace CardExploration.Manager
{
    public class TimeRecordManager
    {
        public static void RecordTime(string process)
        {
            using (var dbContext = new RecordDbContext())
            {
                dbContext.TimeRecords.Add(
                        new timeRecord(){Time = DateTime.Now.ToString(), Process = process}
                    );
                dbContext.SaveChanges();
            }
        }
    }
}