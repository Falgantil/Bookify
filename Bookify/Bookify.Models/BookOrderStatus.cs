﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Models
{
    public enum BookOrderStatus
    {
        Borrowed,
        Sold,
        Queued,
        Dequeued
    }
}
