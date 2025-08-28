using QuizGame.Core;
using QuizGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface IEventService
    {
        List<Event> GetEvent();
        List<Event> GetUpcomingEvents(int count);

        Event GetEventById(int Id);
        bool DeleteEvent(int Id);
        Event SaveEvent(Event events);
        KeyValuePair<int, List<Event>> GetEvents(DataTableServerSide searchModel);

    }
}
