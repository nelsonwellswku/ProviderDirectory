namespace Octogami.ProviderDirectory.Application.Domain
{
	public class State
	{
		public State()
		{
		}

		internal State(string name, string abbreviation)
		{
			Name = name;
			Abbreviation = abbreviation;
		}

		public string Name { get; set; }

		public string Abbreviation { get; set; }
	}
}