using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spis_Gier.Models
{

    public class Games
    {
        string _Platformm;

        public int Lp { get; set; }
        public int Status { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Platform
        {
            get { return _Platformm; }
            set { _Platformm = value; }
        }

        public string Edition { get; set; }
        public string Srv { get; set; }
        public string DatePremiere { get; set; }
        public int EndGame { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }

        public string ColorDot
        {
            get
            {
                if (EndGame == 1)
                { return "Green"; }
                else { return "Red"; }
            }
        }



    }


}




