using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "типы соединений",
		Nominative = "тип соединений")]
	public class ConnectionType: MultiLanguageReferenceBase
	{
		public ConnectionType ()
		{
			
		}
	}
}

