using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Infrastructure.Seeders
{
    public class OvertimeManagerSeeder
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public OvertimeManagerSeeder(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                await SeedRoles();
                await SeedOvertimeStatus();
                await SeedEmployees();
            }
        }


        private async Task SeedEmployees()
        {
            if (!_dbContext.Employees.Any())
            {
                //adding top manager
                var chef = new Employee()
                {
                    FirstName = "Szef",
                    LastName = "Wszystkich Szefów",
                    Email = "Szefuncio@company.com",
                    RoleId = 3,
                    OvertimeSummary = new EmployeeOvertimeSummary()
                };
                await _dbContext.Employees.AddAsync(chef);
                await _dbContext.SaveChangesAsync();

                var cheefId = chef.Id;

                //adding managers
                var manager1 = new Employee()
                {
                    FirstName = "Piero",
                    LastName = "Menagiero",
                    Email = "Menagiero@company.com",
                    RoleId = 2,
                    ManagerId = cheefId,
                    OvertimeSummary = new EmployeeOvertimeSummary()
                };
                var manager2 = new Employee()
                {
                    FirstName = "Eriko",
                    LastName = "Kierowniko",
                    Email = "Kierowniko@company.com",
                    RoleId = 2,
                    ManagerId = cheefId,
                    OvertimeSummary = new EmployeeOvertimeSummary()
                };

                await _dbContext.AddRangeAsync(manager1, manager2);
                await _dbContext.SaveChangesAsync();

                var manager1Id = manager1.Id;
                var manager2Id = manager2.Id;


                //adding employees
                var emplooyees = new List<Employee>()
            {
                new Employee(){FirstName = "Jan", LastName = "Kowalski", Email = "Jan.Kowalski@company.com", RoleId = 1, ManagerId = manager1Id, OvertimeSummary = new EmployeeOvertimeSummary()},
                new Employee(){FirstName = "Zdzisiek", LastName = "Obibok", Email = "Zdzisiek.Obibok@company.com", RoleId = 1, ManagerId = manager1Id, OvertimeSummary = new EmployeeOvertimeSummary()},
                new Employee(){FirstName = "Stefan", LastName = "Burczymucha", Email = "Stefan.Burczymucha@company.com", RoleId = 1, ManagerId = manager1Id, OvertimeSummary = new EmployeeOvertimeSummary()},
                new Employee(){FirstName = "Koziołek", LastName = "Matołek", Email = "Koziolek.Matolek@company.com", RoleId = 1, ManagerId = manager2Id, OvertimeSummary = new EmployeeOvertimeSummary()},
                new Employee(){FirstName = "Edward", LastName = "Nożycoreki", Email = "Edward.Nozycoreki", RoleId = 1, ManagerId = manager2Id, OvertimeSummary = new EmployeeOvertimeSummary()},
            };
                await _dbContext.Employees.AddRangeAsync(emplooyees);
                await _dbContext.SaveChangesAsync(); 
            }
        }

        private async Task SeedOvertimeStatus()
        {
            if (!_dbContext.OvertimeRequestsStatusses.Any())
            {
                var statusses = new List<OvertimeRequestStatus>();
                foreach (var overtimeStatus in OvertimeStatus.Status)
                {
                    statusses.Add(new OvertimeRequestStatus() { Status = overtimeStatus});
                }
                await _dbContext.OvertimeRequestsStatusses.AddRangeAsync(statusses);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedRoles()
        {
            if(!_dbContext.EmployeeRoles.Any())
            {
                var roles = new List<EmployeeRole>();
                foreach (var userRole in UserRoles.Roles)
                {
                    roles.Add(new EmployeeRole() { Name = userRole });
                }
                await _dbContext.EmployeeRoles.AddRangeAsync(roles);
                await _dbContext.SaveChangesAsync();
            }
        }

        private List<string> GetElementsToList(List<string> list)
        {
            var result = new List<string>();
            foreach(var element in list)
            {
                result.Add(element);
            }
            return result;
        }



    }
}
