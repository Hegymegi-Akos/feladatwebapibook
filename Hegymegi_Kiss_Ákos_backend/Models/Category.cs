using System;
using System.Collections.Generic;

namespace Hegymegi_Kiss_Ákos_backend.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
}
