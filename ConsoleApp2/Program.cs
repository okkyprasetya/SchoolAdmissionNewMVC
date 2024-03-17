using SchoolAdmission.BLL;

var verificatorBLL = new AdministratorBLL();

// Call the GetVerificators method to retrieve data
var verificators = verificatorBLL.getAllVerificator();

// Process the retrieved data
foreach (var verificator in verificators)
{
    Console.WriteLine($"Verificator ID: {verificator.VerificatorID}, Name: {verificator.User.FirstName}, Description: {verificator.SKNumber}");
}

// Add any additional processing or logic as needed

Console.ReadLine(); // K