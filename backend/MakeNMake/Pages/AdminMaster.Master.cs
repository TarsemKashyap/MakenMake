using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MakeNMake.CommomFunctions;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text;
using MakeNMake.BL;
using System.Web.UI.HtmlControls;

namespace MakeNMake.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[Constant.Session.AdminSession] == null || Session[Constant.Session.Role]==null)
            {
                Response.Redirect("~/" + Constant.Pages.Login);
            }
            else
            {
                if (!IsPostBack)
                {
                    string LoggerUserName = BindLoginUserName(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    loggedPerson.InnerText = " Welcome " + LoggerUserName;
                    int roleID = Convert.ToInt32(Session[Constant.Session.Role]);                    
                    
                    if (roleID == 4)
                    {
                        if (CheckServiceBalance(Convert.ToInt64(Session[Constant.Session.AdminSession])))
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Hi " + LoggerUserName + " , Your Services has been expired , Please buy new services. ') ;", true);
                        }
                    }
                    BindRolePages(Convert.ToInt64(Session[Constant.Session.AdminSession]));
                    treeModule.CollapseAll();
                    Messages(roleID);
                }
            }
        }
        
        private bool CheckServiceBalance(Int64 UserID)
        {
            return true;
        }
        private string BindLoginUserName(Int64 UserID)
        {
            BL.BLAdmin obj = new BL.BLAdmin();
            return obj.GetLoginuserName(UserID);

        }
        private void Messages(Int32 roleid)
        {
            Common getInfo = new Common();
            DataTable dt = getInfo.GetMessageCirculatD(roleid);
            if (dt != null && dt.Rows.Count > 0)
            {
                dvouterMessage.Visible = true;
                dvMessages.Visible = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.Attributes.Add("class", "bg-info");
                    p.Attributes.Add("style", "margin-bottom:20px;");
                    p.InnerHtml = "**  " + Convert.ToString(dt.Rows[i]["Message"]) + "  ** ";
                    dvMessages.Controls.Add(p);
                }
            }
            else
            {
                dvouterMessage.Visible = false;
                dvMessages.Visible = false;
            }
        }       
        public string LoggedPeron(int roleID)
        {
            string LoggedPersonRole=string.Empty;
            if (roleID == (int)Constant.Users.Administrator)
            {
                LoggedPersonRole = "Administrator";
            }
            else if (roleID == (int)Constant.Users.ServiceEngineer)
            {
                LoggedPersonRole = "Service Engineer";
            }
            else if (roleID == (int)Constant.Users.CustomerCare)
            {
                LoggedPersonRole = "Customer Care";
            }
            else if (roleID == (int)Constant.Users.Consumer)
            {
                LoggedPersonRole = "Client";
            }
            else if (roleID == (int)Constant.Users.MIS)
            {
                LoggedPersonRole = "MIS";
            }
            else if (roleID == (int)Constant.Users.ExclationManager)
            {
                LoggedPersonRole = "Escalation Manager";
            }
            else if (roleID == (int)Constant.Users.InventoryManager)
            {
                LoggedPersonRole = "Inventory Manager";
            }
            else if (roleID == (int)Constant.Users.AccountManager)
            {
                LoggedPersonRole = "Account Manager";
            }
            return LoggedPersonRole;
        }
        private void BindRolePages(Int64 UserID)
        {
            try
            {
                BL.BLAdmin obj = new BL.BLAdmin();
                DataTable dt = obj.GetRoleWisePagesByUserID(UserID);

                StringBuilder strTree = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strTree.Append(dt.Rows[i][0]);
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Convert.ToString(strTree));
                RootObject results;
                var rows = xmlDoc.SelectNodes("//Parents");
                if (rows.Count == 1)
                {
                    XmlNode RootNode = xmlDoc.SelectSingleNode("//MyRoot");
                    XmlNode newElem = xmlDoc.CreateNode("element", "Parents", "");
                    newElem.InnerText = "";
                    XmlNode newElemparent = xmlDoc.CreateNode("element", "ParentName", "");
                    newElem.InnerText = "";
                    XmlNode newElemchild = xmlDoc.CreateNode("element", "child", "");
                    newElem.InnerText = "";
                    newElem.AppendChild(newElemparent);
                    newElem.AppendChild(newElemchild);
                    RootNode.AppendChild(newElem);
                    xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(",null]", "]");
                    string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
                    results = JsonConvert.DeserializeObject<RootObject>(jsonText);
                }
                else
                {
                    string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
                    results = JsonConvert.DeserializeObject<RootObject>(jsonText);
                }
                int count = 0;
                treeModule.Nodes.Clear();
                foreach (var data in results.MyRoot.Parents)
                {
                    if (data.ParentName != null && data.child != null)
                    {
                        TreeNode parent = new TreeNode(data.ParentName, string.Empty);
                        treeModule.Nodes.Add(parent);
                        treeModule.Nodes[count].SelectAction = TreeNodeSelectAction.None;
                        List<child> items = new List<child>();
                        if ((data.child).GetType().Name == "JArray")
                        {
                            items = ((JArray)data.child).Select(x => new child
                            {
                                PageName = (string)x["PageName"],
                                PageTitle = (string)x["PageTitle"]
                            }).ToList();
                        }
                        else
                        {
                            var jChild = ((JObject)data.child).ToObject<Dictionary<string, string>>();
                            items.Add(new child { PageName = jChild["PageName"], PageTitle = jChild["PageTitle"] });
                        }
                        foreach (var child in items)
                        {
                            TreeNode childnode = new TreeNode(child.PageTitle, string.Empty);
                            childnode.NavigateUrl = child.PageName;
                            parent.ChildNodes.Add(childnode);
                        }
                        count++;
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "alert('Please contact administrator there is some issue regarding your account') ;", true);
            }
        }
        public class child
        {
            public string PageName { get; set; }
            public string PageTitle { get; set; }
        }
        public class Parent
        {
            public string ParentName { get; set; }
            public object child { get; set; }
        }
        public class MyRoot
        {
            public List<Parent> Parents { get; set; }
        }
        public class RootObject
        {
            public MyRoot MyRoot { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void treeModule_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/" + Constant.Pages.Login);
        }
        
    }
}