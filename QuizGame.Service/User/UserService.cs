using Dapper;
using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto.Model;
using QuizGame.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class UserService: IUserService
    {

        #region "Fields"
        private IRepository<User> repoAdminUser;
        private IRepository<ContactU> repocontactList;
        private IRepository<PhotoGallery> repoPhotoGalleryList;
        private IRepository<UserProfile> repoUserProfile;
        private IRepository<Role> repoAdminRole;
        private IRepository<Menu> repoMenu;
        private IRepository<District> repoDistrict;
        private IRepository<Block> repoBlock;
        private IRepository<GramPanchayat> repoGramPanchayat;
        private readonly string _connectionString;

        #endregion

        #region "Cosntructor"
        public UserService(IRepository<District> _repoDistrict, IRepository<Block> _repoBlock, IRepository<GramPanchayat> _repoGramPanchayat, IRepository<UserProfile> _repoUserProfile, IRepository<ContactU> _repocontactList, IRepository<PhotoGallery> _repoPhotoGalleryList, IRepository<User> _repoUser, IRepository<Role> _repoAdminRole, IRepository<Menu> _repoMenu)
        {
            this.repoAdminUser = _repoUser;
            this.repoDistrict = _repoDistrict;
            this.repoBlock = _repoBlock;
            this.repoGramPanchayat = _repoGramPanchayat;
            this.repocontactList = _repocontactList;
            this.repoPhotoGalleryList = _repoPhotoGalleryList;
            this.repoAdminRole = _repoAdminRole;
            this.repoMenu = _repoMenu;
            this.repoUserProfile = _repoUserProfile;
            _connectionString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;

        }
        #endregion

        public ICollection<Role> GetAdminRole(int roleid,bool isActive = false)
        {
                return repoAdminRole.Query().AsTracking()
                    .Get().ToList();
        }

        public User GetAdminUser(int id)
        {
            return repoAdminUser.FindById(id);
        }

        public Role GetAdminRole(int roleId)
        {
            return repoAdminRole.FindById(roleId);
        }

        public List<User> AdminRoleUserList(int roleid=1)
        {
            return repoAdminUser.Query().Filter(x => x.Role.Id == roleid-1).Get().ToList();
        }

        public List<PhotoGallery> GetGalleries()
        {
            return repoPhotoGalleryList.Query().Get().ToList();
        }

        public KeyValuePair<int, List<User>> GetAdminUsers(DataTableServerSide searchModel, int userId = 0)
        {
            var predicate = CustomPredicate.BuildPredicate<User>(searchModel, new Type[] {typeof(User),typeof(UserProfile), typeof(Role) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<User> results = repoAdminUser
                .Query()
                .Filter(predicate.And(a =>  (userId == 0 ? true : a.Id != userId) && (a.Role.Id != 1))) //a.IsDeleted == false &&
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(User), typeof(UserProfile),typeof(Role) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<User>> resultResponse = new KeyValuePair<int, List<User>>(totalCount, results);

            return resultResponse;
        }

        public KeyValuePair<int, List<User>> GetAdminRoleUser(DataTableServerSide searchModel, int userId = 0,int RoleID=1)
        {
            var predicate = CustomPredicate.BuildPredicate<User>(searchModel, new Type[] { typeof(User), typeof(UserProfile), typeof(Role) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            if (RoleID == 1)
            {
                List<User> results = repoAdminUser
                   .Query()
                   .Filter(predicate) //a.IsDeleted == false &&
                   .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(User), typeof(UserProfile), typeof(Role) }))
                   .GetPage(page, searchModel.length, out totalCount)
                   .ToList();
                KeyValuePair<int, List<User>> resultResponse = new KeyValuePair<int, List<User>>(totalCount, results);

                return resultResponse;
            }
            else
            {
                List<User> results = repoAdminUser
                    .Query()
                    //.Filter(predicate.And(a => (userId == 0 ? true : a. != userId) && (a.R != 1) && a.UserProfile.Parentid == userId)) //a.IsDeleted == false &&
                    .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(User), typeof(UserProfile), typeof(Role) }))
                    .GetPage(page, searchModel.length, out totalCount)
                    .ToList();
                KeyValuePair<int, List<User>> resultResponse = new KeyValuePair<int, List<User>>(totalCount, results);

                return resultResponse;
            }
           
        }

        public User Save(User adminUser)
        {
            
            if (adminUser.Id == 0)
            {
                adminUser.AddedDate = DateTime.Now;
                repoAdminUser.Insert(adminUser);
            }
           else if (adminUser?.UserProfile?.UserId == 0)
            {
                adminUser.AddedDate = DateTime.Now;
                repoUserProfile.Insert(adminUser.UserProfile);
            }
            else
            {
                repoAdminUser.Update(adminUser);
            }
            return adminUser;
        }
        public PhotoGallery Save(PhotoGallery gallery)
        {
            repoPhotoGalleryList.Insert(gallery);
            return gallery;
        }
        public KeyValuePair<int, List<ContactU>> GetContactList(DataTableServerSide searchModel)
        {
            var predicate = CustomPredicate.BuildPredicate<ContactU>(searchModel, new Type[] { typeof(ContactU)});

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<ContactU> results = repocontactList
                .Query()
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(ContactU)}))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<ContactU>> resultResponse = new KeyValuePair<int, List<ContactU>>(totalCount, results);

            return resultResponse;
        }

        public KeyValuePair<int, List<PhotoGallery>> GetPhotoGalleryList(DataTableServerSide searchModel)
        {
            var predicate = CustomPredicate.BuildPredicate<ContactU>(searchModel, new Type[] { typeof(PhotoGallery) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<PhotoGallery> results = repoPhotoGalleryList
                .Query()
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(PhotoGallery) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<PhotoGallery>> resultResponse = new KeyValuePair<int, List<PhotoGallery>>(totalCount, results);

            return resultResponse;
        }

        public bool Active(int id)
        {
            var AdminUser = repoAdminUser.FindById(id);
            if (AdminUser != null)
            {
                AdminUser.IsActive = !(AdminUser.IsActive);
                repoAdminUser.SaveChanges();
            }
            return true;
        }

        public bool DeleteGalleryImage(int Id)
        {
            try
            {
                repoPhotoGalleryList.Delete(Id);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<District> GetDistrictList(int zoneId)
        {
            if(zoneId > 0)
            {
                return repoDistrict.Query().Get().Where(x => x.ZoneId == zoneId).ToList();
            }
            else
            {
                return repoDistrict.Query().Get().ToList();
            }
        }

        public List<Block> GetBlockList(int districtId)
        {
            if (districtId > 0)
            {
                return repoBlock.Query().Get().Where(x => x.DistrictId == districtId).ToList();
            }
            else
            {
                return repoBlock.Query().Get().ToList();
            }
        }
        public List<GramPanchayat> GetGramPanchayatList(int blockId)
        {
            if (blockId > 0)
            {
                return repoGramPanchayat.Query().Get().Where(x => x.BlockId == blockId).ToList();
            }
            else
            {
                return repoGramPanchayat.Query().Get().ToList();
            }
        }

        public KeyValuePair<int, List<JoiningDataModel>> GetJoiningDatas(DataTableServerSide searchModel)
        {
            int totalCount = 0;
            List<JoiningDataModel> result = null;
            try
            {
                var fdata = searchModel.filterdata;
                int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@PageNumber", page);
                    parameters.Add("@PageSize", (searchModel?.length ?? 0) > 0 ? searchModel.length : 25);
                    parameters.Add("@Name", fdata?.Name ?? "");
                    parameters.Add("@UniqueId", fdata?.RefId ?? "");
                    parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    result = connection.Query<JoiningDataModel>(
                        "usp_JoinUsData",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();
                    totalCount = parameters.Get<int>("@TotalCount");
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return new KeyValuePair<int, List<JoiningDataModel>>(totalCount, result);
        }

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
