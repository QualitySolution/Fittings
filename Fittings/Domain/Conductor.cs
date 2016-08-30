using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "проводимые среды",
		Nominative = "проводимая среда")]
	public class Conductor: MultiLanguageReferenceBase
	{
		public Conductor ()
		{
			
		}
	}
}

