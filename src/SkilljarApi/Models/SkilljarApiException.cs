using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{
    /// <summary>
    /// Represents an Error that occurs from the Skilljar API
    /// </summary>
    public class SkilljarApiException : Exception
    {
        public SkilljarApiException() { }
        public SkilljarApiException(string message) : base(message) { }
        public SkilljarApiException(Exception ex) : base(ex.Message, ex.InnerException) { }
    }
}
