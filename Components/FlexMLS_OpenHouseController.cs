using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace GIBS.Modules.FlexMLS_OpenHouse.Components
{
    public class FlexMLS_OpenHouseController //: IPortable   //ISearchable,
    {

        #region public method

        /// <summary>
        /// Gets all the FlexMLS_OpenHouseInfo objects for items matching the this moduleId
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<FlexMLS_OpenHouseInfo> FlexMLS_GetOpenHouses(string town, string startDate, string endDate)
        {
            return CBO.FillCollection<FlexMLS_OpenHouseInfo>(DataProvider.Instance().FlexMLS_GetOpenHouses(town, startDate, endDate));
        }

        public List<FlexMLS_OpenHouseInfo> FlexMLS_GetTownList()
        {
            return CBO.FillCollection<FlexMLS_OpenHouseInfo>(DataProvider.Instance().FlexMLS_GetTownList());
        }


        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public FlexMLS_OpenHouseInfo GetFlexMLS_OpenHouse(int moduleId, int itemId)
        {
         //   return (FlexMLS_OpenHouseInfo)CBO.FillObject(DataProvider.Instance().GetFlexMLS_OpenHouse(moduleId, itemId), typeof(FlexMLS_OpenHouseInfo));
            return CBO.FillObject<FlexMLS_OpenHouseInfo>(DataProvider.Instance().GetFlexMLS_OpenHouse(moduleId, itemId));
        }


        /// <summary>
        /// Adds a new FlexMLS_OpenHouseInfo object into the database
        /// </summary>
        /// <param name="info"></param>
        public void AddFlexMLS_OpenHouse(FlexMLS_OpenHouseInfo info)
        {
            //check we have some content to store
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().AddFlexMLS_OpenHouse(info.ModuleId, info.Content, info.CreatedByUser);
            }
        }

        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void UpdateFlexMLS_OpenHouse(FlexMLS_OpenHouseInfo info)
        {
            //check we have some content to update
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().UpdateFlexMLS_OpenHouse(info.ModuleId, info.ItemId, info.Content, info.CreatedByUser);
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFlexMLS_OpenHouse(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteFlexMLS_OpenHouse(moduleId, itemId);
        }


        #endregion

        #region ISearchable Members

        /// <summary>
        /// Implements the search interface required to allow DNN to index/search the content of your
        /// module
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo modInfo)
        //{
        //    SearchItemInfoCollection searchItems = new SearchItemInfoCollection();

        //    List<FlexMLS_OpenHouseInfo> infos = GetFlexMLS_OpenHouses(modInfo.ModuleID);

        //    foreach (FlexMLS_OpenHouseInfo info in infos)
        //    {
        //        SearchItemInfo searchInfo = new SearchItemInfo(modInfo.ModuleTitle, info.Content, info.CreatedByUser, info.CreatedDate,
        //                                            modInfo.ModuleID, info.ItemId.ToString(), info.Content, "Item=" + info.ItemId.ToString());
        //        searchItems.Add(searchInfo);
        //    }

        //    return searchItems;
        //}

        #endregion

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        //public string ExportModule(int moduleID)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    List<FlexMLS_OpenHouseInfo> infos = GetFlexMLS_OpenHouses(moduleID);

        //    if (infos.Count > 0)
        //    {
        //        sb.Append("<FlexMLS_OpenHouses>");
        //        foreach (FlexMLS_OpenHouseInfo info in infos)
        //        {
        //            sb.Append("<FlexMLS_OpenHouse>");
        //            sb.Append("<content>");
        //            sb.Append(XmlUtils.XMLEncode(info.Content));
        //            sb.Append("</content>");
        //            sb.Append("</FlexMLS_OpenHouse>");
        //        }
        //        sb.Append("</FlexMLS_OpenHouses>");
        //    }

        //    return sb.ToString();
        //}

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "FlexMLS_OpenHouses");

            foreach (XmlNode info in infos.SelectNodes("FlexMLS_OpenHouse"))
            {
                FlexMLS_OpenHouseInfo FlexMLS_OpenHouseInfo = new FlexMLS_OpenHouseInfo();
                FlexMLS_OpenHouseInfo.ModuleId = ModuleID;
                FlexMLS_OpenHouseInfo.Content = info.SelectSingleNode("content").InnerText;
                FlexMLS_OpenHouseInfo.CreatedByUser = UserID;

                AddFlexMLS_OpenHouse(FlexMLS_OpenHouseInfo);
            }
        }

        #endregion
    }
}
