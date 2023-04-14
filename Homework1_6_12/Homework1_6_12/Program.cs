using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Homework1_6_12
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo()
        {
            _aviaries = new List<Aviary>();
            _aviaries.Add(new Aviary(AnimalNames.Tiger, "Замок"));
            _aviaries.Add(new Aviary(AnimalNames.Lion));
            _aviaries.Add(new Aviary(AnimalNames.Elephant));
            _aviaries.Add(new Aviary(AnimalNames.Fox));
        }

        public void Work()
        {
            const string SearchAviaryCommand = "1";
            const string ExitZooCommand = "2";

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(SearchAviaryCommand + ". Подойти к вольеру");
                Console.WriteLine(ExitZooCommand + ". Покинуть зоопарк");

                switch (Console.ReadLine())
                {
                    case SearchAviaryCommand:
                        SearchAviary();
                        break;

                    case ExitZooCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }

                Console.ReadKey();
            }
        }

        private void SearchAviary()
        {
            for (int i = 1; i <= _aviaries.Count; i++)
            {
                Console.WriteLine(i + ". " + _aviaries[i-1].Name);
            }

            Console.Write("Выберите номер вольера, к которому хотите подойти: ");

            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (userInput > 0 && userInput <= _aviaries.Count)
                {
                    _aviaries[userInput - 1].ShowInfo();
                }
                else
                {
                    Console.WriteLine("Такого вольера нет");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary(AnimalNames animalName, string name = "")
        {
            _animals = new List<Animal>();
            FillAnimals(animalName, name);
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine(Name + ".Здесь " + _animals.Count + " животных:");

            foreach (var animal in _animals)
            {
                animal.ShowInfo();
            }
        }

        private void FillAnimals(AnimalNames animalName, string name)
        {
            if (name != "")
            {
                name += ": ";
            }

            Name += $"{name}Вольер для {animalName}s";

            int minCountAnimals = 1;
            int maxCountAnimals = 10;
            int countAnimals = UserUtilits.GenerateRandomNumber(minCountAnimals, maxCountAnimals + 1);

            for (int i = 0; i < countAnimals; i++)
            {
                _animals.Add(new Animal(animalName));
            }
        }
    }

    class Animal
    {
        private Genders _gender;
        private string _sound;
        private Dictionary<AnimalNames, string> _sounds;

        public Animal (AnimalNames animalName)
        {
            FillSounds();
            _gender = (Genders)UserUtilits.GenerateRandomNumber(0, Enum.GetValues(typeof(Genders)).Length);
            _sound = _sounds[animalName];
        }

        public void ShowInfo()
        {
            Console.WriteLine("Пол: " + _gender + ". Издает звук: " + _sound);
        }

        private void FillSounds()
        {
            _sounds = new Dictionary<AnimalNames, string>();
            _sounds.Add(AnimalNames.Tiger, "мяу");
            _sounds.Add(AnimalNames.Lion, "ррр");
            _sounds.Add(AnimalNames.Elephant, "ииууу");
            _sounds.Add(AnimalNames.Fox, "What???");
        }
    }

    enum AnimalNames
    {
        Tiger,
        Lion,
        Elephant,
        Fox
    }

    enum Genders
    {
        Male,
        Female
    }

    class UserUtilits
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}