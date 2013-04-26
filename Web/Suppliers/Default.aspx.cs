﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NModel;
using NBiz;
public partial class Suppliers_Default : System.Web.UI.Page
{
    BizSupplier bizSupplier = new NBiz.BizSupplier();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindSupplier();
        }
    }
    private int GetPageIndex()
    {
        int pageIndex = 0;
        string paramPageIndex = Request[pager.UrlPageIndexName];
        if (!int.TryParse(paramPageIndex, out pageIndex))
        {
            pageIndex = 0;
        }
        return pageIndex;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSupplier();
    }

    private void BindSupplier()
    {
        int pageIndex = GetPageIndex();
        int totalRecords;
        var product = bizSupplier.Search( tbxName.Text.Trim(), pageIndex, pager.PageSize, out totalRecords);  // bizProduct.GetAll<NModel.Product>();
        pager.RecordCount = totalRecords;
        dgSupplier.DataSource = product;
        dgSupplier.DataBind();
    }
}