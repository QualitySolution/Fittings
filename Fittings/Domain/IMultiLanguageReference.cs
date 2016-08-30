using System;

namespace Fittings.Domain
{
	public interface IMultiLanguageReference
	{
		int Id { get; set; }
		string NameRus{ get; set;}
		string NameEng{ get; set;}
	}
}

