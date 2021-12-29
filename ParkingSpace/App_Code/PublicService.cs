using ParkingSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace ParkingSpace.App_Code
{
    public class PublicService
    {
        /// <summary>
        /// 產生下拉式選單
        /// </summary>
        /// <param name="_object">DB物件</param>
        /// <param name="_text">名稱</param>
        /// <param name="_value">值</param>
        /// <returns>DDL</returns>
        public SelectList DDLType(IEnumerable<object> _object, string _value, string _text, string _selectd)
        {
            SelectList _selectList = new SelectList(_object, _value, _text, _selectd);

            return _selectList;
        }
    }
}