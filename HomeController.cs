using Crud_Operation_1.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud_Operation_1.Controllers
{
    public class HomeController : Controller
    {
        Student_DetailsEntities1 sd = new Student_DetailsEntities1();
        dynamic model = new ExpandoObject();
        string msg;
        // GET: Home

       
       public ActionResult Index()
        {
            List<StudentMaster> Stm = sd.StudentMasters.ToList(); 
            return View(Stm);
        }

        


        public JsonResult BindTeacherDDL()
        {
            sd.Configuration.ProxyCreationEnabled = false;
            List<TeacherMaster> tm = sd.TeacherMasters.ToList();
            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewStudent(StudentMaster stm)
        {   
            try
            {
                sd.StudentMasters.Add(stm);
                sd.SaveChanges();
            }
            catch (Exception e)
            {
                msg = "something Went Wrong";
            }
            ViewBag.Msg = msg;
            return Redirect("/");
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                StudentMaster stm = sd.StudentMasters.Find(Id);
                sd.StudentMasters.Remove(stm);
                sd.SaveChanges();
            }
            catch
            {
                msg = "Sorry Unable To Delete";
            }
            TempData["Msg"] = msg;
            return RedirectToAction("/");
        }

        
        public JsonResult GetStudentDetailsToUpdate(int Id)
        {
            try
            {
                sd.Configuration.ProxyCreationEnabled = false;
                StudentMaster st = sd.StudentMasters.Find(Id);
                
                return Json(st,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult UpdateDetails(StudentMaster stm)
        {
            StudentMaster stu = sd.StudentMasters.Find(stm.id);
            stu.First_Name = stm.First_Name;
            stu.last_Name = stm.last_Name;
            stu.Gender = stm.Gender;
            stu.Date_of_Birth = stm.Date_of_Birth;
            stu.Teacher_Id = stm.Teacher_Id;
            try
            {
                sd.Entry(stu);
                sd.SaveChanges();
                msg = "Record Updated Successfully";
            }
            catch
            {
                msg = "Unable To Update try After Some Time";
            }
            TempData["Update"] = msg;
            return RedirectToAction("/");
        }
    }
}