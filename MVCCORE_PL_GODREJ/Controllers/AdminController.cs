using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCCORE_PL_GODREJ.Models;
using Newtonsoft.Json;

namespace MVCCORE_PL_GODREJ.Controllers
{
    public class AdminController : Controller
    {

        IConfiguration _configuration;
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminViewModel admin)
        {

            AdminViewModel currentAdmin = new AdminViewModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(admin),
                            Encoding.UTF8, "application/json");


                //using (var response = await httpClient.PostAsync
                //                (_configuration["BaseURL"], content))
                //{
                //    string apiResponse = await response.Content.ReadAsStringAsync();
                //    newEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>
                //            (apiResponse);
                //}
                using (var response = await httpClient.PostAsync
                                    (_configuration["BaseURL1"], content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        currentAdmin = JsonConvert.DeserializeObject<AdminViewModel>
                                (apiResponse);
                        //ViewBag.Email = admin.Email;
                        HttpContext.Session.SetString("userId", admin.Email);

                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        currentAdmin = null;
                        ModelState.AddModelError(string.Empty,
                        "Invalid Email Address Or Password ..!");

                    }
                }
            }
            return View(admin);


        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
