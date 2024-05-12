using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.BusinessExeption
{
    public class BusinessExeption : Exception
    {
        public BusinessExeption()
        {

        }

        public BusinessExeption(string message) : base(message)
        {

        }
    }
}
