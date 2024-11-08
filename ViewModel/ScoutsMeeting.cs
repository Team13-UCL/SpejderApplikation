using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsMeeting
    {
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public byte[] Badge { get; set; }
        public string Activity { get; set; }
        public string Preparation { get; set; }
        public string Notes { get; set; }
        public ScoutsMeeting()
        {

        }
    }
}
