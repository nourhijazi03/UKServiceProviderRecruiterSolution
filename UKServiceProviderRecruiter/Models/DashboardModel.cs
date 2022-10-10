using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKServiceProviderRecruiter.Models
{
    public class DashboardModel
    {

        public int CandidatesCount { get; set; }
        public int HotelsCount { get; set; }
        public double Income { get; set; }
        public double  Payments { get; set; }

        public String PendingHotelRequestsString { get; set; }
        public String ActiveHotelRequestsString { get; set; }
        public List<HotelRequest> PendingHotelRequests{ get; set; }
        public List<HotelRequest> ActiveHotelRequests { get; set; }




    }
}