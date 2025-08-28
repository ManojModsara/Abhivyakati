using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class ActivityLogService : IActivityLogService
    {

        #region "Fields"
        private IRepository<ActivityLog> repoActivityLog;

        #endregion

        #region "Cosntructor"
        public ActivityLogService(IRepository<ActivityLog> _repoActivityLog)
        {
            this.repoActivityLog = _repoActivityLog;

        }
        #endregion

        #region "Actions"
        /// <summary>
        /// get list of activity log
        /// </summary>
        /// <returns></returns>
        public ICollection<ActivityLog> GetActivityLog()
        {
            return repoActivityLog.Query().AsTracking()
                //.Filter(c => c.IsActive == isActive || c.IsActive == true)
                .Get().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityLog GetActivityLog(int id)
        {
            return repoActivityLog.FindById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<int, List<ActivityLog>> GetActivityLogs(DataTableServerSide searchModel, int id = 0)
        {


            var predicate = CustomPredicate.BuildPredicate<ActivityLog>(searchModel, new Type[] { typeof(User) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<ActivityLog> results = repoActivityLog
                .Query()
                //.Filter(predicate.And(a =>  (userId == 0 ? true : a.Id != userId) && (a.RoleId != 1))) //a.IsDeleted == false &&
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(User) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<ActivityLog>> resultResponse = new KeyValuePair<int, List<ActivityLog>>(totalCount, results);

            return resultResponse;
        }
        public ActivityLog Save(ActivityLog activityLog)
        {
            activityLog.ActivityDate = DateTime.Now;
            if (activityLog.Id == 0)
            {
                repoActivityLog.Insert(activityLog);
            }
            else
            {
                repoActivityLog.Update(activityLog);
            }
            return activityLog;
        }
        #endregion

        #region "Dispose"
        public void Dispose()
        {
            if (repoActivityLog != null)
            {
                repoActivityLog.Dispose();
                repoActivityLog = null;
            }
        }
        #endregion
    }
}
