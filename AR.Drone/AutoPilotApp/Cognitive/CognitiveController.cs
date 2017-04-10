using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp
{
    public class CognitiveController
    {
        Bitmaps input;
        CognitiveData output;
        public CognitiveController(Bitmaps input, CognitiveData output)
        {
            this.input = input;
            this.output = output;

        }
    }
}
