using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Cognitive
{
    class IdentifiedPerson
    {
        public double Confidence
        {
            get; set;
        }

        public Person Person
        {
            get; set;
        }

        public Guid FaceId
        {
            get; set;
        }
    }
}
