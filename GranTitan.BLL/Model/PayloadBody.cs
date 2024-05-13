using System.Collections.Generic;

namespace GranTitan.BLL.Model
{
    public abstract class PayloadBody
    {
        public List<SubError>? Error { get; set; }
        public List<SubError>? Errors { get; set; }        
    }
    public class SubError
    {
        public string? Field { get; set; }
        public string? Message { get; set; }
    }
}
