using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Drawing;
using Console = Colorful.Console;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Current_account> currents = new List<Current_account>(); //список текущих счетов
            List<Deposit_account> deposits = new List<Deposit_account>(); //список депозитных счетов

            string command;//строка, которая будет содержать команду+

            while (true)
            {
                Console.Write("\nВведите команду (!help для помощи): ", Color.White);
                command = Console.ReadLine(); //читаем команду из консоли

                //Команда помощь
                if (Regex.IsMatch(command, @"^!help$"))
                {
                    Console.WriteLine("\n=== Доступные команды ===\n", Color.Yellow);
                    Console.WriteLine("!add <0/1> <номер счёта> - создать счёт (текущий/депозитный)" +
                        "\n!delete <0/1> <номер счёта> - удалить (текущий/депозитный)  счёт" +
                        "\n!deposit <0/1> <номер счёта> <сумма> - пополнение счёта" +
                        "\n!withdraw <0/1> <номер счёта> <сумма> - снятие средств со счёта" +
                        "\n!list - показать список ВСЕХ счетов" +
                        "\n!exit - выйти из приложения", Color.Gold);
                }
                //Команда выхода
                else if (Regex.IsMatch(command, @"^!exit$"))
                {
                    break;
                }
                //Команда добавления счёта
                else if (Regex.IsMatch(command, @"^\!add\s[0-1]\s\d{1,}$"))
                {
                    int type = int.Parse(command.Split(' ')[1]); //тип  счёта
                    int id = int.Parse(command.Split(' ')[2]); //тип  идентификатор счёта

                    if (type == 0)
                    {
                        if (isExistCurr(currents, id) == -1)
                        {
                            currents.Add(new Current_account(id)); //добавляем новый счёт (ТЕКУЩИЙ)
                        } else { Console.WriteLine("Аккаунт уже существует", Color.IndianRed); }
                    } else
                    {
                        if (isExistDep(deposits, id) == -1)
                        {
                            deposits.Add(new Deposit_account(id)); //добавляем новый счёт (ДЕПОЗИТ)
                        } else { Console.WriteLine("Аккаунт уже существует", Color.IndianRed); }
                    }

                }
                //Команда удаления счёта
                else if (Regex.IsMatch(command, @"^\!delete\s[0-1]\s\d{1,}$"))
                {
                    int type = int.Parse(command.Split(' ')[1]);
                    int id = int.Parse(command.Split(' ')[2]);

                    if (type == 0)
                    {
                        int indexList = isExistCurr(currents, id);
                        if (indexList != -1)
                        {
                            currents[indexList].delete_account();
                            currents.RemoveAt(indexList);
                        }
                        else { Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed); }
                    }
                    else
                    {
                        int indexList = isExistDep(deposits, id);
                        if (indexList != -1)
                        {
                            deposits[indexList].delete_account();
                            deposits.RemoveAt(indexList);
                        }
                        else { Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed); }
                    }
                }
                //Команда пополнение баланса
                else if (Regex.IsMatch(command, @"^\!deposit\s[0-1]\s\d{1,}\s\d{1,}$"))
                {
                    int type = int.Parse(command.Split(' ')[1]);
                    int id = int.Parse(command.Split(' ')[2]);
                    int amount = int.Parse(command.Split(' ')[3]);

                    if (type == 0) //пополнение текущего аккаунта
                    {
                        int indexList = isExistCurr(currents, id);

                        if (indexList != -1) {
                            Console.WriteLine(currents[indexList].deposit(amount), Color.LightGreen);
                        } else Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed);
                    }
                    else //пополнение депозитного аккаунта
                    {
                        int indexList = isExistDep(deposits, id);

                        if (indexList != -1)
                        {
                            Console.WriteLine(deposits[indexList].deposit(amount), Color.LightGreen);
                        } else Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed);
                    }
                }
                //Команда выплаты со счёта
                else if(Regex.IsMatch(command, @"^\!withdraw\s[0-1]\s\d{1,}\s\d{1,}$"))
                {
                    int type = int.Parse(command.Split(' ')[1]);
                    int id = int.Parse(command.Split(' ')[2]);
                    int amount = int.Parse(command.Split(' ')[3]);

                    if (type == 0) //выплата текущего аккаунта
                    {
                        int indexList = isExistCurr(currents, id);

                        if (indexList != -1)
                        {
                            Console.WriteLine(currents[indexList].withdraw(amount), Color.LightGreen);
                        }
                        else Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed);
                    }
                    else //выплата депозитного аккаунта
                    {
                        int indexList = isExistDep(deposits, id);

                        if (indexList != -1)
                        {
                            Console.WriteLine(deposits[indexList].withdraw(amount), Color.LightGreen);
                        }
                        else Console.WriteLine("Аккаунт НЕ существует", Color.IndianRed);
                    }
                }
                //Команда вывода всех счетов на экран
                else if(Regex.IsMatch(command, @"^!list$"))
                {
                    //выводим депозитные счета
                    foreach (var dep in deposits)
                    {
                        Console.WriteLine("Депозитный счёт #" + dep.account_id, Color.LightYellow);
                    }
                    //выводим текущие счета
                    foreach (var curr in currents)
                    {
                        Console.WriteLine("Текущий счёт #" + curr.account_id, Color.LightYellow);
                    }
                }
                //Неизвестная команда
                else
                {
                    Console.WriteLine("Неизвестная команда", Color.PaleVioletRed);
                }
            }
        }

        //Методы для проверки (ПОИСКА) существования счета по его UID
        static int isExistDep(List<Deposit_account> dep, int id)
        {
                int index = -1;
                for (int i = 0; i < dep.Count; i++)
                {
                    if (dep[i].account_id == id)
                    {
                        index = i;
                    }
                }
                return index;
        }

        static int isExistCurr(List<Current_account> curr, int id)
        {
            int index = -1;
            for (int i = 0; i < curr.Count; i++ )
            {
                if (curr[i].account_id == id)
                {
                    index = i;
                }
            }
            return index;
        }
    }
}
