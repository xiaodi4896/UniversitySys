using dxxt.Models;
using System.Collections.Generic;

public class LecturerIndexViewModel
{
    public IEnumerable<Lecturer>? Lecturers { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
