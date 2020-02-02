using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.Modules.FlexMLS_OpenHouse.Components;
using DotNetNuke.Common;
using System.Web.UI.HtmlControls;



namespace GIBS.Modules.FlexMLS_OpenHouse
{
    public partial class ViewFlexMLS_OpenHouse : PortalModuleBase, IActionable
    {

        public string _Town = "Chatham";
        public string _StartDate = DateTime.Now.ToShortDateString();
        public string _EndDate = DateTime.Now.AddDays(30).ToShortDateString();
        public string _FlexMLSPage = "";
        public string _MLSImagesUrl = "";
        //MLSImagesUrl
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSettings();

                if (!IsPostBack)
                {
                    

                    LoadTowns();
                    LoadOpenHouses();

                }

                lblTown.Text = _Town.ToString() + " Open House Schedule";
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void LoadOpenHouses()
        {

            try
            {
                

                if (Request.QueryString["town"] != null && Request.QueryString["town"] != "")
                {
                    _Town = Request.QueryString["town"].ToString();
                   
                    GetSeoValues(_Town.ToString());

                }
                
                List<FlexMLS_OpenHouseInfo> items;
                FlexMLS_OpenHouseController controller = new FlexMLS_OpenHouseController();

                items = controller.FlexMLS_GetOpenHouses(_Town.ToString(), _StartDate.ToString(), _EndDate.ToString());


                //bind the data
                ListView1.DataSource = items;
                ListView1.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void LoadSettings()
        {

            try
            {


                if (Settings.Contains("Town"))
                {
                    _Town = Settings["Town"].ToString();
                }

                if (Settings.Contains("FlexMLSPage"))
                {
                    _FlexMLSPage = Settings["FlexMLSPage"].ToString();
                }
                if (Settings.Contains("MLSImagesUrl"))
                {
                    _MLSImagesUrl = Settings["MLSImagesUrl"].ToString();
                }
                //txtMLSImagesUrl

                //// GET MODULE SETTINGS
                //var mc = new ModuleController();
                //var mi = mc.GetModule(1534);
                //var tSettings = mi.ModuleSettings;        
                //var sValue = tSettings["MLSImagesUrl"];

                //// GET TAB MODULE SETTINGS
                //var tmc = new ModuleController();
                //var tmi = tmc.GetTabModule(1534);
                //var tmSettings = tmi.TabModuleSettings;        
                //var smValue = tmSettings["MLSImagesUrl"];
                
              //  lblDebug.Text = sValue.ToString() + " DEBUG " + _FlexMLSModule.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetSeoValues(string _town)
        {

            try
            {
                if (_town.ToString().Length > 0)
                {
                    DotNetNuke.Framework.CDefault GIBSpage = (DotNetNuke.Framework.CDefault)this.Page;
                    GIBSpage.Title = _town.ToString() + " Open House Schedule";
                    GIBSpage.KeyWords = _town.ToString() + ", " + _town.ToString() + " Open House, " + GIBSpage.KeyWords.ToString();
                    GIBSpage.Description = _town.ToString() + " open house tour schedule. " + GIBSpage.Description.ToString();  
                    GIBSpage.Author = "Joseph M Aucoin, GIBS";
                }






            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                     true, false);

                return actions;
            }
        }

        #endregion


        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // address = _StreetNumber + " " + _StreetName + " " + _StreetType;
            Image ListingImage = (Image)e.Item.FindControl("imgListingImage");
          

            // OPEN HOUSE COMMENTS
            HtmlGenericControl OpenHouseCommentsSpan = (HtmlGenericControl)e.Item.FindControl("OpenHouseComments");
            if (DataBinder.Eval(e.Item.DataItem, "OpenHouseComments").ToString().Trim().Length > 2)
            {
                OpenHouseCommentsSpan.Visible = true;
            }
            else
            {
                OpenHouseCommentsSpan.Visible = false;
            }


            string _ListingNumber = DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString();
            string _content = DataBinder.Eval(e.Item.DataItem, "StreetNumber").ToString() + " "
                + DataBinder.Eval(e.Item.DataItem, "StreetName").ToString() + " "
                 + DataBinder.Eval(e.Item.DataItem, "StreetType").ToString();
            if (DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString().Trim().Length >= 1)
            {
                _content += ", Unit " + DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString();
            }
            _content += "<br />" + DataBinder.Eval(e.Item.DataItem, "Village").ToString();
          
            
            HyperLink eLink = (HyperLink)e.Item.FindControl("hyperlinkListingDetail");
            eLink.Text = _content.ToString();
            string _pageName = DataBinder.Eval(e.Item.DataItem, "Address").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + "_" + DataBinder.Eval(e.Item.DataItem, "Village").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + ".aspx";
            string vLink = Globals.NavigateURL(Int32.Parse(_FlexMLSPage.ToString()));
            var result = vLink.Substring(vLink.LastIndexOf('/') + 1);
            vLink = vLink.ToString().Replace(result.ToString(), "tabid/" + _FlexMLSPage.ToString() + "/pg/v/MLS/" + _ListingNumber.ToString() + "/" + _pageName.ToString());
            eLink.NavigateUrl = vLink.ToString();

            //lblDetails
            int FullBaths = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString());
            int HalfBaths = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString());
            int Bedrooms = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString());

