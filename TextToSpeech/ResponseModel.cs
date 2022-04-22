using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech
{
    public class ResponseModel
    {
        public string async { get; set; }
        public int error { get; set; }
        public string message { get; set; }
        public string request_id { get; set; }
    }
}
