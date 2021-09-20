using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures
{
    struct Point
    {
        public uint x;
        public uint y;
        public uint Value;
        public Point(uint x, uint y, uint RGBA)
        {
            this.x = x;
            this.y = y;
            Value = RGBA;
        }
    }
}
