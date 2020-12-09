using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Console = Colorful.Console;

namespace Task3
{
    class Current_account : Account
    {
        public Current_account(int id)
        {
            this.account_id = id;
            Console.WriteLine($"Текущий счёт #{account_id} успешно создан", Color.LightGreen);
        }

        //бесполезный на данный момент метод
        public override void new_account(int id)
        {
            account_id = id;
            Console.WriteLine($"Текущий счёт #{account_id} успешно создан");
        }

        public override void delete_account()
        {
            Console.WriteLine($"Текущий счёт #{account_id} успешно удалён", Color.LightGreen);
        }

        public string deposit(int a)
        {
            if (a > 0)
            {
                amount += a;
                return $"Счёт был пополнен на {a} грн. Остаток: {amount} ";
            }
            else return "Сумма указана неверно";
            
        }

        public string withdraw(int a)
        {
            if (amount > a && a > 0)
            {
                amount -= a;
                return $"Со счёта было снято {a} грн. Остаток: {amount} ";
            } else return "Сумма указана неверно или на балансе недостаточно средств";
        }

    }
}
