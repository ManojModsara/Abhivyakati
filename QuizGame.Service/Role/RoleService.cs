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
    public class RoleService : IRoleService
    {
        #region "Fields"
        private IRepository<User> repoAdminUser;
         private IRepository<Role> repoAdminRole;
         private IRepository<Menu> repoMenu;
         private IRepository<MapMenuToRole> repoMapMenuToRole;
        #endregion

        #region "Cosntructor"
        public RoleService(IRepository<User> _repoUser, IRepository<Role> _repoAdminRole, IRepository<Menu> _repoMenu, IRepository<MapMenuToRole> _repoMapMenuToRole)
        {
            this.repoAdminUser = _repoUser;
            this.repoAdminRole = _repoAdminRole;
            this.repoMenu = _repoMenu;
            this.repoMapMenuToRole = _repoMapMenuToRole;
        }
        #endregion

        #region "Action"
        public List<MapMenuToRole> GetRolePermission(int roleid)
        {
            return Cache.Get<List<MapMenuToRole>>(CacheKey.RolePermission + roleid) ??
                Cache.AddOrReplace<List<MapMenuToRole>>(CacheKey.RolePermission + roleid, repoMapMenuToRole.Query().Filter(a => a.RoleId == roleid).Get().ToList(),
                DateTimeOffset.Now.AddDays(30));
        }
        public List<Menu> GetMenusByRoleId(int roleid)
        {
            return Cache.Get<List<Menu>>(CacheKey.AdminMenu + roleid) ??
                Cache.AddOrReplace<List<Menu>>(CacheKey.AdminMenu + roleid, repoMenu.Query().Filter(m => m.MapMenuToRoles.Any(a => a.RoleId == roleid)).Get().ToList(),
                DateTimeOffset.Now.AddDays(30));
        }
        public List<Menu> GetMenu()
        {
            return repoMenu.Query().Get().Where(x => x.IsActive == true).ToList();
        }
        public bool DeleteRolePermission(int id)
        {


            var allowedmenus = repoMapMenuToRole.Query().Filter(m => m.RoleId == id).Get().ToList();

            foreach (var menuallowed in allowedmenus)
            {
                repoMapMenuToRole.Delete(menuallowed);
            }
            return true;

        }
        public bool AddRolePermission(List<MapMenuToRole> mapMenuToRoles)
        {

            foreach (var menuallowed in mapMenuToRoles)
            {
                repoMapMenuToRole.Insert(menuallowed);
            }
            return true;


        }
        public Role Save(Role adminRole)
        {
            if (adminRole.Id == 0)
            {
                repoAdminRole.Insert(adminRole);
            }
            else
            {
                repoAdminRole.Update(adminRole);
            }
            return adminRole;
        }
        public Role GetAdminRole(int id)
        {
            return repoAdminRole.FindById(id);
        }
        public KeyValuePair<int, List<Role>> GetAdminRoles(Core.DataTableServerSide searchModel)
        {
            var predicate = Core.CustomPredicate.BuildPredicate<Role>(searchModel, new Type[] { typeof(Role) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<Role> results = repoAdminRole
                .Query()
               .Filter(predicate)
                //.Filter(predicate.And(a => a.IsActive && a.RoleName != Enum.GetName(typeof(RoleType),1)))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Role) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<Role>> resultResponse = new KeyValuePair<int, List<Role>>(totalCount, results);

            return resultResponse;
        }

        public bool CheckCurrentMenu(string url, int roleID)
        {
            ////var pages = repoMenuAccess.Query().Filter(M => M.RoleId == roleID).Get().Select(P => P.FrontMenu.PageName.ToLower() + (!String.IsNullOrEmpty(P.FrontMenu.ChildPages) ? ("," + P.FrontMenu.ChildPages) : "")).ToList();
            ////List<string> pageList = new List<string>();
            ////if (pages.Count > 0)
            ////{
            ////    foreach (var page in pages)
            ////    {
            ////        string[] str = page.Trim().Split(',');
            ////        if (str.Length > 1)
            ////        {
            ////            foreach (string value in str)
            ////            {
            ////                pageList.Add(value);
            ////            }
            ////        }
            ////        else
            ////        {
            ////            pageList.Add(page);
            ////        }
            ////    }
            ////}
            ////return pageList.Any(P => url.ToLower().EndsWith(P));
            //&& M.FrontMenu.MenuAccesses.Any(x => x.RoleId == roleID)

            //var parentIds = repoMenu.Query().Filter(M => M.Roles.Any(a => a.Id == roleID)).Get()
            //                .Select(x => x.ParentId);

            //var pages = repoMenu.Query().Filter(M => M.Roles.Any(a => a.Id == roleID) ||
            // (M.ParentId == 0)).Get().Where(M => parentIds.Contains(M.ParentId) || parentIds.Contains(M.Id))
            //     .Select(P => P.Name.ToLower() + (!String.IsNullOrEmpty(P.ChildPages) ? ("," + P.ChildPages) : ""))
            //    .ToList();


            //List<string> pageList = new List<string>();
            //if (pages.Count > 0)
            //{
            //    foreach (var page in pages)
            //    {
            //        string[] str = page.Trim().Split(',');
            //        if (str.Length > 1)
            //        {
            //            foreach (string value in str)
            //            {
            //                pageList.Add(value);
            //            }
            //        }
            //        else
            //        {
            //            pageList.Add(page);
            //        }
            //    }
            //}
            //return pageList.Any(P => url.ToLower().EndsWith(P));
            return true;
        }
        #endregion

        #region "Dispose"
        public void Dispose()
        {
            if (repoAdminUser != null)
            {
                repoAdminUser.Dispose();
                repoAdminUser = null;
            }
        }
        #endregion
    }
}
