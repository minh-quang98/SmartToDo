using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTodo.Domain.Enums
{
    public enum TodoStatus
    {
        New = 0,
        Pending = 1,
        InProgress = 2,
        Complete = 3,
        Cancel = 4
    }
}