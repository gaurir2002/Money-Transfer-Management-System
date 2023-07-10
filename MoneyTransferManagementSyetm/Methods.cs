using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferManagementSyetm
{
    class Methods
    {
        public void Alert(string Message)
        {
            MBox Obj = new MBox();
            Obj.Show();
            Obj.TopMost = true;
        }
    }
}
