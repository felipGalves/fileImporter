using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace fileImporter.Utils.ConsoleConversation.Enums
{
    public enum FiltersCustomer
    {
        [Description("Find By Cod")]
        FindByCod = 1,
        [Description("Find By Name")]
        FindByName = 2,
    }
}