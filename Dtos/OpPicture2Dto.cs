using System;

namespace OpNoteApi.Dtos;

public class OpPicture2Dto
{
    public string Hn {get; set;}
    public string An {get; set;}
    public string? OpType {get; set;}
    public IFormFile? File {get; set;}
}
