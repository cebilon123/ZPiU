﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.Category
{
    public class CategoryCreateResponse : BaseReponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
