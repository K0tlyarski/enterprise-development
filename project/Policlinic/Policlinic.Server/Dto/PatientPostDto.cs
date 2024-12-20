﻿namespace Policlinic.Server.Dto;

public class PatientPostDto
{

    public long Passport { get; set; }

    public string Fio { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    public string Address { get; set; } = string.Empty;
}