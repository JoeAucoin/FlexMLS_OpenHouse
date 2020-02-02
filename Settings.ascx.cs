using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.Modules.FlexMLS_OpenHouse.Components;
using System.Collections;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;

namespace GIBS.Modules.FlexMLS_OpenHouse
{
    public partial class Settings : FlexMLS_OpenHouseSettings
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    BindModules();
                    
              //      FlexMLS_OpenHouseSettings settingsData = new FlexMLS_OpenHouseSettings(this.TabModuleId);

                    if (Town != null)
                    {
                        txtTown.Text = Town.ToString();
                    }
                    if (FlexMLSModule != null)
                    {
                        ddlFlexMLSModule.SelectedValue = FlexMLSModule.ToString();
                        
                    }

                    if (MLSImagesUrl != null)
                    {
                        txtMLSImagesUrl.Text = MLSImagesUrl.ToString();

                    }

                    
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        // GET THE DROPDOWN FOR GIBS - FlexMLS MODULES
        private void BindModules()
        {

            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "GIBS - FlexMLS");

            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {
                    ListItem objListItem = new ListItem();

                    objListItem.Value = mi.TabID.ToString();    // mi.ModuleID.ToString();
                    objListItem.Text = mi.ModuleTitle.ToString();

                    ddlFlexMLSModule.Items.Add(objListItem);

                    // get module title 
                    //mi.ModuleTitle;
                    // additionally, you can find out what tab it is on //mi.TabID;
                    //mi.ModuleID;
                }
            }


            ddlFlexMLSModule.Items.Insert(0, new ListItem(Localization.GetString("SelectModule", this.LocalResourceFile), ""));
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                //FlexMLS_OpenHouseSettings settingsData = new FlexMLS_OpenHouseSettings(this.TabModuleId);
                //settingsData.Town = txtTown.Text;
                //settingsData.FlexMLSModule = ddlFlexMLSModule.SelectedValue.ToString();


                var modules = new ModuleController();
                
                modules.UpdateModuleSetting(ModuleId, "MLSImagesUrl", txtMLSImagesUrl.Text.ToString());
                modules.UpdateModuleSetting(ModuleId, "Town", txtTown.Text.ToString());
                modules.UpdateModuleSetting(ModuleId, "FlexMLSPage", ddlFlexMLSModule.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}