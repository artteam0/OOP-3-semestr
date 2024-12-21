using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04
{
    public partial class Bush
    {
        public void PrintInf()
        {
            Console.WriteLine("Название" + PlantName());
            Console.WriteLine("Стоимость" + PlantCost());
        }
    }
}
