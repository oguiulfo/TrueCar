
using System.Collections.Generic;

namespace TrueCarChallenge.Models
{
    public class MakesViewModel
    {
        public MakesViewModel()
        {
            Makes = new List<Make>();
        }

        public string ErrorMessage { get; set; }
        public IList<Make> Makes { get; set; }
    }
}