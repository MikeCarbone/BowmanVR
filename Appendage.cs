using System;

namespace AssemblyCSharp
{
	public interface Appendage {
		int Damage { get; }
		string String { get; }
	}

	public class AppendageFactory {
		public static Appendage createAppendage(string name) {
			if (name.Equals ("armRight")||name.Equals ("armLeft")) {
				return new Arm ();
			} else if (name.Equals ("head")) {
				return new Head ();
			} else if (name.Equals ("chest")) {
				return new Chest ();
			}
			else if (name.Equals ("legRight")||name.Equals ("legLeft")) {
				return new Leg ();
			}
			return null;
		}
	}
	public class Leg : Appendage {

		public int Damage {
			get {
				return 25;
			}
		}

		public string String {
			get {
				return "leg";
			}
		}
	}
	public class Head : Appendage {

		public int Damage {
			get {
				return 90;
			}
		}

		public string String {
			get {
				return "head";
			}
		}
	}
	public class Arm : Appendage {

		public int Damage {
			get {
				return 25;
			}
		}

		public string String {
			get {
				return "armRight";
			}
		}
	}		
	public class Chest : Appendage {

		public int Damage {
			get {
				return 50;
			}
		}

		public string String {
			get {
				return "chest";
			}
		}
	}
	public class Human {
		private int _health;
		public int Health { get { return _health; }}
		public void hit(Appendage appendage) {
			_health -= appendage.Damage;
		}

	}
}

