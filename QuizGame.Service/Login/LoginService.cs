using QuizGame.Data;
using QuizGame.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class LoginService : ILoginService
    {
        #region "Fields"
        private IRepository<User> repoUser;
        private IRepository<Role> repoRole;
        private IRepository<Menu> repoMenu;
        #endregion

        #region "Cosntructor"
        public LoginService(IRepository<User> _repoUser, IRepository<Role> _repoAdminRole, IRepository<Menu> _repoMenu)
        {
            this.repoUser = _repoUser;
            this.repoRole = _repoAdminRole;
            this.repoMenu = _repoMenu;

        }
        #endregion

        public User GetUserDeatils(string email, string password)
        {
            return repoUser.Query().Filter(x => x.Username.ToLower() == email.ToLower() && String.Compare(x.Password, password, false) == 0 && x.IsActive == true).Get().FirstOrDefault();
        }

        public User GetUserDeatilByEmail(string email)
        {
            return repoUser.Query().Filter(x => x.Username.ToLower() == email.ToLower() || x.Username == email && x.IsActive == true).Get().FirstOrDefault();
        }

        public User GetUserDeatilByGuid(Guid resetCode)
        {
            return repoUser.Query().Filter(x => x.Username.ToLower() == resetCode.ToString()).Get().FirstOrDefault();
        }
        public User GetUserDeatilById(int userId)
        {
            return repoUser.FindById(userId);
        }

        public User Update(User entity)
        {
            repoUser.Update(entity);
            return repoUser.Query().Filter(x => x.Id == entity.Id).Get().FirstOrDefault();
        }

        #region "Dispose"
        public void Dispose()
        {
            if (repoUser != null)
            {
                repoUser.Dispose();
                repoUser = null;
            }

            if (repoRole != null)
            {
                repoRole.Dispose();
                repoRole = null;
            }

            if (repoMenu != null)
            {
                repoMenu.Dispose();
                repoMenu = null;
            }
        }
        #endregion

    }
}
