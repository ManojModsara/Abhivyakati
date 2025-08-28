using System.Web.Mvc;

namespace QuizGame.Web
{
    public static class DataTableButton
    {

        /// <summary>
        /// Edit button to pass additional parameters in url and open popup in same window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string EditButton(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm'> <i class='fa fa-edit'></i></a>";
        }

        public static string LinkButton(string actionUrl, string targetModalId, string linkTxt, string title)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "'  class='btn btn-link' title='"+title+"' >" + linkTxt + "</a>";
        }

        public static string HyperLink(string actionUrl, string targetModalId, string linkTxt, string title)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' title='" + title + "' >" + linkTxt + "</a>";
        }

        /// <summary>
        /// Edit button which will redirect to different page.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public static string EditButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm'><i class='fa fa-edit'></i></a>";
        }


        /// <summary>
        /// Edit button which will redirect to different page.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public static string ViewButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' target='_blank'> <i class='fa fa-eye'></i></a>";
        }

        /// <summary>
        /// Delete button to pass additional parameters in url and open popup in same window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string DeleteButton(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-danger grid-btn btn-sm'> <i class='fa fa-trash-o'></i></a>";
        }
        

        /// <summary>
        /// Insert button to pass additional parameters in url and open popup in same window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string InsertButton(this HtmlHelper htmlHelper, string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success'>Add new <i class='fa fa-plus'></i></a>";
        }


        /// <summary>
        /// Insert button to pass additional parameters in url and open popup in same window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string AddWalletButton(this HtmlHelper htmlHelper, string actionUrl, string targetModalId)
        { 
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success'>Add Wallet <i class='fa fa-plus'></i></a>";
        }


        public static string ViewButton(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>";
        }
        

        /// <summary>
        /// Insert button to pass additional parameters in url and open page in new window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string InsertButton(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success'>Add new <i class='fa fa-plus'></i></a>";
        }

        public static string AddImageButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Add/Edit Images <i class='fa fa-file-picture'></i></a>";
        }

        public static string MapHotelButton(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Map Hotels <i class='fa fa-file-picture'></i></a>";
        }

        public static string MapHotelButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Map Hotels <i class='fa fa-file-picture'></i></a>";
        }

        public static string AddItineraryButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'> Manage Itineraries <i class='fa fa-file-picture'></i></a>";
        }

        public static string MapHolidayButton(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Map Holiday Type <i class='fa fa-file-picture'></i></a>";
        }

        public static string MapHolidayButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Map Holiday Type <i class='fa fa-file-picture'></i></a>";
        }

        public static string AddButton(string actionUrl, string text)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'> " + text + " <i class='fa fa-file-picture'></i></a>";
        }
        public static string SettingButton(string actionUrl, string text)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'> <i class='fa fa-cogs'></i></a>";
        }
    }
}