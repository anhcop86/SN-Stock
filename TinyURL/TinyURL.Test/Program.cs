using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyURL.Data;
using TinyURL.Entity;

namespace TinyURL.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //URLTiny result;
            //using (var dac = new TinyURLDAO()) {
            //    dac.id = 1;

            //    result = dac.GetURLTiny().FirstOrDefault();
            //}

            URLTiny result;
            using (var dac = new TinyURLDAO()) {
                dac.Id = 1;

                result = dac.GetURLTinyAsync();
            }
          
        }


    }
}
