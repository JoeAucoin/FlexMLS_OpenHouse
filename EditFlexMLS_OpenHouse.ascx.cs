using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.FlexMLS_OpenHouse.Components;

namespace GIBS.Modules.FlexMLS_OpenHouse
{
    public partial class EditFlexMLS_OpenHouse : PortalModuleBase
    {

        int itemId = Null.NullInteger;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ItemId"] != null)
                {
                    itemId = Int32.Parse(Request.QueryString["ItemId"]);
                }

                if (!IsPostBack)
                {
                    //load the data into the control the first time
                    //we hit this page


                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");

                    //check we have an item to lookup
                    if (!Null.IsNull(itemId))
                    {
                        //load the item
                        FlexMLS_OpenHouseController controller = new FlexMLS_OpenHouseController();
                        FlexMLS_OpenHouseInfo item = controller.GetFlexMLS_OpenHouse(this.ModuleId, itemId);

                        if (item != null)
                        {
                            txtContent.Text = item.Content;
                            ctlAudit.CreatedByUser = item.CreatedByUserName;
                            ctlAudit.CreatedDate = item.CreatedDate.ToLongDateString();
                        }
                        else
                            Response.Redirect(Globals.NavigateURL(), true);
                    }
                    else
                    {
                        cmdDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                FlexMLS_OpenHouseController controller = new FlexMLS_OpenHouseController();
                FlexMLS_OpenHouseInfo item = new FlexMLS_OpenHouseInfo();

                item.Content = txtContent.Text;
                item.ItemId = itemId;
                item.ModuleId = this.ModuleId;
                item.CreatedByUser = this.UserId;

                //determine if we are adding or updating
                if (Null.IsNull(item.ItemId))
                    controller.AddFlexMLS_OpenHouse(item);
                else
                    controller.UpdateFlexMLS_OpenHouse(item);

                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Null.IsNull(itemId))
                {
                    FlexMLS_OpenHouseController controller = new FlexMLS_OpenHouseController();
                    controller.DeleteFlexMLS_OpenHouse(this.ModuleId, itemId);
                    Response.Redirect(Globals.NavigateURL(), true);
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}