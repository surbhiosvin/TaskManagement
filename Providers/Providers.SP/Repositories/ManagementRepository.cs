using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Providers.SP.Repositories
{
    public class ManagementRepository : IManagement, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        SqlHelper objHelper = new SqlHelper();
        public ResponseDomainModel AddUpdateEmployee(EmployeeDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                int userId = objHelper.ExecuteScalar("AddUpdateEmployee", new
                {
                    UserId = model.UserId,
                    DepartmentId = model.DepartmentId,
                    DesignationId = model.DesignationId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfJoining = model.DateOfJoining,
                    FatherName = model.FatherName,
                    SpouseName = model.SpouseName,
                    Email = model.Email,
                    Password = model.Password,
                    DateOfBirth = model.DateOfBirth,
                    IsActive = true,
                    Role = model.Role,
                    PermanentAddress = model.PermanentAddress,
                    CurrentAddress = model.CurrentAddress,
                    PhoneNumber = model.PhoneNumber,
                    AlternatePhoneNumber = model.AlternatePhoneNumber,
                    PersonalEmailId = model.PersonalEmailId,
                    LastCompanySalary = model.LastCompanySalary,
                    LastCompanyName = model.LastCompanyName,
                    LastCompanyDesignation = model.LastCompanyDesigantion,
                    DateOfJoiningInFirstCompany = model.DateOfJoiningInFirstCompany,
                    Qualification = model.Qualification,
                    FileNo = model.FileNo,
                    DateOfAnniversary = model.DateOfAnniversary,
                    DateOfLeaving = model.DateOfLeaving,
                    ReasonOfLeaving = model.ReasonOfLeaving,
                    Image = model.Image,
                    IDCardFirst = model.IDCardFirst,
                    IDCardSecond_AddressProof = model.IDCardSecond_AddressProof_,
                    CV = model.CV,
                    ExperienceCertificate = model.ExperienceCertificate,
                    AppointementLetter = model.AppointementLetter,
                    AggrementDocument = model.AggrementDocument,
                    OtherDocument = model.OtherDocument,
                    CreatedDate = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    Archived = model.Archived
                });
                if (userId == -1)
                {
                    objRes.isSuccess = false;
                    objRes.response = "Email already exists.";
                }
                else if (model.UserId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Employee updated successfully.";
                }
                else if (userId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Employee added successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.isSuccess = false;
                objRes.response = ex.Message;
            }
            return objRes;
        }
        public List<DepartmentDomainModel> GetDepartments()
        {
            try
            {
                var listDept = objHelper.Query<DepartmentDomainModel>("GetAllDepartments", null).ToList();
                return listDept;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<DesignationDomainModel> GetDesignationsByDepartmentId(long DepartmentId)
        {
            try
            {
                var listDesignation = objHelper.Query<DesignationDomainModel>("GetDesignationByDepartmentId", new { DepartmentId = DepartmentId }).ToList();
                return listDesignation;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public UserListDomainModel UserListBySearch(long UserId, string Role, string Archived)
        {
            UserListDomainModel objRes = new UserListDomainModel();
            try
            {
                objRes.listUsers = objHelper.Query<EmployeeDomainModel>("GET_USER_LIST_BY_SEARCH", new { UserId = UserId, UserRole = Role, Name = string.Empty, Type = Archived }).ToList();
                if (objRes.listUsers != null && objRes.listUsers.Count > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Success";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public EmployeeDomainModel GetEmployeeDataById(long UserId)
        {
            EmployeeDomainModel empDetails = new EmployeeDomainModel();
            try
            {
                empDetails = objHelper.Query<EmployeeDomainModel>("GetEmployeeDataById", new { UserId = UserId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return empDetails;
        }
        public ResponseDomainModel DeleteDepartmentById(long DepartmentId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("DeleteDepartmentById", new { DepartmentId = DepartmentId });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Department deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    objRes.isSuccess = true;
                    objRes.response = "You are not allowed to delete this department.";
                }
            }
            return objRes;
        }
        public ResponseDomainModel DeleteDesignationById(long DesignationId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("DeleteDesignationById", new { DesignationId = DesignationId });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Designation deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    objRes.isSuccess = true;
                    objRes.response = "You are not allowed to delete this designation.";
                }
            }
            return objRes;
        }
        public ResponseDomainModel DeleteUserById(long UserId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("DeleteUserById", new { UserId = UserId });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public ResponseDomainModel ActivateDeactivateDepartment(long DepartmentId, bool IsActive)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("ActivateDeactivateDepartment", new { DepartmentId = DepartmentId, IsActive = IsActive });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public ResponseDomainModel ActivateDeactivateDesignation(long DesignationId, bool IsActive)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("ActivateDeactivateDesignation", new { DesignationId = DesignationId, IsActive = IsActive });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public ResponseDomainModel AddUpdateDepartment(DepartmentDomainModel model)
        {

            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                int DepartmentId = objHelper.ExecuteScalar("AddUpdateDepartment", new
                {
                    DepartmentId = model.DepartmentId,
                    DepartmentName = model.DepartmentName,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                });
                if (model.DepartmentId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Department updated successfully.";
                }
                else if (DepartmentId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Department added successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.isSuccess = false;
                objRes.response = ex.Message;
            }
            return objRes;
        }
        public ResponseDomainModel AddUpdateDesignation(DesignationDomainModel model)
        {

            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                int DesignationId = objHelper.ExecuteScalar("AddUpdateDesignation", new
                {
                    DesignationId = model.DesignationId,
                    DepartmentId = model.DepartmentId,
                    DesignationName = model.DesignationName,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                });
                if (model.DesignationId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Designation updated successfully.";
                }
                else if (DesignationId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Designation added successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.isSuccess = false;
                objRes.response = ex.Message;
            }
            return objRes;
        }
        public ResponseDomainModel ActivateDeactivateUser(long UserId, bool IsActive)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("ActivateDeactivateUser", new { UserId = UserId, IsActive = IsActive });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public ResponseDomainModel UpdateEmployeeArchive(long UserId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("UpdateEmployeeArchive", new { UserId = UserId });
                string Archived = res.First();
                if (Archived == "Insert")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Employee has Archived.";
                }
                else if (Archived == "Update")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Employee has removed from Archived.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public List<DesignationDomainModel> GetDesignationsBasedOnRole(long DepartmentId = 0)
        {
            List<DesignationDomainModel> listDesignation = null;
            try
            {
                if (DepartmentId > 0)
                {
                    listDesignation = objHelper.Query<DesignationDomainModel>("GET_DESIGNATION_BY_TEAMLEADER_DEPARTMENT_ID", new { DepartmentId = DepartmentId }).ToList();
                }
                else
                {
                    listDesignation = objHelper.Query<DesignationDomainModel>("GetAllDesignations", null).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listDesignation;
        }
        public PaySlipDomainModel AddUpdateEmployeePaySlip(PaySlipDomainModel model)
        {
            PaySlipDomainModel PaySlip = new PaySlipDomainModel();
            try
            {
                PaySlip = objHelper.Query<PaySlipDomainModel>("AddEmployeePaySlip", new
                {
                    PaySlipId = model.PaySlipID,
                    EmployeeId = model.EmployeeID,
                    DepartmentId = model.DepartmentId,
                    DesignationId = model.DesignationId,
                    EmployeeCode = model.EmployeeCode,
                    BankAccountNumber = model.BankAccountNumber,
                    PanCardNo = model.PanCardNumber,
                    DateOfJoining = model.DateOfJoining,
                    PaySlipMonth = model.PaySlipMonth,
                    NumberOfDaysInCurrentMonth = model.NumberOfDaysInCurrentMonth,
                    NumberOfDaysWorked = model.NumberOfDaysWorked,
                    BasicSalary = model.BasicSalary,
                    MedicalAllowance = model.MedicalAllowance,
                    OtherAllowance = model.OtherAllowance,
                    HouseRentAllowance = model.HouseRentAllowance,
                    ConveyanceAllowance = model.ConveyanceAllowance,
                    GrossSalary = model.GrossSalary,
                    TDS = model.TDS,
                    AmountForLeaveDeducation = model.AmountForLeaveDeduction,
                    TakeHomeSalary = model.TakeHomeSalary,
                    CreatedBy = model.CreatedBy
                }).FirstOrDefault();
                if (PaySlip != null && PaySlip.PaySlipID > 0)
                {
                    PaySlip.isSuccess = true;
                    PaySlip.response = "Success";
                }
                else
                {
                    PaySlip.isSuccess = false;
                    PaySlip.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                PaySlip.isSuccess = false;
                PaySlip.response = ex.Message;
            }
            return PaySlip;
        }
        public List<EmployeeDomainModel> GetEmployeeList()
        {
            try
            {
                var listEmployees = objHelper.Query<EmployeeDomainModel>("GetEmployeeList", null).ToList();
                return listEmployees;
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
