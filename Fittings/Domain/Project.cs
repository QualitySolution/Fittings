﻿using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Collections.Generic;
using System.Linq;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "проекты",
		Nominative = "проект")]
	public class Project: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string customer;

		public virtual string Customer {
			get { return customer; }
			set { SetField (ref customer, value, () => Customer); }
		}

		string projectName;

		[PropertyChangedAlso("Title")]
		public virtual string ProjectName {
			get { return projectName; }
			set { SetField (ref projectName, value, () => ProjectName); }
		}

		IList<ProjectItem> projectRows = new List<ProjectItem> ();

		[Display (Name = "Проекты")]
		public virtual IList<ProjectItem> ProjectRows {
			get { return projectRows; }
			set { SetField (ref projectRows, value, () => ProjectRows); }
		}

		GenericObservableList<ProjectItem> observableProjectRows;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<ProjectItem> ObservableProjectRows {
			get {
				if (observableProjectRows == null)
					observableProjectRows = new GenericObservableList<ProjectItem> (ProjectRows);
				return observableProjectRows;
			}
		}

		#endregion

		public virtual string Title {
			get{
				return ProjectName;
			}
		}

		public Project ()
		{
			
		}

		#region Функции

		public virtual void AddItem(Fitting fitting){
			int seq = 1;
			if (ProjectRows.Count > 0)
				seq = ProjectRows.Max (x => x.SequenceNumber)+1;
			var item = new ProjectItem {
				Project = this,
				Fitting = fitting,
				SequenceNumber = seq
			};
			ObservableProjectRows.Add (item);
		}

		#endregion
	}
}

