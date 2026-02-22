using System.Text;

var passwords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Keep it");
    Console.WriteLine("1. Add a password");
    Console.WriteLine("2. Get existing password");
    Console.WriteLine("3. List all passwords");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddPassword();
            break;

        case "2":
            GetPassword();
            break;

        case "3":
            ListPasswords();
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

void AddPassword()
{
    Console.Write("Site name: ");
    var where  = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(where))
    {
        Console.WriteLine("Invalid site name.");
        return;
    }

    Console.Write("Password: ");
    var password = ReadHiddenInput();

    passwords[where] = password;
    Console.WriteLine("Password saved.");
}

void GetPassword()
{
    Console.Write("Site name: ");
    var site = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(site))
        return;

    if (passwords.TryGetValue(site, out var password))
        Console.WriteLine($"Password: {password}");
    else
        Console.WriteLine("No password found.");
}

void ListPasswords()
{
    if (passwords.Count == 0)
    {
        Console.WriteLine("No saved sites.");
        return;
    }

    Console.WriteLine("Saved Passwords:");
    foreach (var where in passwords.Keys)
        Console.WriteLine($"- {where}");
}

string ReadHiddenInput() //tutaj zmienia input w konsoli na *
{
    var sb = new StringBuilder();
    ConsoleKeyInfo key;

    while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
    {
        if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
        {
            sb.Length--;
            Console.Write("\b \b");
        }
        else if (!char.IsControl(key.KeyChar))
        {
            sb.Append(key.KeyChar);
            Console.Write("*");
        }
    }

    Console.WriteLine();
    return sb.ToString();
}