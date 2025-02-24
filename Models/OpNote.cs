using System;

namespace OpNoteApi.Models;

public class OpNote
{
    public int Id {get; set;}
    public string Hn {get; set;}
    public string An {get; set;}
    public DateOnly OpDate {get; set;}
    public TimeOnly StartTime {get; set;}
    public TimeOnly EndTime {get; set;}
    // public string Surgeon {get; set;}
    public List<string> Surgeons { get; set; } = new List<string>();
    // public string Assist {get; set;}
    public List<string> Assist { get; set; } = new List<string>();
    public string ScrubNurse {get; set;}
    public string CirculateNurse {get; set;}
    public string Anesthesiologist {get; set;}
    public string NurseAnesthetis {get; set;}
    public string PreopDx {get; set;}
    public string PostopDx {get; set;}
    public string Operation {get; set;}
    public string OpFinding {get; set;}
    public string Procedure {get; set;}
    public string Patho {get; set;}
    public int BloodLoss {get; set;}
}
