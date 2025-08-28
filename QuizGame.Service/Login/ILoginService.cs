using QuizGame.Data;
using QuizGame.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface ILoginService : IDisposable
    {
        User GetUserDeatils(string email, string password);
        User GetUserDeatilByEmail(string email);
        User Update(User entity);
        User GetUserDeatilByGuid(Guid resetCode);
        User GetUserDeatilById(int resetCode);

    }
}
