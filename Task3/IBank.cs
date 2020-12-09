using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    interface IBank
    {
        void new_account(int id);
        void delete_account();

        int account_id { get; set; } // идентификатор счёта
        decimal amount { get; set; } //кол-во денег

    }
}
