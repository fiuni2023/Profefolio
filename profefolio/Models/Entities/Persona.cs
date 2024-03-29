﻿using Microsoft.AspNetCore.Identity;

namespace profefolio.Models.Entities;

public class Persona : IdentityUser
{
    public bool Deleted
    {
        get;
        set;
    }

    public DateTime Created
    {
        get;
        set;
    }

    public string? CreatedBy
    {
        get;
        set;
    }
    public DateTime Modified
    {
        get;
        set;
    }

    public string? ModifiedBy
    {
        get;
        set;
    }
    public string? Nombre
    {
        get;
        set;
    }

    public string? Apellido
    {
        get;
        set;
    }

    public DateTime Nacimiento
    {
        get;
        set;
    }

    public string? Documento
    {
        get;
        set;
    }

    public string? DocumentoTipo
    {
        get;
        set;
    }

    public bool EsM
    {
        get;
        set;
    }

    public string? Direccion
    {
        get;
        set;
    }
    public Colegio Colegio {get; set;}
    //Para la relacion muchos a muchos
    public IEnumerable<ColegioProfesor> ColegiosProfesor { get; set; }
    public IEnumerable<ColegiosAlumnos> ColegiosAlumnos { get; set; }
    public IEnumerable<MateriaLista> ListaMaterias { get; set; }
}