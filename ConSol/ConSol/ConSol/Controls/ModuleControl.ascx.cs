using ConSolHWeb.Data;
using ConSolHWeb.Data.Models;
using System;
using System.Web;
using System.Web.UI;

namespace ConSol.Controls
{
    public partial class ModuleControl : BaseControl
    {
        private string moduleid = "";
        private string action = "";
        private string imagefile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: TMS_SelfModule ::";
            // Determine unique action for Update
            if (Request.Params["moduleid"] != null)
            {
                moduleid = Request.Params["moduleid"];
            }
            if (Request.Params["action"] != null)
            {
                action = Request.Params["action"];
                if (int.Parse(action) == 1)
                {
                    BtnCommand.Text = "Update TMS_SelfModule Info";
                    txtModuleID.Enabled = false;
                }
            }

            if (Page.IsPostBack == false)
            {
                if (moduleid != "")
                {
                    // Obtain a single row of event information
                    Module dr = DataService.Provider.getModuleInfoByModuleId(moduleid);

                    txtModuleName.Text = dr.ModuleName;
                    txtModuleDescription.Text = dr.ModuleDescription;
                    Session["imageFile"] = dr.ModuleImage;
                    imagefile = Session["imageFile"].ToString();
                    imagepath.ImageUrl = "~/Resources/" + Session["imageFile"].ToString();
                    txtModuleID.Text = dr.ModuleID;
                }
            }
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["moduleid"] != null)
                {
                    moduleid = Request.Params["moduleid"];
                }
                if (Request.Params["action"] != null)
                {
                    action = Request.Params["action"];
                }
                if (int.Parse(action) == 0)
                {
                    Module supplier = new Module();
                    supplier.ModuleID = (txtModuleID.Text == string.Empty) ? string.Empty : txtModuleID.Text;
                    supplier.ModuleName = (txtModuleName.Text == string.Empty) ? string.Empty : txtModuleName.Text;
                    supplier.ModuleDescription = (txtModuleDescription.Text == string.Empty) ? string.Empty : txtModuleDescription.Text;
                    supplier.ModuleImage = getImagePath();
                    supplier.active = 1;
                    DataService.Provider.AddModuleInfo(supplier);
                    ShowMessage("TMS_SelfModule Info Saved Successfully!");
                }
                else if (int.Parse(action) == 1)
                {
                    imagefile = Session["imageFile"].ToString();
                    Module supplier = new Module();
                    supplier.ModuleID = (txtModuleID.Text == string.Empty) ? string.Empty : txtModuleID.Text;
                    supplier.ModuleName = (txtModuleName.Text == string.Empty) ? string.Empty : txtModuleName.Text;
                    supplier.ModuleDescription = (txtModuleDescription.Text == string.Empty) ? string.Empty : txtModuleDescription.Text;
                    supplier.ModuleImage = (moduleImage.HasFile) ? getImagePath() : imagefile;
                    supplier.active = 1;
                    DataService.Provider.UpdateModuleInfo(supplier);
                    ShowMessage("TMS_SelfModule Info Updated Successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private string getImagePath()
        {
            string imagefile = null;
            HttpPostedFile file = Request.Files[0];
            if (moduleImage.HasFile)
            {
                imagefile = moduleid + moduleImage.FileName;
                file.SaveAs(Page.MapPath("~/Resources/" + imagefile));
                Session["imageFile"] = imagefile;
                return imagefile;
            }
            else
            {
                if (Session["imageFile"] != null)
                {
                    return Session["imageFile"].ToString();
                }
                else { return "no_image.jpg"; }
            }
        }
    }
}
