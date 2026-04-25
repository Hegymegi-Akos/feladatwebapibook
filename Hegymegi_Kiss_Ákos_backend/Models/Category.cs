using System;
using System.Collections.Generic;

namespace Hegymegi_Kiss_Ákos_backend.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    // A Books lista KELL a feladat 11-hez!
    // Eredetileg hiányzott a generált modellből, ezért az Include() elszállt.
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
