using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace fileImporter.Utils.ConsoleConversation.Enums
{
    public enum CustomerFieldsToFind
    {
        [Description("ID")]
        FindByID = 1,
        [Description("NAME")]
        FindByName = 2,
        [Description("ADDRESS")]
        FindByAddress = 3,
        [Description("EMAIL")]
        FindByEmail = 4,
        [Description("PHONE")]
        FindByPhone = 5,
        [Description("CITY")]
        FindByCity = 6,
    }
}