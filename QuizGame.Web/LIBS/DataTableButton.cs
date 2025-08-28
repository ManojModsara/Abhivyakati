using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizGame.Web.LIBS
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
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='Edit'> <i class='fa fa-edit'></i></a>";
        }


        public static string UploadInvoice(string actionUrl, string targetModalId)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='Edit'> <i class='fa fa-edit'></i></a>";
        }
        public static string UploadInvoice2(string actionUrl)
        {
            return "<a  href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='Edit'> <i class='fa fa-edit'></i></a>";
        }
        public static string LinkButton(string actionUrl, string targetModalId, string linkTxt, string title)
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "'  class='btn btn-link' title='" + title + "' ></a>";
        }
        public static string HyperLink1(string actionUrl, string targetModalId, string linkTxt, string title, string color = "", string onClick = "")
        {

            if (string.IsNullOrEmpty(color))
            {
                return "<a  onclick=" + targetModalId + "   href='" + actionUrl + "'  title='" + title + "' >" + (!string.IsNullOrEmpty(linkTxt) ? linkTxt : string.Empty) + "</a>";
                // return "<button onclick=ConfirmReject(1, 'vikas')>Try it</button>";
            }
            else
            {

                return "<a   onclick=" + targetModalId + "  href='" + actionUrl + "' title='" + title + "' style='color:" + color + ";font-weight:bold;' >" + (!string.IsNullOrEmpty(linkTxt) ? linkTxt : string.Empty) + "</a>";
            }

        }

        public static string PrintButton(string actionUrl, string id, string text = "Add", string title = "Add")
        {
            return "<a id='" + id + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + title + "' target='_blank'> <i class='fa fa-print'></i></a>";
        }
        public static string HyperLink(string actionUrl, string targetModalId, string linkTxt, string title, string color = "")
        {
            if (string.IsNullOrEmpty(color))
            {
                return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' title='" + title + "' >" + (!string.IsNullOrEmpty(linkTxt) ? linkTxt : string.Empty) + "</a>";
            }
            else
            {
                return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' title='" + title + "' style='color:" + color + ";font-weight:bold;' >" + (!string.IsNullOrEmpty(linkTxt) ? linkTxt : string.Empty) + "</a>";
            }

        }
        public static string HyperLink12(string actionUrl, string title)
        {
            return "<a  href='" + actionUrl + "' title='" + title + "'</a>";
        }

        /// <summary>
        /// Edit button which will redirect to different page.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public static string EditButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='Edit'> <i class='fa fa-edit'></i></a>";
        }

        public static string CustomButton(string actionUrl, string Icon, string title = "Edit")
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + title + "'> <i class='" + Icon + "'></i></a>";
        }
        public static string DynamicSetting(string actionUrl, string titel)
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + titel + "'> <i class='fa fa-cog'></i></a>";
        }
        public static string ValidateButton(string actionUrl, string targetModalId, string displayName = "Update")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-bitbucket grid-btn btn-sm' title='" + displayName + "'  > <i class='fa fa-check'></i></a>";
        }

        public static string colorspan(string color, string text)
        {
            return "<span style='color:" + color + ";font-weight:bold'>" + text + "</span>";
        }

        /// <summary>
        /// Edit button which will redirect to different page.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public static string ViewButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' target='_blank' title='View'><i class='fa fa-eye'></i></a>";
        }

        /// <summary>
        /// Delete button to pass additional parameters in url and open popup in same window.
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="targetModalId"></param>
        /// <returns></returns>
        public static string DeleteButton(string actionUrl, string targetModalId, string displayName = "Delete")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-danger grid-btn btn-sm' title='" + displayName + "'><i class='fa fa-trash-o'></i></a>";
        }
        public static string DeleteButtonformodal(string actionUrl, string targetModalId, string displayName = "Delete")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-danger grid-btn btn-sm' title='" + displayName + "'><i class='fa fa-trash-o'></i></a>";
        }

        public static string DeleteButton(string actionUrl, string displayName = "Delete")
        {
            return "<a href='" + actionUrl + "' class='btn btn-danger grid-btn btn-sm' title='" + displayName + "'><i class='fa fa-trash-o'></i></a>";
        }
        public static string ShowGst_File(string actionUrl, string targetModalId, string title = "Add")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'><i class='fa fa-plus'></i></a>";
        }



        public static string Showformfill(string actionUrl, string targetModalId, string title = "Add File", string icon = "fa fa-plus")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'><i class='" + icon + "'></i></a>";
        }

        public static string ComplaintButton(string actionUrl, string targetModalId, string title = "Generate Complaint")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'><i class='fa fa-plus'></i></a>";
        }
        public static string ComplaintReOpenButton(string actionUrl, string targetModalId, string title = "Generate Complaint")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + title + "'><i class='fa fa-plus'></i></a>";
        }

        public static string ExtLinkRedButton(string actionUrl, string tiltle = "Look-Up")
        {
            return "<a href='" + actionUrl + "' class='btn btn-danger grid-btn btn-sm' target='_blank' title='" + tiltle + "'><i class='fa fa-external-link'></i></a>";

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
        public static string DynamicSettingbtn(this HtmlHelper htmlHelper, string actionUrl, string targetModalId, string title = "Msg Template")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success' title='" + title + "'><i class='fa fa-cog'></i></a>";
        }
        public static string W2RComplainButton(string actionUrl, string targetModalId, string title = "Generate Complaint")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'>W2RComplainButton</a>";
        }
        //public static string W2RComplainButton(this HtmlHelper htmlHelper, string actionUrl, string targetModalId)
        //{
        //    return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success'>W2RComplainButton</a>";

        //    //return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success'>W2RComplain</a>";
        //}

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
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='View'><i class='fa fa-eye'></i></a>";
        }

        public static string ListButton(string actionUrl, string title, string displayText = "")
        {
            return "<a href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + title + "'>" + displayText + "<i class='fa fa-eye'></i></a>";
        }

        public static string CheckBox(string Id, string Val, string Class)
        {
            return "<input type='checkbox' id='" + Id + "' name='" + Id + "' value='" + Val + "' class='" + Class + "' />";
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
        public static string UpdateButton(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success'>Update Group Url<i class='fa fa-plus'></i></a>";
        }
        public static string UBankInsertButton(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success'>Add New Bank<i class='fa fa-plus'></i></a>";
        }
        public static string AppredeemInsertButton(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success'>Add New Gst_File<i class='fa fa-plus'></i></a>";
        }

        public static string SettingButtonCshtml(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success'><i class='fa fa-cog'></i></a>";
            //return "<a href='" + actionUrl + "' class='btn btn-" + cls + " grid-btn btn-sm' title='" + text + "'>  <i class='fa fa-cog'></i></a>";
        }
        public static string ShowGst_File2(this HtmlHelper htmlHelper, string actionUrl, string targetModalId, string title = "Show Gst_Files")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'><i class='fa fa-eye'></i></a>";
        }

        public static string UploadExcel(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-default'>Upload By Excel <i class='fa fa-file-excel-o'></i></a>";
        }
        public static string W2RComplainButton1(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>W2RComplainButton</a>";

        }

        public static string DownloadExcel(this HtmlHelper htmlHelper, string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-default'>Download Excel <i class='fa fa-file-excel-o'></i></a>";
        }
        public static string AddImageButton(string actionUrl)
        {
            return "<a href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm'>Add/Edit Images <i class='fa fa-file-picture'></i></a>";
        }
        public static string SettingButton(string actionUrl, string text, string cls = "success")
        {
            return "<a href='" + actionUrl + "' class='btn btn-" + cls + " grid-btn btn-sm' title='" + text + "'>  <i class='fa fa-cog'></i></a>";
        }

        public static string RefreshVbalButton(string actionUrl, string title = "Refresh")
        {
            return "<a class='btn btn-success' title='" + title + "' onclick='GetBalance(this)' > <i class='fa fa-refresh'></i></a>";
        }
        public static string RefreshVbalFetchButton(int id, string title = "Refresh")
        {
            //return "<a class='btn btn-success' title='" + title + "' onclick='GetBalance(" + id + ")' > <i class='fa fa-refresh'></i></a>";
            //< a class="btn btn-success" title="Fetch Vendor Balance" data-id="302"> <i class="fa fa-refresh"></i></a>

            return "<a class='btn btn-success btnRefresh' title='" + title + "' data-id=" + id + " > <i class='fa fa-refresh'></i></a>";
        }

        public static string RefreshAllButton(this HtmlHelper htmlHelper, string actionUrl, string title = "Refresh All")
        {
            return "<a href='" + actionUrl + "' title='" + title + "' class='btn btn-success'> Refresh All <i class='fa fa-refresh'></i></a>";
        }

        public static string RefreshButton(string actionUrl, string title = "Refresh")
        {
            return "<a href='" + actionUrl + "' class='btn btn-success' title='" + title + "' > <i class='fa fa-refresh'></i></a>";
        }

        public static string AddButton(string actionUrl, string id, string text = "Add", string title = "Add")
        {
            return "<a id='" + id + "' href='" + actionUrl + "' class='btn btn-primary grid-btn btn-sm' title='" + title + "'> " + text + "<i class='fa fa-plus'></i></a>";
        }

        public static string PlusButton(string actionUrl, string id, string title = "Add", string displayText = "")
        {
            return "<a id='" + id + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'>" + displayText + "<i class='fa fa-plus'></i></a>";
        }

        public static string PlusButton2(string actionUrl, string targetModalId, string title = "Add", string displayText = "")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'>" + displayText + "<i class='fa fa-plus'></i></a>";
        }

        public static string ShowData(string actionUrl, string targetModalId, string title = "Details", string displayText = "")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'>" + displayText + "<i class='fa fa-bank'></i></a>";
        }

        public static string RupayButton(string actionUrl, string targetModalId, string title = "Details", string displayText = "")
        {
            return "<a data-toggle='modal' data-target='#" + targetModalId + "' href='" + actionUrl + "' class='btn btn-success grid-btn btn-sm' title='" + title + "'>" + displayText + "<i class='fa fa-inr'></i></a>";
        }

        public static string ButtonJS(string onclick, string icon, string btnType, string title = "", string displayName = "")
        {

            return "<a class='btn btn-" + btnType + "' title='" + title + "' " + (!string.IsNullOrEmpty(onclick) ? " onclick='" + onclick + "'" : "") + " > " + displayName + "<i class='fa fa-" + icon + "'></i></a>";
        }
        public static string DeleteButtonJS(string onclick, string icon, string btnType, string title = "", string displayName = "")
        {
            return "<input type='button' class='btn btn-" + btnType + " fa fa-" + icon + "' title='" + title + "' " +
                   (!string.IsNullOrEmpty(onclick) ? " onclick='" + onclick + "'" : "") +
                   " style='width:34px;height:34px' value='" + displayName + "' />";
        }

        public static string IconButton(string onclick, string icon, string color, string title = "")
        {
            return "<i class='fa fa-" + icon + "' title='" + title + "' " +
                  (!string.IsNullOrEmpty(onclick) ? " onclick='" + onclick + "'" : "") +
                   " style='color:" + color + ";font-size:30px;cursor:pointer' />";

            //return "<i style = 'color: #0058a6;' class='fa fa-address-card;' title='Complete PAN' (!string.IsNullOrEmpty(onclick) ? " onclick = '" + onclick + "'" : "") ></i>"
        }


        public static string ButtonDownload(string path, string displayName = "", string title = "Download")
        {
            return "<a  title='" + title + "' href='" + path + "' download=''> " + displayName + "</a>";
        }
        public static string Label(string color, string text, string url = "#")
        {
            return "<a  href='" + url + "' title='Status' style='color:" + color + ";font-weight:bold;' >" + text + " </a>";
        }

        //internal static string W2RComplainButton(string v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}