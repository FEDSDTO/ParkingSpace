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
    
    public partial class UserInfo_Main
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string Name { get; set; }
        public string MallId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> PositionId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int LoggedInFail { get; set; }
        public Nullable<System.DateTime> ChangPwdTime { get; set; }
        public bool IsLoggedIn { get; set; }
        public Nullable<System.DateTime> LoggedInTime { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<int> ModifyUser { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public bool IsUse { get; set; }
    }
}