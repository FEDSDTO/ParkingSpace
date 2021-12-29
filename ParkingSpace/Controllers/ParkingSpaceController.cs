using ParkingSpace.Models;
using ParkingSpace.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSpace.Controllers
{
    public class ParkingSpaceController : Controller
    {
        private GiftsEntities _dbG = new GiftsEntities();
        private FEDSEntities _dbF = new FEDSEntities();
        private ParkingLotInfoEntities _dbP = new ParkingLotInfoEntities();
        PublicService _Pub = new PublicService();

        // GET: ParkingSpace
        public ActionResult Index(string userNo)
        {
            if (Request.Cookies[".ASPXAUTH"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //登錄者分店
            string mallId = _dbG.UserInfo_Main.Where(o => o.UserNo == userNo).FirstOrDefault().MallId;
            var userMall = _dbG.Mall.Where(o => o.MallId == mallId).FirstOrDefault();
            //分店列表
            var mallList = _dbG.Mall.Where(o => o.IsUse == true && o.MallId != "10").Select(o => new { Id = o.MallId, Name = o.Name }).ToList();

            ViewBag.Mall = userMall;
            ViewBag.User = userNo;
            ViewBag.MallList = _Pub.DDLType(mallList, "Id", "Name", string.Empty); ;


            return View();
        }

        [HttpPost]
        public ActionResult LoadSpace(string car, string motor, string userNo, int selectMall)
        {
            var _user = _dbG.UserInfo_Main.Where(o => o.UserNo == userNo).FirstOrDefault();
            string mallId = _user.MallId;
            int user_Id = _user.Id;
            try
            {
                ParkinglotInfo _p = _dbF.ParkinglotInfo.Where(o => o.MallId == selectMall).FirstOrDefault();
                if(_p != null)
                {
                    if(!string.IsNullOrEmpty(car))
                    {
                        _p.CarParkinglot = car;
                    }

                    if (!string.IsNullOrEmpty(motor))
                    {
                        _p.MotocycleParkinglot = motor;
                    }
                }
                else
                {
                    var _result = new
                    {
                        IsSuccess = false,
                        Message = "此分店不提供輸入剩餘車位服務"
                    };
                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(_result), "application/json");
                }
                
                _dbP.System_Log.Add(new System_Log
                {
                    Controller = "ParkingSpace",
                    Event = "編輯者:" + userNo + ", 汽車位數:" + car + ", 機車位數:" + motor,
                    ModifyUser = user_Id,
                    ModifyDate = DateTime.Now
                });

                _dbF.SaveChanges();
                _dbP.SaveChanges();
                
                var result = new
                {
                    IsSuccess = true,
                };
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result), "application/json");
            }
            catch(Exception ex)
            {
                _dbP.SystemError_Log.Add(new SystemError_Log
                {
                    Controller = "ParkingSpace",
                    Event = ex.Message,
                    ModifyUser = user_Id,
                    ModifyDate = DateTime.Now
                });
                _dbP.SaveChanges();

                var result = new
                {
                    IsSuccess = false,
                    Message = "聯絡技術單位"
                };
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result), "application/json");
            }    
        }
    }
}