using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _FetchStudents()
        {
            List<Student> student;
            using (var ctx = new StudentContext())
            {
                student = await ctx.Students.Where(s => s.isDeleted == false).ToListAsync();
            }
            return PartialView(student);
        }

        [HttpPost]
        public async Task<JsonResult> AddStudent([Bind(Include = "StudentName, Email, Address")]Student student)
        {
           
            try
            {
                using(var ctx = new StudentContext())
                {
                    ctx.Students.Add(student);
                    await ctx.SaveChangesAsync();

                    return Json(new { message = "Success"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {message = ex.Message }); 
            }
        }

        [HttpPost]
        public ActionResult EditStudent([Bind(Include = "ID, StudentName, Email, Address")]Student student)
        {           
            if(ModelState.IsValid)
            {
                using (var ctx = new StudentContext())
                {
                    ctx.Entry(student).State = EntityState.Modified;
                    ctx.SaveChanges();

                    return Json(new { data = student , message = "success" });
                }
            }
            else
            {
                return Json(new { data = student, message = "Something went wrong, please try again" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteStudent(int? id)
        {
            Student student;
            using(var ctx = new StudentContext())
            {
                student = await ctx.Students.FindAsync(id);
                ctx.Students.Remove(student);
                await ctx.SaveChangesAsync();


                return Json(new { message = "Student Removed" });

            }
        }

       
    }
}