using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Tamagotchi
{
    internal class Tamagotchi
    {
        public static Random r = new Random();
        public int Health { get; set; }
        public int Happiness { get; set; }
        public int Hunger { get; set; }
        public Tamagotchi()
        {
            Health = r.Next(100);
            Happiness = r.Next(100);
            Hunger = r.Next(100);
        }
  
        public override string ToString()
        {
            return $" Здоровье - {Health}\n Счастье - {Happiness}\n Сытость - {Hunger}\n";
        }

        public void Print()
        {
            if (Health > 50 && Happiness > 50 && Hunger > 50)
                WriteLine(@"
              ▓▓▓▓
            ▓▓......▓
            ▓▓......▓▓                  ▓▓▓▓
            ▓▓......▓▓              ▓▓......▓▓▓▓
            ▓▓....▓▓              ▓......▓▓......▓▓
              ▓▓....▓            ▓....▓▓    ▓▓▓....▓▓
                ▓▓....▓        ▓....▓▓          ▓▓...▓
                  ▓▓..▓▓    ▓▓..▓▓                 ▓▓
                  ▓▓......▓▓....▓▓
                 ▓..................▓
               ▓.....................▓
              ▓......^..........^.....▓
              ▓............@..........▓
              ▓.......................▓
                ▓........\__/.......▓
                    ▓▓..........▓▓\n");

            else
                WriteLine(@"
              ▓▓▓▓
            ▓▓......▓
            ▓▓......▓▓                  ▓▓▓▓
            ▓▓......▓▓              ▓▓......▓▓▓▓
            ▓▓....▓▓              ▓......▓▓......▓▓
              ▓▓....▓            ▓....▓▓    ▓▓▓....▓▓
                ▓▓....▓        ▓....▓▓          ▓▓...▓
                  ▓▓..▓▓    ▓▓..▓▓                 ▓▓
                  ▓▓......▓▓....▓▓
                 ▓..................▓
               ▓.....................▓
              ▓......--........--.....▓
              ▓............@..........▓
              ▓.......................▓
                ▓........./\........▓
                    ▓▓..........▓▓\n");

        }
    }

    class Option
    {
        public event Action<Tamagotchi> Tired; //устал
        public event Action<Tamagotchi> Sad; //загрустил
        public event Action<Tamagotchi> Hungry; //проголодался
        public event Action<Tamagotchi> Died; //умер
         
        public void CheckStatus(Tamagotchi tm)
        {
            while (tm.Health > 0 && tm.Hunger > 0 && tm.Happiness > 0)
            {
                if (tm.Health <= tm.Hunger && tm.Health < tm.Happiness)
                {
                    Clear();
                    Tired?.Invoke(tm);
                }
                if (tm.Happiness < tm.Health && tm.Happiness < tm.Hunger)
                {
                    Clear();
                    Sad?.Invoke(tm);
                }
                if (tm.Hunger < tm.Health && tm.Hunger < tm.Happiness)
                {
                    Clear();
                    Hungry?.Invoke(tm);
                }
            }

            Clear();
            Died?.Invoke(tm);
        }
    }

    class Game
    {
        public Game()
        {
            Tamagotchi tamagotchi = new Tamagotchi();
            Option option = new Option();
            option.Tired += Option_Tired;
            option.Sad += Option_Sad;
            option.Hungry += Option_Hungry;
            option.Died += Option_Died;
            option.CheckStatus(tamagotchi);
        }
        private static void Option_Died(Tamagotchi tm)
        {
            WriteLine("Питомец умер");
            return;
        }

        private static void Option_Hungry(Tamagotchi tm)
        {
            tm.Print();
            WriteLine(tm);
            WriteLine("Я проголодался, накорми меня");
            WriteLine("Накормить? Если ДА - нажмите 1, иначе НЕТ");
            try
            {
                string tmp = ReadLine();
                if (tmp == "1")
                {
                    tm.Hunger = 100;
                    if (tm.Happiness <= 90)
                        tm.Happiness += 10;
                    if(tm.Health >20)
                        tm.Health -= 20;
                }
                else
                {
                    if (tm.Health >30)
                        tm.Health -= 20;
                    if (tm.Happiness >30)
                        tm.Happiness -= 20;
                    tm.Hunger -= 20;
                    throw new Exception("Хорошо еще потерплю немного");
                   
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        private static void Option_Sad(Tamagotchi tm)
        {
            tm.Print();
            WriteLine(tm);
            WriteLine("Мне грустно, поиграй со мной");
            WriteLine("Поиграть? Если ДА - нажмите 1, иначе НЕТ");
            try
            {
                string tmp = ReadLine();
                if (tmp == "1")
                {
                    tm.Happiness = 100;
                    if(tm.Hunger>20)
                        tm.Hunger -= 20;

                }
                else
                {
                    if (tm.Health >20)
                        tm.Health -= 20;
                    tm.Happiness -= 10;
                    throw new Exception("Я плачу");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        private static void Option_Tired(Tamagotchi tm)
        {
            tm.Print();
            WriteLine(tm);
            WriteLine("Я уcтал, уложи меня спать...");
            WriteLine("Уложить? Если ДА - нажмите 1, иначе НЕТ");
            try
            {
                string tmp = ReadLine();
                if (tmp == "1")
                {
                    tm.Health = 100;
                    if (tm.Happiness < 90)
                        tm.Happiness += 10;
                    if(tm.Hunger>30)
                    tm.Hunger -= 30;
                }
                else
                {
                    tm.Health -= 20;
                    if(tm.Happiness>10)
                    tm.Happiness -= 10;
                    throw new Exception("Хорошо еще потерплю немного");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
    }
}

