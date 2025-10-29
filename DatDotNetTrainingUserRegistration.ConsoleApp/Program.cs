using DatDotNetTrainingUserRegistration.ConsoleApp.Features;

Console.WriteLine("=== DotNet Training Login Console ===");

while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Login");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();

    if (choice == "1")
    {
        LoginService loginService = new LoginService();
        await loginService.Login();
    }
    else if (choice == "0")
    {
        Console.WriteLine("Exiting...");
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Try again.");
    }
}