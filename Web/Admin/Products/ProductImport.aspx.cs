﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBiz;
public partial class Admin_Products_ProductImport : System.Web.UI.Page
{
    BizProduct bizProduct = new BizProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.InnerHtml = string.Empty;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            bizProduct.ImportProductFromExcel(fuProduct.PostedFile.InputStream);
           
            lblMsg.Attributes["class"] = "success";
            lblMsg.InnerHtml = bizProduct.ImportMsg;
        }
        catch (Exception ex){
            lblMsg.Attributes["class"] = "error";
            string innerException = ex.InnerException==null ?"": ex.InnerException.Message;
            lblMsg.InnerHtml = ex.Message+"<br/>"+innerException;
        }

    }
}