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
using System.Threading.Tasks;
using System.Globalization;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeController : ApiController
    {
        
        decimal TotalDotNetWorkingHours, TotalPhpWorkingHours, TotalAndroidWorkingHours, TotalIOSWorkingHours, TotalDesignWorkingHours, TotalSeoWorkingHours, TotalQaWorkingHours, TotalHybridHours = 0, TotalWorkingHoursOfProject = 0, OverAllTotalWorkingHoursOfProject = 0;
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
        [Route("~/api/Employee/GetSummaryOfWeekDetails")]
        public HttpResponseMessage GetSummaryOfWeekDetails(GetDSRDomainModel model)
        {
            try
            {
                SummaryDSRDomainModel SummaryModel = null;
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                   
                    var dsrs = EmployeeRepository.GetSummaryOfWeekDetails(model);
                    if (dsrs == null)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                    }
                    else if (dsrs.Count() == 0)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, SummaryModel);
                    }
                    else
                    {
                        SummaryModel = new SummaryDSRDomainModel();

                        Parallel.ForEach(dsrs, d =>
                        {
                            var mins = ConversionInMinute(d.WorkingHoursOfProject);
                            var finalmins = ConversionInMinute(TotalWorkingHoursOfProject.ToString());
                            var overAllMins = mins + finalmins;
                            var finalhrs = ConversionInHour(overAllMins);
                            TotalWorkingHoursOfProject = finalhrs;

                            var overAllWorkingHoursMins = ConversionInMinute(d.TotalWorkingHoursOfProject);
                            var overAllWorkingHourfinalmins = ConversionInMinute(OverAllTotalWorkingHoursOfProject.ToString());
                            var overAllWorkingHourMins = overAllWorkingHoursMins + overAllWorkingHourfinalmins;
                            OverAllTotalWorkingHoursOfProject = ConversionInHour(overAllWorkingHourMins);
                        });
                        SummaryModel.dsr = dsrs;
                        SummaryModel.OverallTotalWorkingHours = TotalWorkingHoursOfProject;
                        SummaryModel.OverallWeekTotalWorkingHours = OverAllTotalWorkingHoursOfProject;
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, SummaryModel);
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
        [Route("~/api/Employee/GetWeekelySummaryOfEmpDetails")]
        public HttpResponseMessage GetWeekelySummaryOfEmpDetails(GetDSRDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {

                    var dsrs = EmployeeRepository.GetWeekelySummaryOfEmpDetails(model);
                    if (dsrs == null)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, dsrs);
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
            decimal hours, finalHours = 0;
            OverAllSummaryOfWeekDetailsMainDomainModel OverAllSummaryOfWeekDetailsModel = new OverAllSummaryOfWeekDetailsMainDomainModel();
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
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
                                    //var TotalWeeklyHoursMins = ConversionInMinute(TotalWeeklyHours.ToString()) + finalmins;
                                    //TotalWeeklyHours = ConversionInHour(TotalWeeklyHoursMins);
                                }
                            }
                            OverAllSummaryOfWeekDetailsModel.TotalDotNetWorkingHours = TotalDotNetWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalPhpWorkingHours = TotalPhpWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalAndroidWorkingHours = TotalAndroidWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalIOSWorkingHours = TotalIOSWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalDesignWorkingHours = TotalDesignWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalSeoWorkingHours = TotalSeoWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalQaWorkingHours = TotalQaWorkingHours;
                            OverAllSummaryOfWeekDetailsModel.TotalHybridWorkingHours = TotalHybridHours;
                            var weeklyMinsTotal = ConversionInMinute(TotalDotNetWorkingHours.ToString()) + ConversionInMinute(TotalPhpWorkingHours.ToString()) + ConversionInMinute(TotalAndroidWorkingHours.ToString()) + ConversionInMinute(TotalIOSWorkingHours.ToString()) + ConversionInMinute(TotalDesignWorkingHours.ToString()) + ConversionInMinute(TotalSeoWorkingHours.ToString()) + ConversionInMinute(TotalQaWorkingHours.ToString()) + ConversionInMinute(TotalHybridHours.ToString());
                            OverAllSummaryOfWeekDetailsModel.TotalWeeklyWorkingHours = ConversionInHour(weeklyMinsTotal);
                            OverAllSummaryOfWeekDetailsModel.SummaryOfWeekDetails = SummaryOfWeekDetailsMainModelList;
                        }

                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, OverAllSummaryOfWeekDetailsModel);
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
            decimal minutes = 0;
            if (parts.Length > 1)
            {
                hours = Convert.ToDecimal(parts[0]);
                minutes = Convert.ToDecimal((parts[1]));
            }
            else
            {
                hours = Convert.ToDecimal(parts[0]);
                minutes = 0;
            }

            decimal TotalMintues = hours * 60 + minutes;
            return TotalMintues;
        }
    }
}