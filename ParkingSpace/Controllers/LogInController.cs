﻿using ParkingSpace.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.Security;

namespace ParkingSpace.Controllers
{
    public class LogInController : Controller
    {
        private GiftsEntities _dbG = new GiftsEntities();
        // GET: LogIn

        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "UpdateMember");
            }
            else
            {
                //若為cookie到期自動登出時，轉頁前先清除cookie
                FormsAuthentication.SignOut();
                return View();
            }
        }

        [HttpPost]
        public ActionResult LogIn(string userNo, string userPassword)
        {

            string ePassword = SHA512Encryption(userPassword);
            //Response.Cookies["userNo"].Value = userNo;
            //Response.Cookies["userPassword"].Value = userPassword;
            var result = _dbG.UserInfo_Main.Where(o => o.MallId == "32" || o.MallId == "37" || o.MallId == "10").Select(o => new { id = o.UserNo, pwd = o.Password, mall = o.MallId }).ToList();
            var fin = result.Where(o => o.id == userNo && o.pwd == ePassword).FirstOrDefault();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userNo, DateTime.Now, DateTime.Now.AddDays(1), false, "", FormsAuthentication.FormsCookiePath);
            string enTicket = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket)); //將資料存入cookie

            if (fin != null)
            {
                var rresult = new
                {
                    IsSuccess = true,
                    MallId = fin.mall,
                    UserNo = fin.id
                };
                return Json(rresult, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "UpdateMember");
                //return View("Index", "UpdateMember");
            }
            else
            {
                var rresult = new
                {
                    IsSuccess = false,
                };
                //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(rresult), "application/json");
                return Json(rresult, JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// 將欲加密的文字以SHA512的方式加密
        /// </summary>
        /// <param name="EncryptionText">加密文字</param>
        /// <returns></returns>
        public string SHA512Encryption(string EncryptionText)
        {
            //建立一個SHA512
            SHA512 EncryptionSHA = new SHA512CryptoServiceProvider();
            //將字串轉成byte[]
            byte[] source = Encoding.Default.GetBytes(EncryptionText);
            //進行SHA512加密
            byte[] crypo = EncryptionSHA.ComputeHash(source);

            //return Convert.ToBase64String(crypo);
            return BitConverter.ToString(crypo).Replace("-", string.Empty).ToLower();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}