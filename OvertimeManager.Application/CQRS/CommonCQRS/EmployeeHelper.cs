using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.CommonCQRS
{
    public static class EmployeeHelper
    {
        /// <summary>
        /// Check if an employee is under a specific manager
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="managerId"></param>
        /// <param name="_employeeRepository"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public static async Task<bool> IsEmployeeUnderManager(int employeeId, int managerId, IEmployeeRepository _employeeRepository)
        {
            var manager = await _employeeRepository.GetByIdAsync(managerId);
            if (manager == null)
                throw new NotFoundException("Manager", managerId.ToString());

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null || employee.ManagerId != manager.Id)
                return false;

            return true;
        }
        /// <summary>
        /// Retrieve an employee if they are under a specific manager
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="managerId"></param>
        /// <param name="_employeeRepository"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public static async Task<Domain.Entities.User.Employee> GetEmployeeIfUnderManager(
            int employeeId, int managerId, IEmployeeRepository _employeeRepository)
        {
            var manager = await _employeeRepository.GetByIdAsync(managerId);
            if (manager == null)
                throw new NotFoundException("Manager", managerId.ToString());

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null || employee.ManagerId != manager.Id)
                throw new NotFoundException($"Employee with id: {employeeId} not found under the current manager");

            return employee;
        }


        /// <summary>
        /// Check if request created for employee under a specific manager
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <param name="requestId"></param>
        /// <param name="currentManagerId"></param>
        /// <param name="_employeeRepository"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public static async Task<bool> IsManagerEmployeeRequest(Domain.Entities.Overtime.OvertimeRequestBase baseRequest, 
            int requestId, int currentManagerId, IEmployeeRepository _employeeRepository)
        {
            if (baseRequest == null)
                throw new NotFoundException("Overtime request", requestId.ToString());

            var manager = await _employeeRepository.GetByIdAsync(currentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager", currentManagerId.ToString());

            if (baseRequest.RequestedForEmployee!.ManagerId != manager.Id)
                return false; 

            return true;
        }

    }
}
