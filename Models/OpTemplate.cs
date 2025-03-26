using System;

namespace OpNoteApi.Models;

public class OpTemplate
{
    public int Id {get; set;}
    public string DoctorCode {get; set;}
    public string TemplateName {get; set;}
    public string Operation {get; set;}
    public string Procedure {get; set;}
}
