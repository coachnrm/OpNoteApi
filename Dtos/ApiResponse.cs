using System;

namespace OpNoteApi.Dtos;

public class ApiResponse
{
    public int ResponseCode {get; set;}
    public string Result {get; set;}
    public string Errormessage {get; set;}
}
