﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Events
{
    public interface OnRoundTripEvent
    {
        void RoundTrip(ref int __result);
    }
}
 