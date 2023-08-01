using System;
using System.Collections.Generic;

namespace ActividadDeTalleres.Models;

public partial class Inscripcione
{
    public int Id { get; set; }

    public int? TallerId { get; set; }

    public int? ParticipanteId { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Participante? Participante { get; set; }

    public virtual Tallere? Taller { get; set; }
}
