
using System.Collections.Generic;

using System.Web.UI.WebControls;
using QuizGame.Core;

namespace QuizGame.Service
{
    public interface IRoleService
    {
        List<Data.Menu> GetMenu();
        List<Data.MapMenuToRole> GetRolePermission(int roleid);
        Data.Role Save(Data.Role adminRole);
        List<Data.Menu> GetMenusByRoleId(int roleid);
        Data.Role GetAdminRole(int id);
        KeyValuePair<int, List<Data.Role>> GetAdminRoles(DataTableServerSide searchModel);
        bool CheckCurrentMenu(string url, int roleID);

        bool AddRolePermission(List<Data.MapMenuToRole> mapMenuToRoles);
        bool DeleteRolePermission(int id);
    }
}
