using System;
using QSOrmProject;

namespace Fittings.Domain
{
	public interface IMultiLanguageReference: IDomainObject
	{
		int Id { get; set; }
		string NameRus{ get; set;}
		string NameEng{ get; set;}
	}
}

