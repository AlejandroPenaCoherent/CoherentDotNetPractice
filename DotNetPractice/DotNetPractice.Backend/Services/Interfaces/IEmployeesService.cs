using DotNetPractice.Backend.Models;
using DotNetPractice.DataModel;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services.Interfaces
{
    public interface IEmployeesService
    {
        public Task<OperationResultWithData<Employee>> Create(Employee employee);
        public Task<OperationResultWithData<Employee>> Read(int employeeId);
        public Task<OperationResult> Update(Employee employee);
        public Task<OperationResult> Delete(int employeeId);
    }
}
