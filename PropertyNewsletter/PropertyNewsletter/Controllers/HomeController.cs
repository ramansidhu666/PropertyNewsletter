using PropertyNewsletter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PropertyNewsletter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        [HttpPost]
        public ActionResult LogoImage(NewsletterModel model, HttpPostedFileBase Logofile, HttpPostedFileBase Imgfile)
        {
            var logopath = "";
            var Photopath = "";
            if (Logofile != null)
            {

                //Save the photo in Folder
                var fileExt = Path.GetExtension(Logofile.FileName);
                string fileName = Guid.NewGuid() + fileExt;
                var subPath = Server.MapPath("~/NewsLetterImages");

                //Check SubPath Exist or Not
                if (!Directory.Exists(subPath))
                {
                    Directory.CreateDirectory(subPath);
                }
                //End : Check SubPath Exist or Not

                var path = Path.Combine(subPath, fileName);
                Logofile.SaveAs(path);
                var URL = ConfigurationManager.AppSettings["LiveURL"].ToString();
                logopath = URL + "/NewsLetterImages/" + fileName;
            }
            if (Imgfile != null)
            {

                //Save the photo in Folder
                var fileExt = Path.GetExtension(Imgfile.FileName);
                string fileName = Guid.NewGuid() + fileExt;
                var subPath = Server.MapPath("~/NewsLetterImages");

                //Check SubPath Exist or Not
                if (!Directory.Exists(subPath))
                {
                    Directory.CreateDirectory(subPath);
                }
                //End : Check SubPath Exist or Not

                var path = Path.Combine(subPath, fileName);
                Imgfile.SaveAs(path);
                var URL = ConfigurationManager.AppSettings["LiveURL"].ToString();
                Photopath = URL + "/NewsLetterImages/" + fileName;
            }
            SendMailToUser(logopath, Photopath, model);
            return Json("success");
        }

        [HttpPost]
        public JsonResult UserImage(NewsletterModel model, HttpPostedFileBase file)
        {
            JsonResult JsonResult = new JsonResult();
            return JsonResult;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public void SendMailToUser(string logopath, string Photopath, NewsletterModel model)
        {
            try
            {
                //Send mail
                MailMessage mail = new MailMessage();
              
                string FromEmailID = WebConfigurationManager.AppSettings["FromEmailID"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                string ToEmailID = WebConfigurationManager.AppSettings["ToEmailID"];
                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
                int _Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString());
                Boolean _UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseDefaultCredentials"].ToString());
                Boolean _EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"].ToString());
                mail.To.Add(new MailAddress(ToEmailID));
                mail.From = new MailAddress(FromEmailID);
                mail.Subject = "News Letter";
                //string LogoPath = Common.GetURL() + "/images/logo.png";
                string msgbody = "";
               msgbody += "<div class='col-md-8'>";

                msgbody += "<div class='newsltr_bg'>";
                msgbody += "<div style='float:left; width:98%; background-color:#231f20; border-bottom:8px solid #eda320; padding:6px 0;'>";
                msgbody += " <div style='float:left; width:29%; padding:6px; text-align:center;'>";
                msgbody += "<img src='" + logopath + "' alt='' title='' style='width:180px;'/>";
                msgbody += "</div>";
                msgbody += "<div style='float:left; width:68%; padding:6px;'>";
                msgbody += " <h2 style='float:left; width:100%; font-size:26px; font-family:Arial, Helvetica, sans-serif; font-weight:bold; color:#FFF; margin:6px 0 0;'>Tittle of the Newsletter Section</h2>";
                msgbody += "<div style='float:left; width:100%; margin:12px 0 0;'>";
                msgbody += "<h2 style='float:left; width:100%; font-size:18px; font-family:Arial, Helvetica, sans-serif; margin: 0px; color:#FFF;'>AJ Shah</h2>";
                msgbody += "<p style='float:left; width:100%; font-size:18px; font-family:Arial, Helvetica, sans-serif; margin: 0px; color:#FFF;'>Broker Of Record / Owner</p>";
                msgbody += "<p style='float:left; width:100%; font-size:16px; font-family:Arial, Helvetica, sans-serif; color:#c48820; margin:5px 0 2px 0;'>HomeLife/Miracle Realty Ltd., Brokerage*</p>";
                msgbody += "<p style='float:left; width:100%; margin: 0px;'>";
                msgbody += " <span style='float:left; font-size:15px; color:#c48820;'>Phone:</span>";
                msgbody += "<span style='float:left; font-size:14px; color:#FFF; margin:0 10px 0 4px;'>416-747-9777</span>";
                msgbody += "<span style='float:left; font-size:15px; color:#c48820; margin: 0 6px 0 1px'>Email:</span>";
                msgbody += "<span style='float:left; font-size:14px; color:#FFF;'>homekey123@yahoo.com</span>";
                msgbody += "</p>";
                msgbody += "<p style='float:left; width:100%; margin: 0px;'>";
                msgbody += "<span style='float:left; font-size:15px; color:#c48820;'>Address:</span>";
                msgbody += "<span style='float:left; font-size:14px; color:#FFF; margin:0 10px 0 4px;'>5010 Steeles Avenue West , Suite 11A , Toronto, Ontario, M9V 5C6</span>";
                msgbody += "</p>";
                msgbody += "<p style='float:left; width:100%; font-size:22px; font-family:Arial, Helvetica, sans-serif; color:#c48820; margin:5px 0 0;'>www.homelifemiracle.com</p>";
                msgbody += "</div>";
                msgbody += "</div>";
                msgbody += "</div>";
                msgbody += "<div style='float:left; width:100%;'>";
                msgbody += "<div style='float:left; width:29%; padding:6px; padding:12px 4px; background:#026f52; height:750px;'>";
                msgbody += "<img style='width: 50%;'  src='http://74.208.69.145:75//NewsLetterImages/img(1).jpg' alt='' title='' >";
                msgbody += "<p style='float:left; width:95%; color:white; font-size:14px; line-height:22px; text-align:justify; padding:4px 10px;margin:0px'>";
                msgbody += "400 CENTRAL PARK WEST 5W</p>";
                msgbody += "<p style='float:left; width:95%; color:white; font-size:14px; line-height:22px; text-align:justify; padding:4px 10px;margin:0px'>";
                msgbody += "This spacious and sunny renovated 1-bedroom/1 bath apartment is ready for you to move in. Enjoy Central Park views as you gaze out the windows or as you relax on the huge outdoor terrace. The brand new kitchen features granite counter tops, custom cabinets and top-of-the-line appliances. The large living room is wonderful for entertaining and there is also a separate dining area. The bedroom is full-sized and can accommodate any type of furniture. </p>";
                msgbody += "<p style='float:left; width:95%; color:white; font-size:14px; line-height:22px; text-align:justify; padding:4px 10px;margin:0px'>";
                msgbody += " There is great closet space (including two walk-ins), there are wood floors throughout, and each room has its own through-the-wall air condit...ioner. 400 Central Park West is a full-service Condominium building with a 24-hour Concierge, a fitness center, a modern laundry, storage, a bike room, a playground, outdoor parking, and is near to all transportation and shopping. Pets are permitted and subletting is allowed.  </p>";
                msgbody += "</div>";
                msgbody += "<div style='float:left; width:68%; padding:3px 0 0 3px;'>";
                msgbody += "<div style='float:left; width:100%;'>";
                msgbody += "<img src='"+Photopath+"' style='width:100%; height:420px;' 'alt='' title=''/>";
                msgbody += "</div>";
                msgbody += "<div style='float:left; width:100%; margin-top:3px; background-color:#f0f0f0;'>";
                msgbody += "<p style='float:left; width:95%; color:#3d3d3d; font-size:13px; line-height:22px; text-align:justify; padding:4px 10px; margin: 0px;'>";
                msgbody += "This charming mint one bedroom penthouse with a wrap terrace and a WBFP is located on a tree lined block and is steps from Central Park. Take the elevator up to the 10th floor and walk up one flight of stairs to a semi private landing. When entering this special home you will find light surrounding you from four exposures. The south facing living room boasts a wood burning fireplace and French doors that open to the wrap terrace, here you will be able to entertain your friends surrounded by lush plantings or sit back and enjoy your morning coff...ee. </p>";
                msgbody += "<p style='float:left; width:95%; color:#3d3d3d; font-size:13px; line-height:22px; text-align:justify; padding:4px 10px; margin:0px;'>";
                msgbody += "The oversized windowed kitchen, with south, north and eastern exposures has granite counter tops, stainless appliances (including a Samsung French door refrigerator/freezer, Bosch dishwasher, electric stove top and oven.) There is also an oversized dining area with access to the terrace from a French door. The newly renovated windowed bathroom sits just outside the bedroom and is finished with white Carrera marble, a pedestal sink as well as a soaking tub/shower. The bedroom has south, west and northern exposures and an entrance to the terrace. There are beautifully refinished hard wood floors throughout, as well as through wall A/C,.</p>";
                msgbody += "<p style='float:left; width:95%; color:#3d3d3d; font-size:13px; line-height:22px; text-align:justify; padding:4px 10px; margin:0px;'>";
                msgbody += "recessed and cove lighting, built in book shelves and 17 windows! The Morleigh is a prewar, elevator cooperative with a live in super and has a video intercom, laundry room, bike room and storage. This rare gem in located in the heart of the UWS near Central Park, Lincoln Center, public transportation and great restaurants. Pied-a-terre's allowed but sorry no pets allowed.  </p>";

                msgbody += "</div>";

                msgbody += "</div>";
                msgbody += "</div>";
                msgbody += "</div>";

                msgbody += "</div>";
                

                msgbody += "</div>";
                msgbody += "</div>";
                mail.Body = msgbody;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = "smtp.gmail.com"; //_Host;
                smtp.Port = _Port;
                //smtp.UseDefaultCredentials = _UseDefaultCredentials;
                smtp.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);// Enter senders User name and password
                smtp.EnableSsl = _EnableSsl;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.ToString();
            }
        }
    }
}
