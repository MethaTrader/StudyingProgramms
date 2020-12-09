using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Account : IBank 
    {
        public int account_id { get; set; }
        public decimal amount { get; set; }

        public virtual void new_account(int id)
        {
            Console.WriteLine("Счёт успешно создан");
        }
        public virtual void delete_account()
        {

        }

    }
}
