using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "материалы корпусов",
		Nominative = "материал корпуса")]
	public class BodyMaterial: MultiLanguageReferenceBase
	{
		public BodyMaterial ()
		{
			
		}
	}
}

