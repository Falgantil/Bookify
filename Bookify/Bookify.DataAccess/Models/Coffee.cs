using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.DataAccess.Models
{
    public class Coffee
    {
        public bool BadCoffee { get; set; } = false;

        public string IsItGood()
        {
            return BadCoffee ? "This one is a bad cup of coffee, get a new" : "This is a good cup of coffee, you can drink this one";
        }
    }

}
