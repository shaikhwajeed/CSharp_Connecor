using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        public static List<Student> Students = new List<Student>();

        [Route("student/create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("student/create")]
        [HttpPost]
        public async Task<ActionResult> Create(Student item)
        {
            //save the student to the temp list.
            Students.Add(item);

            //loop through subscriptions and call webhooks for each
            foreach (var sub in SubscriptionRepository.Subscriptions)
            {
                await CallWebhook(sub.WebHookUri, item);

            }

            return RedirectToAction("Create");
        }
        
        [Route("student/detail/{id}")]
        public ActionResult Detail(string id)
        {
            return View(Students.FirstOrDefault(i => i.RollNo == id));
        }


        private async Task CallWebhook(string webhook, Student item)
        {

            //prepare the json payload
            var json = @"
                {
                    'summary': 'A new student was added in database',
                    'sections': [
                        {
                            'activityTitle': 'New Admission!',
                            'facts': [
                                {
                                    'name': 'Name:',
                                    'value': '" + item.FullName + @"'
                                },
                                {
                                    'name': 'Roll No:',
                                    'value': '" + item.RollNo + @"'
                                }
                            ]
                        }
                    ],
                    'potentialAction': [
                        {
                            '@context': 'http://schema.org',
                            '@type': 'ViewAction',
                            'name': 'View student details',
                            'target': [
                                'https://54901686.ngrok.io/student/detail/" + item.RollNo + @"'
                            ]
                        }
                    ]}";

            //prepare the http POST
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(webhook, content))
            {
                //TODO: check response.IsSuccessStatusCode
            }
        }
    }
}