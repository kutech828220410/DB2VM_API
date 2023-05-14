using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
namespace OrderTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = Basic.Net.WEBApiGet("https://192.168.47.250/dbvm/BBAR/orderRefresh");
            Console.WriteLine(result);
        }
    }
}
