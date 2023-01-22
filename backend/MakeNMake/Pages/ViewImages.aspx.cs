using MakeNMake.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake.ServiceEngineer
{
    public partial class ViewImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindPics();
                }
                catch
                {
                    Response.Redirect("ServicesTime.aspx");
                }
            }
        }
        private void BindPics()
        {
            Int64 TID =Convert.ToInt64( Utilities.EncryptDecrypt.DecryptText(Convert.ToString(Request.QueryString["ImgID"])));
            BLServiceEngineer obj = new BLServiceEngineer();
            DataTable dt = obj.GetImagesByID(TID);
            if (dt != null && dt.Rows.Count > 0)
            {
                rptImages.Visible = true;
                rptImages.DataSource = dt;
                rptImages.DataBind();
            }
            else
            {
                rptImages.Visible = false;
            }
        }

        protected void rptImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteImage")
            {
                BLServiceEngineer obj = new BLServiceEngineer();
                int result = obj.DeleteImage(Convert.ToInt64(e.CommandArgument));
                if (result == 1)
                {
                    HyperLink imagUrl = (HyperLink)e.Item.FindControl("hyperMainImg");
                    Image imgThumb = (Image)e.Item.FindControl("imgThumb");
                    FileInfo file = new FileInfo("~/UserImages/Issues" + imagUrl.NavigateUrl.Substring(imagUrl.NavigateUrl.LastIndexOf("/") + 1));
                    if (file.Exists)
                    {
                        file.Delete();
                        FileInfo fileThumbnail = new FileInfo("~/UserImages/IssuesThumb" + imgThumb.ImageUrl.Substring(imgThumb.ImageUrl.LastIndexOf("/") + 1));
                        if (fileThumbnail.Exists)
                        {
                            fileThumbnail.Delete();
                        }
                    }
                    BindPics();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Sucessfully Deleted') ;", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Problem occurs while deleting') ;", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServicesTime.aspx");
        }
    }
}