using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Models
{
    public enum Movements
    {
        Hover,
        Left,
        Right,
        Ahead,
        Picture,
        Land,
        TakeOff,
        Up
    }

    public class Navigation : ObservableObject
    {
        bool[] movements = new bool[Enum.GetValues(typeof(Movements)).Length];

        public void SetMovement(Movements move)
        {
            for (int i = 0; i < movements.Length; i++)
            {
                movements[i] = false;
            }
            movements[(int)move] = true;
            RaiseAllPropertiesChanged();
        }

        public bool GoLeft
        {
            get { return movements[(int)Movements.Left]; }
        }
        public bool GoRight
        {
            get { return movements[(int)Movements.Right]; }
        }

        public bool GoAhead
        {
            get { return movements[(int)Movements.Ahead]; }
        }

        public bool TakePicture
        {
            get { return movements[(int)Movements.Picture]; }
        }

        public bool Land
        {
            get { return movements[(int)Movements.Land]; }
        }

        public bool Hover
        {
            get { return movements[(int)Movements.Hover]; }

        }

        public bool TakeOff
        {
            get { return movements[(int)Movements.TakeOff]; }

        }

        public bool Up
        {
            get { return movements[(int)Movements.Up]; }

        }
    }
}
