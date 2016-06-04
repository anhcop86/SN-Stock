using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyURL.Data;

namespace TinyURL.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var sss = TinyURLDAO.GetURLTiny(6666);
        }
    }
}
