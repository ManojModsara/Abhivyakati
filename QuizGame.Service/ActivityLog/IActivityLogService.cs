using QuizGame.Core;
using QuizGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface IActivityLogService
    {

        ICollection<ActivityLog> GetActivityLog();
        ActivityLog GetActivityLog(int id);
        KeyValuePair<int, List<ActivityLog>> GetActivityLogs(DataTableServerSide searchModel, int ActivityLogId = 0);
        ActivityLog Save(ActivityLog activityLog);

    }
}
