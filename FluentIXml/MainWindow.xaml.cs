using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FluentIXml
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new Primer();
		}
	}
	}

	public class Primer : INotifyPropertyChanged, IDataErrorInfo
	{
		private Validator v = new Validator();

		private string _nesto;

		public string this[string columnName]
		{
			get
			{
				var greska = v.Validate(this).Errors.FirstOrDefault(er => er.PropertyName == columnName);
				return greska != null ? greska.ErrorMessage : "";
			}
		}

		public string Nesto
		{
			get => _nesto;
			set
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nesto"));
				_nesto = value;
			}
		}

		//Ne koristi se u wpf pa slobodno na exception
		public string Error => throw new NotImplementedException();
		public int bla { get; set; }
		public string x { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class Validator : AbstractValidator<Primer>
	{
		public Validator()
		{
			RuleFor(p => p.Nesto).MinimumLength(3).WithMessage("Premalo")
								 .MaximumLength(8).WithMessage("Previse!");
		}
	}
}
