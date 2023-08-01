using System;
using System.Collections.Generic;

namespace ActividadDeTalleres.Models;

public partial class Tallere
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public int DuracionHoras { get; set; }

    public int CupoMaximo { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
