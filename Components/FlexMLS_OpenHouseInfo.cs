using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.Modules.FlexMLS_OpenHouse.Components
{
    public class FlexMLS_OpenHouseInfo
    {
        //private vars exposed thro the
        //properties
        private int moduleId;
        private int itemId;
        private string content;
        private int createdByUser;
        private DateTime createdDate;
        private string createdByUserName = null;
        private string _Address;
        private string _ListingNumber;
        private string _ListingAgentName;
        private string _ListingOfficeName;
        private int _HourDiff;
        private DateTime _EventStart;
        private DateTime _EventEnd;
        private string _OpenHouseComments;
        private double _ListingPrice;
        private string _StreetName;
        private string _StreetNumber;
        private string _StreetType;
        private string _UnitNumber;
        private string _Town;
        private string _Village;
        private int _FullBaths;
        private int _HalfBaths;
        private int _Bedrooms;
        private float _Latitude;
        private float _Longitude;
        private string _Complex;


        /// <summary>
        /// empty cstor
        /// </summary>
        public FlexMLS_OpenHouseInfo()
        {
        }


        #region properties

        public string ListingNumber
        {
            get { return _ListingNumber; }
            set { _ListingNumber = value; }
        }

        public string ListingAgentName
        {
            get { return _ListingAgentName; }
            set { _ListingAgentName = value; }
        }



        public string ListingOfficeName
        {
            get { return _ListingOfficeName; }
            set { _ListingOfficeName = value; }
        }

        public int HourDiff
        {
            get { return _HourDiff; }
            set { _HourDiff = value; }
        }


        public DateTime EventStart
        {
            get { return _EventStart; }
            set { _EventStart = value; }
        }

        public DateTime EventEnd
        {
            get { return _EventEnd; }
            set { _EventEnd = value; }
        }

        //_OpenHouseComments
        public string OpenHouseComments
        {
            get { return _OpenHouseComments; }
            set { _OpenHouseComments = value; }
        }

        public double ListingPrice
        {
            get { return _ListingPrice; }
            set { _ListingPrice = value; }
        }

        public string StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }

        public string StreetNumber
        {
            get { return _StreetNumber; }
            set { _StreetNumber = value; }
        }

        public string StreetType
        {
            get { return _StreetType; }
            set { _StreetType = value; }
        }

        public string UnitNumber
        {
            get { return _UnitNumber; }
            set { _UnitNumber = value; }
        }

        public string Town
        {
            get { return _Town; }
            set { _Town = value; }
        }

        public string Village
        {
            get { return _Village; }
            set { _Village = value; }
        }

        public int FullBaths
        {
            get { return _FullBaths; }
            set { _FullBaths = value; }
        }

        public int HalfBaths
        {
            get { return _HalfBaths; }
            set { _HalfBaths = value; }
        }

        public int Bedrooms
        {
            get { return _Bedrooms; }
            set { _Bedrooms = value; }
        }

        public float Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        public float Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        public string Complex
        {
            get { return _Complex; }
            set { _Complex = value; }
        }



        public string Address
        {

            get
            {
                if (_Address == null)
                {

                    _Address = _StreetNumber + " " + _StreetName + " " + _StreetType;
                }

                return _Address;
            }

        }


        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }


        public string CreatedByUserName
        {
            get
            {
                if (createdByUserName == null)
                {
                    int portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
                    UserController controller = new UserController();
                    UserInfo user = controller.GetUser(portalId, createdByUser);
                    createdByUserName = user.DisplayName;
                }

                return createdByUserName;
            }
        }





        #endregion
    }
}