            string _details = DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString() + " Beds, ";
            
            if (FullBaths > 0)
            {
                _details += DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString() + " Full";
            }
            
            if (HalfBaths > 0)
            {
                _details += " & " + DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString() + " Half";
            }

            if (HalfBaths + FullBaths > 1)
            {
                _details += " Baths";
            }
            else
            {
                _details += " Bath";
            }

            Label lblDetails = (Label)e.Item.FindControl("lblDetails");
            lblDetails.Text = _details.ToString();






            string checkImage = _MLSImagesUrl.ToString() + _ListingNumber.ToString() + ".jpg";

            if (UrlExists(checkImage.ToString()) == true)
            {
                // ListingImage.ImageUrl = checkImage.ToString();
                ListingImage.ImageUrl = _MLSImagesUrl.ToString() + _ListingNumber.ToString() + ".jpg";

            }
            else if (UrlExists(_MLSImagesUrl.ToString() + _ListingNumber.ToString() + "_1.jpg") == true)
            {
                //
                ListingImage.ImageUrl = _MLSImagesUrl.ToString() + _ListingNumber.ToString() + "_1.jpg";

            }
            else
            {

                ListingImage.ImageUrl = _MLSImagesUrl.ToString() + "NoImage.jpg";

                //     ImageNeeded(Int32.Parse(_ListingNumber.ToString()));
            }



            ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString() + " - " + _content.ToString();
            ListingImage.ToolTip = "MLS " + _ListingNumber.ToString() + " - " + _content.ToString();


        }

        public void LoadTowns()
        {

            try
            {
                //   string year = DateTime.Now.Year.ToString();
               // string town = "";
                

                List<FlexMLS_OpenHouseInfo> items;
                FlexMLS_OpenHouseController controller = new FlexMLS_OpenHouseController();

                items = controller.FlexMLS_GetTownList();

                //bind the data

                RepeaterTowns.DataSource = items;
                RepeaterTowns.DataBind();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void RepeaterTowns_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hp = (HyperLink)e.Item.FindControl("HyperLink1");

                string townname = DataBinder.Eval(e.Item.DataItem, "Town").ToString();
                string _pageName = DataBinder.Eval(e.Item.DataItem, "Town").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + "_Tax_Rates.aspx";

                string vLink = Globals.NavigateURL("View", "Town", townname.ToString());
                vLink = vLink.ToString().Replace("ctl/View/", "");
                vLink = vLink.ToString().Replace("Default.aspx", _pageName.ToString());
                hp.Text = DataBinder.Eval(e.Item.DataItem, "Town").ToString();
                hp.NavigateUrl = vLink.ToString();


            }

        }


        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }



    }
}