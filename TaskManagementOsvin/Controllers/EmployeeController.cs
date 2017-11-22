using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Providers.Providers.SP.Repositories;
using DomainModel.EntityModel;
using System.Text.RegularExpressions;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeController : ApiController
    {
        decimal TotalDotNetWorkingHours, TotalPhpWorkingHours, TotalAndroidWorkingHours, TotalIOSWorkingHours, TotalDesignWorkingHours, TotalSeoWorkingHours, TotalQaWorkingHours, TotalHybridHours = 0;
        static readonly IEmployee EmployeeRepository = new EmployeeRepository();
        // GET api/<controller>
        [HttpPost]
        [Route("~/api/Employee/AuthenticateUser")]
        public HttpResponseMessage AuthenticateUser(UserDetailsDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Employee = EmployeeRepository.AuthenticateEmployees(model);
                    if (Employee != null && Employee.isSuccess == true)
                    {
                        roleTypeDomainModel GetRoleType;
                        var roleType = Regex.Replace(Employee.Role, @"\s+", "");
                        Enum.TryParse(roleType, out GetRoleType);
                        Employee.roleType = GetRoleType;
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, Employee);
                    }
                    else if (Employee.isSuccess == false)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, model);
                    }
                    return httpResponse;
                }
                else
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    return httpResponse;
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.", StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        
        [Route("~/api/Employee/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var response = EmployeeRepository.GetEmployees();
                if (response == null)
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        [Route("~/api/Employee/ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePassword model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Response = EmployeeRepository.ChangePassword(model);
                    if (Response.isSuccess)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, Response);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, Response);
                    }
                }
                else
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        [Route("~/api/Employee/SummaryOfWeekDetails")]
        public HttpResponseMessage SummaryOfWeekDetails(GetSummaryDomainModel model)
        {
            decimal hours = 0; decimal finalHours = 0;
            List<SummaryOfWeekDetailsMainDomainModel> SummaryOfWeekDetailsMainModelList = new List<SummaryOfWeekDetailsMainDomainModel>();
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Summary = EmployeeRepository.SummaryOfWeekDetailsMain(model);
                    if (Summary != null)
                    {
                        var GroupedSummaries = Summary.GroupBy(x => x.ProjectId);
                        foreach (var items in GroupedSummaries)
                        {
                            finalHours = 0;// reset for every project else hours will be added to next project
                            hours = 0; // reset for every project else hours will be added to next project
                            foreach (var project in items)
                            {
                                
                                // to get accurate hours
                                var mins = ConversionInMinute(project.WorkingHours.ToString());
                                hours += ConversionInHour(mins);
                                // converted again for final output
                                var finalMins = ConversionInMinute(hours.ToString());
                                finalHours = ConversionInHour(finalMins);
                            }
                            var Getproject = items.First();
                            var SummaryOfWeekDetailsMainDomainModel = new SummaryOfWeekDetailsMainDomainModel() { ProjectId = Getproject.ProjectId, WorkingHours = finalHours, ClientName = Getproject.ClientName, ProjectTitle = Getproject.ProjectTitle };
                            SummaryOfWeekDetailsMainModelList.Add(SummaryOfWeekDetailsMainDomainModel);
                            var GetProjectFromSummary = SummaryOfWeekDetailsMainModelList.First(x => x.ProjectId == Getproject.ProjectId);
                            // to get subsummary of every department for every project 
                            GetProjectFromSummary.subweeksummary = Getproject.subweeksummary;
                        }
                        if (Summary.Count() > 0)
                        {
                            foreach (var item in SummaryOfWeekDetailsMainModelList)
                            {
                                var DotNetHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "Dot Net");
                                if (DotNetHours != null)
                                {
                                    var mins = ConversionInMinute(DotNetHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalDotNetWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalDotNetWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalDotNetWorkingHours = finalhrs;
                                }

                                var PhpHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "Php");
                                if (PhpHours != null)
                                {
                                    var mins = ConversionInMinute(PhpHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalPhpWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalPhpWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalPhpWorkingHours = finalhrs;
                                }

                                var AndroidHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "Android");
                                if (AndroidHours != null)
                                {
                                    var mins = ConversionInMinute(AndroidHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalAndroidWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalAndroidWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalAndroidWorkingHours = finalhrs;
                                }

                                var IOSHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "IPhone");
                                if (IOSHours != null)
                                {
                                    var mins = ConversionInMinute(IOSHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalIOSWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalIOSWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalIOSWorkingHours = finalhrs;
                                }

                                var DesignHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "Design");
                                if (DesignHours != null)
                                {
                                    var mins = ConversionInMinute(DesignHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalDesignWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalDesignWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalDesignWorkingHours = finalhrs;
                                }

                                var SEOHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "SEO");
                                if (SEOHours != null)
                                {
                                    var mins = ConversionInMinute(SEOHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalSeoWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalSeoWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalSeoWorkingHours = finalhrs;
                                }

                                var QAHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "QA");
                                if (QAHours != null)
                                {
                                    var mins = ConversionInMinute(QAHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalQaWorkingHours += hrs;
                                    var finalmins = ConversionInMinute(TotalQaWorkingHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalQaWorkingHours = finalhrs;
                                }

                                var HybridHours = item.subweeksummary.FirstOrDefault(x => x.DepartmentName == "Hybrid App ");
                                if (HybridHours != null)
                                {
                                    var mins = ConversionInMinute(HybridHours.WorkingHours.ToString());
                                    var hrs = ConversionInHour(mins);
                                    TotalHybridHours += hrs;
                                    var finalmins = ConversionInMinute(TotalHybridHours.ToString());
                                    var finalhrs = ConversionInHour(finalmins);
                                    TotalHybridHours = finalhrs;
                                }
                            }
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalDotNetWorkingHours = TotalDotNetWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalPhpWorkingHours = TotalPhpWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalAndroidWorkingHours = TotalAndroidWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalIOSWorkingHours = TotalIOSWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalDesignWorkingHours = TotalDesignWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalSeoWorkingHours = TotalSeoWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalQaWorkingHours = TotalQaWorkingHours;
                            SummaryOfWeekDetailsMainModelList.FirstOrDefault().TotalHybridWorkingHours = TotalHybridHours;
                        }

                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, SummaryOfWeekDetailsMainModelList);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occurred");
                    }
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, "Not authorized");
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        private decimal ConversionInHour(decimal durationMin)
        {
            decimal hours = (durationMin - durationMin % 60) / 60;
            decimal total = Convert.ToDecimal("" + hours + "." + Convert.ToDecimal(durationMin - hours * 60));
            return total;
        }
        private decimal ConversionInMinute(string WorkingHours)
        {

            string[] parts = WorkingHours.Split('.');
            decimal hours = 0;
            decimal mintues = 0;
            if (parts.Length > 1)
            {
                hours = Convert.ToDecimal(parts[0]);
                mintues = Convert.ToDecimal((parts[1]));
            }
            else
            {
                hours = Convert.ToDecimal(parts[0]);
                mintues = 0;
            }

            decimal TotalMintues = hours * 60 + mintues;
            return TotalMintues;
        }
    }
}