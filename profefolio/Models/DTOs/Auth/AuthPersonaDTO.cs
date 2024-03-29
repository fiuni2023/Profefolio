﻿namespace profefolio.Models.DTOs.Auth;
// ReSharper disable once InconsistentNaming
public class AuthPersonaDTO
{
    public string? Email
    {
        get; 
        set;
    }

    public List<string>? Roles
    {
        get;
        set;
    }

    public DateTime Expires
    {
        get;
        set;
    }

    public string? Token
    {
        get;
        set;
    }
    public int ColegioId { get; set; }
    public string ColegioNombre { get; set; } = "";
}

