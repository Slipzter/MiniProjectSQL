namespace MiniProjectSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Menysystem som styrs med upp och ned pilar
            List<string> main_Menu = new List<string>()
                {
                    "Register Person",
                    "Register Project",
                    "Register Project Assignment",
                    "Show Overview of Projects"
                };

            bool[] choices = { true, false, false, false, };

            int x = 0;

            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.Write("\nWelcome to Tiny Projects Time Management Solutions.");
                Console.WriteLine();
                Console.WriteLine("\n(You can navigate through the menu with the 'up' and 'down' arrow keys) \n");

                if (choices[0] == true)
                {
                    Console.WriteLine("[ " + main_Menu[0] + " ]");
                }
                else if (choices[0] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[0]);
                }
                if (choices[1] == true)
                {
                    Console.WriteLine("[ " + main_Menu[1] + " ]");
                }
                else if (choices[1] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[1]);
                }
                if (choices[2] == true)
                {
                    Console.WriteLine("[ " + main_Menu[2] + " ]");
                }
                else if (choices[2] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[2]);
                }
                if (choices[3] == true)
                {
                    Console.WriteLine("[ " + main_Menu[3] + " ]");
                }
                else if (choices[3] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[3]);
                }

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (x == 3)
                    {
                        choices[0] = true;
                        choices[x] = false;
                        x = 0;
                    }
                    else
                    {
                        choices[x + 1] = true;
                        choices[x] = false;
                        x++;
                    }

                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (x == 0)
                    {
                        choices[3] = true;
                        choices[x] = false;
                        x = 5;
                    }
                    else
                    {
                        choices[x - 1] = true;
                        choices[x] = false;
                        x--;
                    }
                }

                else if (key.Key == ConsoleKey.Enter)
                {
                    var persons = PostgresDataAccess.LoadPersons();
                    var projects = PostgresDataAccess.LoadProjects();
                    var projectsPersons = PostgresDataAccess.LoadProjectsPersons();

                    switch (x)
                    {
                        case 0:
                            Console.Clear();
                            Console.WriteLine("You selected: Register new person.");
                            Console.Write("Please enter the person's Full name: ");
                            string fullName = Console.ReadLine();
                            PostgresDataAccess.RegisterPerson(fullName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Register complete.");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            break;
                        case 1:
                            Console.Clear();
                            Console.WriteLine("You selected: Register new project.");
                            Console.Write("Please enter project name: ");
                            string projectName = Console.ReadLine();
                            PostgresDataAccess.RegisterProject(projectName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Register complete.");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            break;
                        case 2:

                            int personId = 0;
                            int projectId = 0;

                            Console.Clear();
                            Console.WriteLine("You selected: Project Assignment");
                            Console.Write("Please enter the full name of the person to be assigned: ");
                            string assignedName = Console.ReadLine();
                            Console.Write("And what project should he/she be assigned to?: ");
                            string assignedProject = Console.ReadLine();
                            Console.Write("How many hours has he/she worked on this project? (only integers are valid): ");
                            int assignedHours = int.Parse(Console.ReadLine());

                            // Kollar om input stämmer överens med registrerade personer och projekt från databasen
                            foreach (var pers in persons)
                            {
                                if (assignedName == pers.person_name)
                                {
                                    personId = pers.id;
                                }
                            }
                            foreach (var proj in projects)
                            {
                                if (assignedProject == proj.project_name)
                                {
                                    projectId = proj.id;
                                }
                            }

                            PostgresDataAccess.AssignProject(personId, projectId, assignedHours);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Assignment complete.");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            break;
                        case 3:
                            string person = "";
                            string project = "";

                            // Printar ut en översikt över vem som jobbat med vad och hur länge
                            // Eftersom id inte ska visas för användaren kör jag en foreach på varje id och matchar med namn
                            foreach (var p in projectsPersons)
                            {
                                foreach (var pers in persons)
                                {
                                    if (p.person_id == pers.id)
                                    {
                                        person = pers.person_name;
                                    }
                                }
                                foreach (var proj in projects)
                                {
                                    if (p.project_id == proj.id)
                                    {
                                        project = proj.project_name;
                                    }
                                }
                                Console.WriteLine($"{person} has worked on {project} for {p.hours} hours.");
                            }
                            break;
                    }

                    Console.ReadLine();
                }
            }
        }
    }
}
