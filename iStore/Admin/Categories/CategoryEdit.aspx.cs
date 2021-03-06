﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Categories
{
    public partial class CategoryEdit : System.Web.UI.Page
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategories.DataSource = allCategoriesList;
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataTextField = "Name";
                ddlCategories.DataBind();
                BL.Category category = CurrentCategory;
                if (category != null)
                {
                    txtName.Text = category.Name;
                    if (category.ParentID == null)
                    {
                        ddlCategories.SelectedValue = "parent";
                    }
                    else
                    {
                        ddlCategories.SelectedValue = category.ParentID.ToString();
                    }
                }
                else
                {
                    string scatId = Request.QueryString["parentId"];
                    if (!string.IsNullOrEmpty(scatId))
                    {
                        Guid catId = new Guid(scatId);
                        if (allCategories.Any(c => c.CategoryID == catId))
                        {
                            ddlCategories.SelectedValue = scatId;
                        }
                    }
                }
            }
            Page.Title = iStore.Modules.Controls.PageTitle.Get("Edit category");
        }

        protected void Save(object sender, EventArgs e)
        {
            string name = Server.HtmlEncode(txtName.Text.Trim());
            BL.Category category = CurrentCategory;
            string hf = ddlCategories.SelectedItem.Value;
            if (string.IsNullOrEmpty(name))
            {
                divError.InnerHtml = "Please enter name";
                divError.Visible = true;
                return;
            }

            if (txtName.Text.Length > 20)
            {
                divError.InnerHtml = "Name  must be no longer than 20 characters";
                divError.Visible = true;
                return;
            }
            if (cbl.CategoryNameInParentList(name, hf))
            {
                if (category == null)
                {
                    divError.InnerHtml = "Please chouse other category name";
                    divError.Visible = true;
                    return;
                }
            }
            else
            {
                if (category == null)
                {
                    if (hf == "parent")
                    {
                        cbl.AddCategory(name, null);
                    }
                    else
                    {
                        Guid parentId = new Guid(hf);
                        cbl.AddCategory(name, parentId);
                    }
                }
                else
                {
                    if (hf == "parent")
                    {
                        cbl.UpdateCategory(category.CategoryID, name, null);
                    }
                    else
                    {
                        Guid parentId = new Guid(hf);
                        cbl.UpdateCategory(category.CategoryID, name, parentId);
                    }
                }
                if (hf == "parent") hf = string.Empty;
                Response.Redirect(iStore.Site.SiteAdminUrl + "Categories/?cid=" + hf);
            }
        }

        public IQueryable<BL.Category> allCategories
        {
            get
            {
                return cbl.GetAllCategories().OrderBy(c => c.Sort);
            }
        }


        public IList<BL.Category> allCategoriesList
        {
            get 
            {
                IList<BL.Category> allcategories = allCategories.ToList();
                if (CurrentCategory == null)
                {
                    return allcategories;
                }
                return cbl.GetCategoriesWithotChild(CurrentCategory.CategoryID).ToList();
            }
        }

        public BL.Category CurrentCategory
        {
            get
            {
                string sid = Request.QueryString["cid"];
                if (sid != null)
                {
                    Guid id = new Guid(sid);
                    return cbl.GetCategoryById(id);
                }
                return null;
            }
        }
    }
}