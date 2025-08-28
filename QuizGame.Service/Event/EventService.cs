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
    public class EventService : IEventService
    {
        #region "Fields"
        private IRepository<Event> repoEvent;
        #endregion

        #region "Cosntructor"
        public EventService(IRepository<Event> _repoEvent)
        {
            this.repoEvent = _repoEvent;

        }
        #endregion

        public List<Event> GetEvent()
        {
            return repoEvent.Query().Get().ToList();
        }

        public List<Event> GetUpcomingEvents(int count)
        {
            DateTime today = DateTime.Now.Date;
            return repoEvent.Query()
                .Filter(x => x.EventDate > today).GetQuerable()
                .OrderByDescending(x => x.AddedDate)
                .Take(count)
                .ToList();
        }
        public Event GetEventById(int Id)
        {
            return repoEvent.Query().Get().Where(x => x.Id == Id).FirstOrDefault();
        }
        public bool DeleteEvent(int Id)
        {
            repoEvent.Delete(Id);
            return true;
        }

        public Event SaveEvent(Event events)
        {
            if (events.Id == 0)
            {
                events.AddedDate = DateTime.Now;
                repoEvent.Insert(events);
            }
            else
            {
                events.UpdatedDate = DateTime.Now;
                repoEvent.Update(events);
            }
            return events;
        }
        public KeyValuePair<int, List<Event>> GetEvents(DataTableServerSide searchModel)
        {
            var predicate = CustomPredicate.BuildPredicate<Event>(searchModel, new Type[] { typeof(Event), typeof(User), typeof(UserProfile) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<Event> results = repoEvent
                .Query()
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Event),typeof(User), typeof(UserProfile) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<Event>> resultResponse = new KeyValuePair<int, List<Event>>(totalCount, results);

            return resultResponse;
        }
    }
}
