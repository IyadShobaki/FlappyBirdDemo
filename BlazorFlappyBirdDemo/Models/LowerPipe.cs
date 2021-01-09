using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlappyBirdDemo.Models
{
    public class LowerPipe : PipeModel
    {
        public override int Gap
        {
            get
            {
                return 300 - 150 + DistanceFromBottom; // pipe height - ground height + pipe distance from bottom
            }
        }
    }
}
