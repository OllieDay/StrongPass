using System;
using System.Collections.Generic;
using System.IO;

namespace StrongPass
{
	public sealed class StrongPassOptions
	{
		private Func<IEnumerable<string>> _passwordBuilder;

		public string Code { get; set; }
		public string Description { get; set; }

		public void FromFile(string file)
		{
			_passwordBuilder = () => File.ReadAllLines(file);
		}

		public void FromCollection(IEnumerable<string> passwords)
		{
			_passwordBuilder = () => passwords;
		}

		internal ISet<string> BuildPasswords()
		{
			return new HashSet<string>(_passwordBuilder());
		}
	}
}
