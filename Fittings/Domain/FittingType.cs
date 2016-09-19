using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "виды арматуры",
		Nominative = "вид арматуры")]
	public class FittingType: MultiLanguageReferenceBase
	{
		public FittingType ()
		{

		}

		string modelCode;

		public virtual string ModelCode {
			get { return modelCode; }
			set { SetField (ref modelCode, value, () => ModelCode); }
		}
	}
}

