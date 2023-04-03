using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
            _aviaries.Add(new Aviary("Вольер для тигров", AnimalName.Tiger));
            _aviaries.Add(new Aviary("Вольер для львов", AnimalName.Lion));
            _aviaries.Add(new Aviary("Вольер для слонов", AnimalName.Elephant));
            _aviaries.Add(new Aviary("Вольер для жирафов", AnimalName.Fox));
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
        private Random _random; 

        public Aviary(string name, AnimalName animalName)
        {
            Name = name;
            _random = new Random();
            _animals = new List<Animal>();

            int minCountAnimals = 1;
            int maxCountAnimals = 10;
            int countAnimals = _random.Next(minCountAnimals, maxCountAnimals + 1);

            for (int i = 0; i < countAnimals; i++)
            {
                _animals.Add(new Animal(animalName));
            }
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
    }

    class Animal
    {
        private Sex _sex;
        private string _sound;
        private AnimalName _name;
        private Random _random;
        private Dictionary<AnimalName, string> _sounds;

        public Animal (AnimalName animalName)
        {
            _random = new Random();
            FillSounds();
            _sex = (Sex)_random.Next(0, (int)Sex.Length);
            _sound = _sounds[animalName];
        }

        public void ShowInfo()
        {
            Console.WriteLine("Пол: " + _sex + ". Издает звук: " + _sound);
        }

        private void FillSounds()
        {
            _sounds = new Dictionary<AnimalName, string>();
            _sounds.Add(AnimalName.Tiger, "мяу");
            _sounds.Add(AnimalName.Lion, "ррр");
            _sounds.Add(AnimalName.Elephant, "ииууу");
            _sounds.Add(AnimalName.Fox, "What???");
        }
    }

    enum AnimalName
    {
        Tiger,
        Lion,
        Elephant,
        Fox
    }

    enum Sex
    {
        Male,
        Female,
        Length
    }
}