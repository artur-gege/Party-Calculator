namespace Tysa
{
    class Program
    {
        public static void Main(string[] agrs)
        {
            Person[] people =
            {
                new Person("Вадим", 67.0),
                new Person("Артур", 37.0),
                new Person("Саня", 13.0),
                new Person("Бендер", 15.0),
                new Person("Гаврик", 7.0),
                new Person("Полина", 4.0)
            };
            
            var calculate = new Calculate("BYN", people);

            calculate.ShowInfo();

            calculate.DebtCalculate();
            Console.WriteLine();
        }
    }

    class Person
    {
        public bool IsAlcohol { get; set; }
        public string Name { get; private set; }
        public double Contribution { get; set; }

        public Person(string name, double contribution, bool isAlcohol = true)
        {
            Name = name;
            Contribution = contribution * 10000;
            IsAlcohol = isAlcohol;
        }
    }

    class Calculate
    {
        public static string Valuta { get; private set; }
        public static Person[] People { get; private set; }

        private List<Person> peopleWithDept = new List<Person>();
        private List<Person> peopleWithoutDept = new List<Person>();

        public Calculate(string valuta, Person[] people)
        {
            Valuta = valuta;
            People = people;
        }

        public void DebtCalculate()
        {
            double totalCost = 0;
            foreach (var item in People)
            {
                totalCost += item.Contribution;
            }

            double costOnePerson = totalCost / People.Length;
            Console.WriteLine("Total cost = " + totalCost / 10000 + " " + Valuta);
            Console.WriteLine("Cost of one person = " + costOnePerson  / 10000 + " " + Valuta + "\n");

            foreach (var item in People)
            {
                item.Contribution -= costOnePerson;
                if (item.Contribution >= 0)
                    peopleWithoutDept.Add(item);
                else
                    peopleWithDept.Add(item);
            }

            ShowInfo();

            //ShowPeopleWithContribution();
            //ShowPeopleWithDept();

            double debt, fullDebt = 0;
            double contribution;
            int i = 0, j = 0;

            foreach (var item in peopleWithoutDept)
            {
                fullDebt += item.Contribution;
            }

            Console.WriteLine("Общий долг = " + fullDebt / 10000 + " " + Valuta);
            Console.WriteLine();

            while (fullDebt > 0 && fullDebt != 0)
            {
                debt = Math.Abs(peopleWithDept[i].Contribution);
                contribution = peopleWithoutDept[j].Contribution;

                if (debt == contribution)
                {
                    Console.WriteLine($"{peopleWithDept[i].Name} должен {peopleWithoutDept[j].Name} {debt / 10000} {Valuta}");
                    peopleWithoutDept[j].Contribution = 0;
                    peopleWithDept[i].Contribution = 0;
                    j++;
                    i++;
                    fullDebt -= debt;
                }
                else if (debt < contribution) 
                {
                    Console.WriteLine($"{peopleWithDept[i].Name} должен {peopleWithoutDept[j].Name} {debt / 10000} {Valuta}");
                    peopleWithoutDept[j].Contribution += peopleWithDept[i].Contribution;
                    peopleWithDept[i].Contribution = 0;
                    i++;
                    fullDebt -= debt;
                }
                else
                {
                    Console.WriteLine($"{peopleWithDept[i].Name} должен {peopleWithoutDept[j].Name} {(contribution) / 10000} {Valuta}");
                    peopleWithDept[i].Contribution += peopleWithoutDept[j].Contribution;
                    peopleWithoutDept[j].Contribution = 0;
                    j++;
                    fullDebt -= contribution;
                }
            }
        }

        public void ShowPeopleWithContribution()
        {
            Console.WriteLine("People with contribution:");
            foreach (var item in peopleWithoutDept)
            {
                Console.Write($"Имя: {item.Name}\tВклад: {item.Contribution} {Valuta}\n");
            }
            Console.WriteLine();
        }
        public void ShowPeopleWithDept()
        {
            Console.WriteLine("People with dept:");
            foreach (var item in peopleWithDept)
            {
                Console.Write($"Имя: {item.Name}\tВклад: {item.Contribution} {Valuta}\n");
            }
            Console.WriteLine();
        }
        public void ShowInfo()
        {
            foreach (var item in People)
            {
                Console.Write($"Имя: {item.Name}\tВклад: {item.Contribution / 10000} {Valuta}\n");
            }
            Console.WriteLine();
        }
    }
}


