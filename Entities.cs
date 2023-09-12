using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncToolOld
{
    internal class LoginMoca
    {
        public string accessToken { get; set; }
        public DateTime? authTokenExpiresAt { get; set; }
        public string refreshToken { get; set; }
        public DateTime? refreshTokenExpiresAt { get; set; }
    }

    internal class Accommodation
    {
        public Accommodation() { }
        public string type { get; set; }
        public string name { get; set; }
        public List<LabelItem> label { get; set; }
        public string description { get; set; }
        public int? stars { get; set; }
        public string mainPhotoUrl { get; set; }
        public List<string> imagesUrls { get; set; }
        public Address address { get; set; }
        public Location location { get; set; }
        public List<string> amenities { get; set; }
        public List<string> rules { get; set; }
        public int? priceLevel { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string websiteUrl { get; set; }
    }

    internal class LabelItem
    {
        public string lang { get; set; }
        public string value { get; set; }
    }

    internal class Address
    {
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string area { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string countryCode { get; set; }

    }

    internal class Location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
