using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;

namespace GIBS.Modules.FlexMLS_OpenHouse.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FlexMLS_OpenHouseSettings : ModuleSettingsBase
    {


        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>


        public string Town
        {
            get
            {
                if (Settings.Contains("Town"))
                    return Settings["Town"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "Town", value.ToString());
            }
        }


        public string FlexMLSModule
        {
            get
            {
                if (Settings.Contains("FlexMLSPage"))
                    return Settings["FlexMLSPage"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "FlexMLSPage", value.ToString());
            }
        }

        public string MLSImagesUrl
        {
            get
            {
                if (Settings.Contains("MLSImagesUrl"))
                    return Settings["MLSImagesUrl"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MLSImagesUrl", value.ToString());
            }
        }

        #endregion
    }
}
