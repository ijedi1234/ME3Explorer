using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OodleWrapper.Enums {
    public enum DecodeThreadPhase {
		ThreadPhase1 = 0x1,
		ThreadPhase2 = 0x2,

		Unthreaded = ThreadPhase1 | ThreadPhase2
	}
}
