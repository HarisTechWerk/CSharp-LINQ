#nullable disable
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Employee
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int DepartmentId { get; set; }
  public decimal Salary { get; set; }
}

class Department
{
  public int Id { get; set; }
  public string Name { get; set; }
}

class Program
{
  static void Main()
  {
    List<Employee> employees = new List<Employee>
    {
      new Employee { Id = 1, Name = "Ali", DepartmentId = 1, Salary = 60000 },
      new Employee { Id = 2, Name = "Ahmed", DepartmentId = 2, Salary = 50000 },
      new Employee { Id = 3, Name = "Sara", DepartmentId = 1, Salary = 70000 },
      new Employee { Id = 4, Name = "John", DepartmentId = 3, Salary = 80000 },
      new Employee { Id = 5, Name = "Linda", DepartmentId = 1, Salary = 90000 },
    };

    List<Department> departments = new List<Department>
    {
      new Department { Id = 1, Name = "IT" },
      new Department { Id = 2, Name = "HR" },
      new Department { Id = 3, Name = "Finance" },
    };

    // LINQ Join - Employees with Departments
    var employeeDetails = employees
    .Join(departments,
     emp => emp.DepartmentId, // Foreign Key
     dept => dept.Id, // Primary Key
     (emp, dept) => new
     {
       emp.Name,
       Department = dept.Name,
       emp.Salary
     });

    Console.WriteLine("👨‍💼 Employees Details with Department:");
    foreach (var emp in employeeDetails)
    {
      Console.WriteLine($"- {emp.Name}, {emp.Department}, ({emp.Salary:C})");
    }

    var employeeSalaries = employees.Select(emp => new { emp.Name, emp.Salary });

    Console.WriteLine("\n💰 Employees Salaries:");

    foreach (var emp in employeeSalaries)
    {
      Console.WriteLine($" 🤵{emp.Name} | 💵 {emp.Salary:C}");
    }

    var totolPayroll = employees.Aggregate(0M, (sum, emp) => sum + emp.Salary);
    Console.WriteLine($"\n💸 Total Payroll Cost: {totolPayroll:C}");

    var sortedEmployes = employees.OrderByDescending(emp => emp.Salary);
    Console.WriteLine("\n📉 Employees Sorted by Salary:");

    foreach (var emp in sortedEmployes)
    {
      Console.WriteLine($" 🤵 {emp.Name} | 💵 {emp.Salary:C}");
    }

    var employeesByDepartment = employees.GroupBy(emp => emp.DepartmentId).Select(group => new
    {
      Department = departments.First(dept => dept.Id == group.Key).Name,
      Employees = group.Select(emp => emp)
    });

    Console.WriteLine("\n🏢 Employees Grouped by Department:");
    foreach (var dept in employeesByDepartment)
    {
      Console.WriteLine($"📌 {dept.Department}:");
      foreach (var emp in dept.Employees)
        Console.WriteLine($" 🤵 {emp.Name} | 💰{emp.Salary:C}");
    }

    var highEarners = employees.Where(emp => emp.Salary > 75000);
    Console.WriteLine("\n🤑 High Earners (Salary > 70K):");
    foreach (var emp in highEarners)
    {
      Console.WriteLine($" 🤵 {emp.Name} | 💰{emp.Salary:C}");
    }

    Console.WriteLine($"\n💰 Total Payroll: {employees.Sum(e => e.Salary):C}");
    Console.WriteLine($"📉 Lowest Salary: {employees.Min(e => e.Salary):C}");
    Console.WriteLine($"📈 Highest Salary: {employees.Max(e => e.Salary):C}");
    Console.WriteLine($"📊 Average Salary: {employees.Average(e => e.Salary):C}");

    var uniqueSalaries = employees.Select(e => e.Salary).Distinct();
    Console.WriteLine("\n📊 Unique Salaries:");
    foreach (var salary in uniqueSalaries)
    {
      Console.WriteLine($" 💰 {salary:C}");
    }

    var employeeDict = employees.ToDictionary(e => e.Id);
    if (employeeDict.TryGetValue(3, out var employee))
    {
      Console.WriteLine($"\n📚 Employee ID 3 is: {employee.Name}");
    }
    else
    {
      Console.WriteLine("\n📚 Employee ID 3 is: Not Found");
    }
  }
}






















//     // Optimized - combined Filters in One .Where() clause
//     var highestPaidITEmployees = employees.Where(e => e.Department == "IT" && e.Salary > 60000).ToList();

//     Console.WriteLine("\n💻 High Paid IT Employees:");
//     highestPaidITEmployees.ForEach(e => Console.WriteLine($"- {e.Name}, ({e.Salary:C})"));

//     // Optimized - Using .Any() instead of .Count()
//     bool hasHighEarners = employees.Any(e => e.Salary > 75000);

//     Console.WriteLine($"\n🤑 Do we have high earners? {hasHighEarners}");

//     // Optimized - Using .Max() instead of Sorting + First()
//     var highestSalary = employees.Max(e => e.Salary);
//     var highestPaidEmployee = employees.First(e => e.Salary == highestSalary);

//     Console.WriteLine($"\n🏆 Highest Paid Employee: {highestPaidEmployee.Name}, ({highestPaidEmployee.Salary:C})");

//     // Optimized - Grouping with Aggregates
//     var avgSalaryByDept = employees.GroupBy(e => e.Department).Select(Group => new
//     {
//       Department = Group.Key,
//       AverageSalary = Group.Average(e => e.Salary)
//     });

//     Console.WriteLine("\n🏢 Average Salary by Department:");

//     foreach (var dept in avgSalaryByDept)
//     {
//       Console.WriteLine($"📌 Department: {dept.Department}, Average Salary: {dept.AverageSalary:C}");
//     }
//   }
// }