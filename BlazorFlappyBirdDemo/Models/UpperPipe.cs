using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlappyBirdDemo.Models
{
    public class UpperPipe : PipeModel
    {
        //public int RightDistanceFromBottomPipe { get; private set; } = new Random().Next(1, 40);
        public override int Gap
        {
            get
            {
                return 430 - 150 + DistanceFromBottom; // pipe gap - ground height + pipe distance from bottom
            }
        }
    }
}
