using System;

namespace OpNoteApi.Models;

public class OpPicture
{
    public int Id {get; set;}
    public string Hn {get; set;}
    public string An {get; set;}
    public string OpType {get; set;}
    public byte[] Content {get; set;}
}
