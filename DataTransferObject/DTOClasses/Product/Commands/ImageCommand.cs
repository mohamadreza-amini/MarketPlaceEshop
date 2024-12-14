﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class ImageCommand
{
    public string Path { get; set; }
    public string MimeType { get; set; }
    public int FileSize { get; set; }

}
