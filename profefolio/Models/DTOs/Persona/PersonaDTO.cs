﻿using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Persona;
// ReSharper disable once InconsistentNaming
public class PersonaDTO : DataDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    public string? Nombre
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El apellido es requerido")]
    public string? Apellido
    {
        get;
        set;
    }

    
    [Required(ErrorMessage = "El Nacimiento es requerido")]
    public DateTime Nacimiento
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El Documento es requerido")]
    public string? Documento
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El Tipo de Documento es requerido")]
    public string? DocumentoTipo
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El genero es requerido")]
    public string? Genero
    {
        get;
        set;
    }

   
    public string? Direccion
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El telefono es requerido")]
    public string? Telefono
    {
        get;
        set;
    }

    [Required(ErrorMessage = "la contraseña es requerida")]
    public string? Password
    {
        get;
        set;
    }

    [Required(ErrorMessage = "necesitas confirmar la contraseña")]
    [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
    public string? ConfirmPassword
    {
        get;
        set;
    }

    [Required(ErrorMessage = "El Email es requerido")]
    [EmailAddress(ErrorMessage = "El email debe ser de formato Mail")]
    public string? Email
    {
        get;
        set;
    }
}