﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
}
