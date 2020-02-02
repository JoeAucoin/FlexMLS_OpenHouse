using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.Modules.FlexMLS_OpenHouse.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;
        private string FlexMLSConnectionString;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            FlexMLSConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FlexMLS"].ConnectionString;  

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        public override IDataReader FlexMLS_GetOpenHouses(string town, string startDate, string endDate)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_OpenHouseGetList"), town, startDate, endDate);
        }


        public override IDataReader FlexMLS_GetTownList()
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_OpenHouse_GetTowns"));
        }		



        public override IDataReader GetFlexMLS_OpenHouse(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetFlexMLS_OpenHouse"), moduleId, itemId);
        }

        public override void AddFlexMLS_OpenHouse(int moduleId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("AddFlexMLS_OpenHouse"), moduleId, content, userId);
        }

        public override void UpdateFlexMLS_OpenHouse(int moduleId, int itemId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("UpdateFlexMLS_OpenHouse"), moduleId, itemId, content, userId);
        }

        public override void DeleteFlexMLS_OpenHouse(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteFlexMLS_OpenHouse"), moduleId, itemId);
        }

        #endregion
    }
}
