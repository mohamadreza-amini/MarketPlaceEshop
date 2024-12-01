using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject;

public abstract class BaseDTO<KeyTypeId> where KeyTypeId : struct
{
    public KeyTypeId Id { get; set; }
}
