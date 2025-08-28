using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuizGame.Service
{
    public interface IUserService
    {
        ICollection<Role> GetAdminRole(int Roleid, bool isActive = false);
        User GetAdminUser(int id);
        Role GetAdminRole(int roleId);
        KeyValuePair<int, List<User>> GetAdminUsers(DataTableServerSide searchModel, int userId = 0);
        KeyValuePair<int, List<User>> GetAdminRoleUser(DataTableServerSide searchModel, int userId = 0,int role=1);
        KeyValuePair<int, List<ContactU>> GetContactList(DataTableServerSide searchModel);
        KeyValuePair<int, List<PhotoGallery>> GetPhotoGalleryList(DataTableServerSide searchModel);
        List<PhotoGallery> GetGalleries();
        List<User> AdminRoleUserList(int roleid = 1);
        User Save(User adminUser);
        PhotoGallery Save(PhotoGallery gallery);
        bool Active(int id);
        bool DeleteGalleryImage(int Id);
        List<District> GetDistrictList(int ZoneId = 0);
        List<Block> GetBlockList(int districtId = 0);
        List<GramPanchayat> GetGramPanchayatList(int blockId = 0);
        KeyValuePair<int, List<JoiningDataModel>> GetJoiningDatas(DataTableServerSide searchModel);
    }
}
