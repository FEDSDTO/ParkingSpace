//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkingSpace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParkinglotInfo
    {
        public int Id { get; set; }
        public int MallId { get; set; }
        public string CarParkinglot { get; set; }
        public string MotocycleParkinglot { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal GPS_X { get; set; }
        public decimal GPS_Y { get; set; }
        public byte EntityStatus { get; set; }
        public string NearbyUrl { get; set; }
    }
}