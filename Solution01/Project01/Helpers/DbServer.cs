using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project01.Helpers
{
    public class DbServer
    {
        public static string localConnection { get; } = @"Data Source=(LocalDb)\LocalDatabase;Initial Catalog=semester4_dotnet;Integrated Security=True";
    }
}
