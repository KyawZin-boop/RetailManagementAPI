﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppConfig;

public class PaginatedResponseModel<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
