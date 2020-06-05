using System;
using System.Collections.Generic;
using System.IO;
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
    public class EmployeeController : Controller
    {
      

        IConfiguration _configuration;
        //DI 
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeViewModel> emplist = new List<EmployeeViewModel>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(_configuration["BaseURL"]))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emplist = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(apiResponse);
                }
                   
            }
                return View(emplist);
        }
     
        public async Task<IActionResult> Details(int id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration["BaseURL"]
                    + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(apiResponse);
                }
            }
            return View(employee);
        }


         // POST: Employees/Edit/5
        //[Consumes("application/json")]
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            EmployeeViewModel updatedEmployee = new EmployeeViewModel();
            using (var httpClient = new HttpClient())
            {
                //var content = new MultipartFormDataContent();
                //content.Add(new StringContent(employee.EmployeeId.ToString()), 
                //                "EmployeeId");
                //content.Add(new StringContent(employee.FirstName), "FirstName");
                //content.Add(new StringContent(employee.LastName), "LastName");
                //content.Add(new StringContent(employee.DateOfBirth.ToString()),
                //    "DateOfBirth");
                //content.Add(new StringContent(employee.PhoneNumber), "PhoneNumber");
                //content.Add(new StringContent(employee.Email), "Email");

                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(employee),
                            Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(_configuration["BaseURL"]
                + "/" + employee.EmployeeId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updatedEmployee = JsonConvert
                        .DeserializeObject<EmployeeViewModel>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration["BaseURL"]
                    + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(apiResponse);
                }
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employee)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient
                        .DeleteAsync(_configuration["BaseURL"] + "/"
                        + employee.EmployeeId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration["BaseURL"]
                    + "/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(apiResponse);
                }
            }
            return View(employee);
        }
      
        public ViewResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Produces("application/json")]
        public async Task<IActionResult> Create(EmployeeViewModel employee,
            IFormFile photo)
        {
            if (employee == null)
                return BadRequest("Employee is null");

            if (photo == null || photo.Length == 0)
            {
                return Content("File not selected ..!");
            }
            else
            {
                // Handling image at server side or storing 
                //              image on server under wwwroot/images
                //var path = Path.Combine
                //    (this._ihostingEnvironment.WebRootPath, "images", photo.FileName);

                // Or
                var path = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot/images", photo.FileName);

                var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream);
                employee.Photo = photo.FileName;
            }

            EmployeeViewModel newEmployee = new EmployeeViewModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(employee),
                            Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync
                                    (_configuration["BaseURL"], content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>
                            (apiResponse);
                }
            }
            //  return View(newEmployee);
            return RedirectToAction("Index");
        }

       
    }
}