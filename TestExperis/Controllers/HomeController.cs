using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestExperis.Data;
using TestExperis.Models;
using TextExperis.Infrastructure.Data;

namespace TestExperis.Controllers
{
    public class HomeController : Controller
    {
        string UserApiUrl = "";
        public HomeController()
        {
            UserApiUrl = ConfigurationManager.AppSettings["UserApiUrl"];
        }
        public async Task<ActionResult> Index(SelectedOptions selections)
        {
            ViewBag.SelectedCategory = 0;
            Technology technology = null;
            List<User> users = new List<User>();
            bool IsEven = false;

            if (selections.Category!= null)
            {
                ViewBag.SelectedCategory = selections.Category.Category_Id;
            }
            if(selections.Technologies == null)
            {
                selections.Technologies = new List<long>();
            }
            if (ViewBag.SelectedTechnologies == null)
            {
                ViewBag.SelectedTechnologies = new List<long>();
            }
            try
            {
                using (TestDataContext context = new TestDataContext())
                {
                    ViewBag.Categories = context.Category.ToList();

                    if (selections.Category != null && selections.Category.Category_Id > 0)
                    {                         
                        ViewBag.Technologies = context.Technology.Where(x => x.Technology_Category_Id == selections.Category.Category_Id).ToList(); 
                    }
                    foreach (var tech in selections.Technologies)
                    {
                        technology = context.Technology.Where(x => x.Technology_Id == tech).FirstOrDefault();
                        IsEven = technology.Technology_CodeId % 2 == 0;  users = (await UsersApi.GetList<User>(UserApiUrl, "users")).Where(x => IsEven ? x.id % 2 == 0 : x.id % 2 != 0).ToList();
                        
                    }

                    ViewBag.SelectedTechnologies = selections.Technologies;
                }                
            }
            catch(Exception ex)
            {

            }

            return View(users);
        }

        void LoadInterViewModes()
        {
            using (TestDataContext context = new TestDataContext())
            {
                ViewData["InterviewModes"] = context.InterviewMode.ToList();
            }
        }

        [HttpPost]
        public async Task<ActionResult> PerformSchedule(Interview interview)
        {
            LoadInterViewModes();
            List<long> SelectedUsers = new List<long>();
            SelectedUsers = (List<long>)TempData["SelectedUsers"];

            if(interview.Interview_Category_Id >0 )
                ViewData["SelectedCategory"] = interview.Interview_Category_Id;

            if (interview.Interview_RecruiterUser_Id <= 0)
            {
                ViewBag.Interview_RecruiterUser_Id = 1;
            }
            if (interview.Interview_Applicant_Id <= 0 && SelectedUsers != null && SelectedUsers.Count>0)
            {
                ViewBag.Interview_RecruiterUser_Id = SelectedUsers.FirstOrDefault();
            }
            if(interview.Interview_Category_Id <= 0 && TempData["SelectedCategory"] != null)
            {
                interview.Interview_Category_Id = (long)TempData["SelectedCategory"];
            }
            if (!ModelState.IsValid || interview.Interview_StartDate <= DateTime.Now)
            {
                if (interview.Interview_StartDate <= DateTime.Now)
                    ViewBag.Result = "La fecha de la entrevista debe ser mayor a la fecha actual";
                
                return View(interview);
            }

            //ViewBag.Model.InterviewModes = context.InterviewMode.ToList();


            try
            {
                Applicant applicant = null;
                User user = null;
                DBStoredProcContext procContext = new DBStoredProcContext();
                bool result = false;
                if (!ModelState.IsValid)
                {
                    ViewBag.Result = "No se registro la información, favor validar los datos.";
                    LoadInterViewModes();
                    return View(interview);
                }

                
                using (TestDataContext context = new TestDataContext())
                {
                    applicant = context.Applicant.Where(x => x.id == interview.Interview_Applicant_Id).FirstOrDefault();
                    if (applicant == null)
                    {
                        user = (await UsersApi.GetList<User>(UserApiUrl, "users")).Where(x => x.id == interview.Interview_Applicant_Id).FirstOrDefault();
                        if (user == null) return View(interview);
                        applicant = new Applicant
                        {
                            address_city = user.address.city,
                            address_geo_lat = user.address.geo.lat,
                            address_geo_lng = user.address.geo.lng,
                            address_street = user.address.street,
                            address_suite = user.address.suite,
                            address_zipcode = user.address.zipcode,
                            company_bs = user.company.bs,
                            company_catchPhrase = user.company.catchPhrase,
                            company_name = user.company.name,
                            email = user.email,
                            id = user.id,
                            name = user.name,
                            phone = user.phone,
                            website = user.website,
                            username = user.username,
                        };
                        context.Applicant.Add(applicant);
                        result = await context.SaveChangesAsync() > 0;
                    }
                    var str = procContext.sp_CheckInterviewIntersections(interview.Interview_StartDate, interview.Interview_Duration).ToList();

                    if (str.Count > 0)
                    {
                        ViewBag.Result = $"No es posible agendar la entrevista porque se cruza con una entrevista ya programada '{str.FirstOrDefault().Interview_StartDate}' + {str.FirstOrDefault().Interview_Duration} Min";
                        LoadInterViewModes();
                        return View(interview);
                    }

                    context.Interview.Add(interview);
                    result = await context.SaveChangesAsync() > 0;
                    if(result)
                        SelectedUsers.Remove(interview.Interview_Applicant_Id);
                    if (SelectedUsers.Count > 0)                {
                        

                        TempData["SelectedUsers"] = SelectedUsers;
                        TempData["SelectedCategory"] = interview.Interview_Category_Id;
                        ViewBag.Success = $"Se ha programado la entrevista para el dia {interview.Interview_StartDate} correctamente... continuando con la siguiente entrevista";
                        return RedirectToAction(nameof(PerformSchedule));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

            LoadInterViewModes();
            return View(interview);
        }
        public async Task<ActionResult> PerformSchedule(long SelectedCategory = 0, List<long> SelectedUsers = null)
        {

            try
            {
                Interview interview = new Interview();
                if (SelectedCategory <= 0)
                {
                    SelectedCategory = (long)TempData["SelectedCategory"];
                }
                if (SelectedUsers == null)
                {
                    SelectedUsers = (List<long>)TempData["SelectedUsers"];
                }
                using (TestDataContext context = new TestDataContext())
                {
                    ViewBag.Model = interview;
                    //ViewBag.Model.InterviewModes = context.InterviewMode.ToList();
                    ViewData["InterviewModes"] = context.InterviewMode.ToList();
                    ViewData["SelectedCategory"] = SelectedCategory;
                    
                    TempData["SelectedUsers"] = SelectedUsers;
                    if (SelectedCategory <= 0 && SelectedUsers == null && SelectedUsers.Count <= 0)
                        return View();

                    
                    ViewBag.SelectedUsers = SelectedUsers;
                    ViewBag.Interview_Category_Id = SelectedCategory;
                    ViewBag.Interview_Applicant_Id = SelectedUsers.FirstOrDefault();
                    ViewBag.Interview_RecruiterUser_Id = 1;
                    ViewBag.CurrentApplicantName = (await UsersApi.GetList<User>(UserApiUrl, "users")).Where(x=> x.id == SelectedUsers.FirstOrDefault()).FirstOrDefault().name;
                    
                }
            }
            catch (Exception ex)
            {
                var dta = ex.InnerException;
            }

            return View();
        }
        public ActionResult UserDetail()
        {
            //Perform action here

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}