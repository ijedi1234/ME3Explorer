using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OodleWrapper.Enums {
    public enum OodleCompressionLevel {
		HyperFast4 = -4,
		HyperFast3,
		HyperFast2,
		HyperFast1,
		None,
		SuperFast,
		VeryFast,
		Fast,
		Normal,
		Optimal1,
		Optimal2,
		Optimal3,
		Optimal4,
		Optimal5,

		Min = HyperFast4,
		Max = Optimal5
	}
}
