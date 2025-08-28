using iTextSharp.text;
using iTextSharp.text.pdf;
using QuizGame.Core;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Controllers;
using QuizGame.Web.LIBS;
using QuizGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QuizGame.Controllers
{
    public class SiteController : BaseController
    {
        #region "Fields"
        private readonly IRoleService roleService;
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IBlogService blogService;


        ActivityLogDto aLogdto;
        public ActionAllowedDto action;
        private SqlConnection con;

        #endregion

        #region "Constructor"
        public SiteController(IBlogService _blogService, IEventService _eventService, IRoleService _userroleService, IActivityLogService _activityLogService, IUserService _userService) : base(_activityLogService, _userroleService)
        {
            this.blogService = _blogService;
            this.eventService = _eventService;
            this.roleService = _userroleService;
            this.userService = _userService;
            this.aLogdto = new ActivityLogDto();
            this.action = new ActionAllowedDto();
        }
        #endregion
        // GET: Site
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetBlogs()
        {
            var blogs = blogService.GetLatestActiveBlogs(5)
                        .Select(i => new BlogDto
                        {
                            ID = i.Id,
                            Title = i.Title,
                            ShortDescription = i.ShortDescription,
                            Description = i.Descriptions,
                            ShowImg = i.ImageUrl,
                            BlogDate = i.AddedDate?.ToString("dd MMM yyyy")
                        }).ToList();

            return PartialView("_showBlog", blogs);

        }

        public ActionResult GetEvents()
        {
            var events = eventService.GetUpcomingEvents(5)
                        .Select(i => new EventDto
                        {
                            ID = i.Id,
                            Title = i.Title,
                            Description = i.Descriptions,
                            ShowImg = i.ImageUrl,
                            EvtDate = i.EventDate?.ToString("dd MMM yyyy")
                        }).ToList();

            return PartialView("_showEvent", events);
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ContactDto contact = new ContactDto();
            return View(contact);
        }
        public ActionResult Donate()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            var photos = userService.GetGalleries().OrderByDescending(x => x.Id).ToList();
            return View(photos);
        }


        public ActionResult Event()
        {
            var events = eventService.GetEvent().OrderBy(x => x.Id).ToList();
            return View(events);
        }

        [HttpGet]
        [Route("Site/EventDetails/{id}")]
        public ActionResult EventDetails(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Event");
            }

            var events = eventService.GetEvent().Where(x => x.Title == id).FirstOrDefault();
            if (events == null)
            {
                return RedirectToAction("Event");
            }
            BlogDetailsModel blogdto = new BlogDetailsModel
            {
                Image = events.ImageUrl,
                Title = events.Title,
                Description = events?.Descriptions ?? "",
                Dates = events.EventDate?.ToString("dd MMMM yyyy")
            };
            return View(blogdto);
        }

        [Route("blog")]
        public ActionResult blog()
        {
            BlogListdto blogListdto = new BlogListdto();
            try
            {
                List<Data.Blog> blogs = blogService.GetBlog().Where(x => x.IsActive == true).OrderByDescending(x => x.AddedDate).ToList();
                List<BlogDetailsModel> blogdtos = new List<BlogDetailsModel>();
                foreach (var data in blogs)
                {
                    BlogDetailsModel blogdto = new BlogDetailsModel
                    {
                        Image = data.ImageUrl,
                        Title = data.Title,
                        Description = data.Descriptions,
                        Url = GenerateItemNameAsParam(data.Url),
                        Dates = data.AddedDate?.ToString("dd MMMM yyyy")
                    };
                    blogdtos.Add(blogdto);
                }

                var recentBlogs = blogService.GetBlog().Where(x => x.IsActive == true).Take(5).OrderByDescending(x => x.AddedDate).ToList();
                ViewBag.Data = recentBlogs;

                var blogDetail = blogService.GetBlog().Where(x => x.IsActive == true).OrderByDescending(x => x.Id).FirstOrDefault();
                if (blogDetail != null)
                {
                    BlogDetailsModel blogDetails = new BlogDetailsModel
                    {
                        Description = blogDetail.Descriptions,
                        ShortDescription = blogDetail.ShortDescription,
                        Image = blogDetail.ImageUrl,
                        Title = blogDetail.Title,
                        Url = blogDetail.Url
                    };
                    TempData["Blogs"] = blogDetails;
                }
                blogListdto.Blogs = blogdtos;
            }
            catch (Exception ex)
            {
                LogException(ex, "excp in blog Method");
            }
            return View(blogListdto);
        }

        [HttpGet]
        [Route("Site/blog/{id}")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("blog");
            }

            var blog = blogService.GetBlog().Where(x => x.Url == id && x.IsActive == true).FirstOrDefault();
            if (blog == null)
            {
                return RedirectToAction("blog");
            }
            BlogDetailsModel blogdto = new BlogDetailsModel
            {
                Image = blog.ImageUrl,
                Title = blog.Title,
                Description = blog.Descriptions,
                Url = blog.Url,
                Dates = blog.AddedDate?.ToString("dd MMMM yyyy")
            };

            var recentBlogs = blogService.GetBlog().Where(x => x.IsActive == true).Take(5).OrderByDescending(x => x.AddedDate).ToList();
            ViewBag.Data = recentBlogs;
            return View(blogdto);
        }

        [HttpPost]
        public ActionResult Contact(ContactDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Connection();
                    SqlCommand cmd = new SqlCommand("usp_ContactUs", con);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", model.Name);
                        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber.Trim());
                        cmd.Parameters.AddWithValue("@Message", model.Message);
                        //cmd.Parameters.AddWithValue("@email", model.Email);
                        cmd.Parameters.Add("@result", SqlDbType.Int);
                        cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        int Tokenvalid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                        if (Tokenvalid == 1)
                        {
                            model.Name = string.Empty;
                            model.MobileNumber = string.Empty;
                            model.Message = string.Empty;
                            ShowInfoMessage("Success", "Thank You For Contacting Us, Our team will connect with you as soon as possible.");
                        }
                        else
                        {
                            ShowInfoMessage("Hi", "Our team will connect with you as soon as possible.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex, "excp in ContactForm");
                    ShowErrorMessage("TryAfter", "Please try after some time.");
                }
            }
            else
            {
                ShowErrorMessage("Error", "Fill all required fields.");
            }
            return View(model);
        }

        #region JoinUs
        public ActionResult JoinUs()
        {
            ViewBag.DistrictList = userService.GetDistrictList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
            ViewBag.BlockList = userService.GetBlockList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
            ViewBag.GramPanchayatList = userService.GetGramPanchayatList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
            JoinUsDto join = new JoinUsDto();
            return View(join);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinUs(JoinUsDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DistrictList = userService.GetDistrictList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
                    ViewBag.BlockList = userService.GetBlockList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
                    ViewBag.GramPanchayatList = userService.GetGramPanchayatList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x?.Name ?? "NA" }).ToList();
                    var errorMessages = ModelState.Keys
                          .SelectMany(key => ModelState[key].Errors.Select(x => $"Field: {key}, Error: {x.ErrorMessage}"))
                          .ToList();

                    var errorMessage = string.Join("; ", errorMessages);

                    ShowErrorMessage("Error!", $"Check All Required Values. Errors: {errorMessage}");
                    return View(model);
                }
                //generate uniqueId
                string dt = DateTime.Now.ToString("yyMMddHHmmssffffff");
                model.UniqueId = Core.Common.GetUniqueAlphaNumericUP(12) + dt;
                Connection();
                SqlCommand cmd = new SqlCommand("usp_SaveJoinUsData", con);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@GuardianName", model.GuardianName);
                    cmd.Parameters.AddWithValue("@AlternateMobileNumber", model.AlternateMobileNumber);
                    cmd.Parameters.AddWithValue("@DOB", model.DOB);
                    cmd.Parameters.AddWithValue("@Village", model.Village);
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber.Trim());
                    cmd.Parameters.AddWithValue("@GramPanchayatId", model.GramPanchayatId);
                    cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                    cmd.Parameters.AddWithValue("@DistrictId", model.DistrictId);
                    cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
                    cmd.Parameters.AddWithValue("@Education", model.Education);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@AadharNumber", model.AadharNumber);
                    cmd.Parameters.AddWithValue("@PanNumber", model.PanNumber);
                    cmd.Parameters.AddWithValue("@UniqueId", model.UniqueId);
                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    int result = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                    if (result == 1)
                    {
                        // generate offer letter
                        var offerModel = new OfferLetterViewModel
                        {
                            Name = model.Name,
                            UniqueId = model.UniqueId
                        };
                        return View("ShowOfferLetter", offerModel);
                    }
                    else
                    {
                        ShowErrorMessage("TryAfter", "Try after some time.");
                        return View(model);
                    }
                }
            }
            catch (Exception)
            {
                ShowErrorMessage("TryAfter", "Try after some time.");
                return RedirectToAction("JoinUs");
            }
        }

        public ActionResult ShowOfferLetter()
        {
            return View();
        }

        public FileResult DownloadOfferLetter(string name, string uniqueId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Static + dynamic content
                document.Add(new Paragraph("OFFICIAL OFFER LETTER"));
                document.Add(new Paragraph("----------------------------------"));
                document.Add(new Paragraph($"Dear {name},"));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Congratulations!"));
                document.Add(new Paragraph("You have been selected to join our organization."));
                document.Add(new Paragraph($"Your Unique ID is: {uniqueId}"));
                document.Add(new Paragraph("Please keep this ID for future reference."));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Regards,"));
                document.Add(new Paragraph("Team Abhivaykati Foundation"));

                document.Close();
                writer.Close();

                byte[] byteInfo = ms.ToArray();
                return File(byteInfo, "application/pdf", $"OfferLetter_{uniqueId}.pdf");
            }
        }
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        [HttpGet]
        public ActionResult GetBlocksByDistrict(int districtId)
        {
            var blocks = userService.GetBlockList(districtId)
                          .Select(b => new { b.Id, b.Name })
                          .ToList();

            return Json(blocks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGramPanchayatsByBlock(int blockId)
        {
            var panchayats = userService.GetGramPanchayatList(blockId)
                .Select(x => new { x.Id, x.Name })
                .ToList();

            return Json(panchayats, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult JoinUs(JoinUsDto model)
        //{
        //    //try
        //    //{
        //    //    Connection();
        //    //    SqlCommand cmd = new SqlCommand("usp_ContactUs", con);
        //    //    {
        //    //        cmd.CommandType = CommandType.StoredProcedure;
        //    //        cmd.Parameters.AddWithValue("@Name", model.Name);
        //    //        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber.Trim());
        //    //        cmd.Parameters.AddWithValue("@Message", model.Message);
        //    //        //cmd.Parameters.AddWithValue("@email", model.Email);
        //    //        cmd.Parameters.Add("@result", SqlDbType.Int);
        //    //        cmd.Parameters["@result"].Direction = ParameterDirection.Output;
        //    //        con.Open();
        //    //        cmd.ExecuteNonQuery();
        //    //        con.Close();
        //    //        int Tokenvalid = Convert.ToInt32(cmd.Parameters["@result"].Value);
        //    //        if (Tokenvalid == 1)
        //    //        {
        //    //            model.Name = string.Empty;
        //    //            model.MobileNumber = string.Empty;
        //    //            model.Message = string.Empty;
        //    //            model.Response = "Thank You For Contacting Us, Our team will connect with you as soon as possible.";
        //    //            //return RedirectToAction("Response", model);
        //    //        }
        //    //        else
        //    //        {
        //    //            model.Response = "Our team will connect with you as soon as possible.";
        //    //            //return RedirectToAction("Response", model);
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception)
        //    //{

        //    //}
        //    return View(model);
        //}
        #endregion
        private void Connection()
        {
            con = new SqlConnection(SiteKey.SqlConn);
        }

        private string GenerateItemNameAsParam(string Name)
        {
            string phrase = string.Format("{0}", Name);
            string str = RemoveAccent(phrase).ToLower();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }


        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}