using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class AdminDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // seperate header and body elements on table
            FarmerGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}