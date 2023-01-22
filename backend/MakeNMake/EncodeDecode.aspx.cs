using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeNMake
{
    public partial class EncodeDecode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btndecode_Click(object sender, EventArgs e)
        {
            lblmsg.Text = MakeNMake.Utilities.EncryptDecrypt.DecryptText(txtdecode.Text);
        }
    }
}