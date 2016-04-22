using System;
using System.Collections.Generic;

namespace CustomerDataBridge {
	/// <summary>
	/// Entry point into console application
	/// </summary>
	class MainApp {
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args) {
			//Create RefinedAbstraction
			Customers customers = new Customers("Chicago");
			//set ConcreteImplementor
			customers.Data = new CustomersData();
			//Excercise the bridge
			for (int i = 0; i < 2; i++) {
				customers.Show();
				customers.Next();
			}
			customers.Show();
			customers.Add("Henry Velasquez");

			customers.ShowAll();

			//wait for uesr
			Console.ReadKey();
		}
	}

	/// <summary>
	/// The "Abstraction" class
	/// </summary>
	class CustomerBase {
		private DataObject _dataObject{get;set;}
		protected string group { get; set; }

		public CustomerBase(string group) {
			this.group = group;
		}

		//Property
		public DataObject Data {
			set {_dataObject = value;}
			get {return _dataObject;}
		}

		public virtual void Next() {
			_dataObject.NextRecord();
		}

		public virtual void Prior(){
			_dataObject.PriorRecord();
		}

		public virtual void Add(string customer) {
			_dataObject.AddRecord(customer);
		}

		public void Delete(string customer) {
			_dataObject.DeleteRecord(customer);
		}

		public virtual void Show() {
			_dataObject.ShowRecord();
		}

		public virtual void ShowAll() {
			Console.WriteLine("Customer Group: "+group);
			_dataObject.ShowAllRecords();
		}
	}

	/// <summary>
	/// The "RefinedAbstraction" class
	/// </summary>
	/// <seealso cref="CustomerDataBridge.CustomerBase" />
	class Customers:CustomerBase{
		//Constructor
		public Customers(string group) : base(group) { }
		public override void ShowAll() {
			//Add separator lines
			Console.WriteLine("\n------------------------------");
			base.ShowAll();
			Console.WriteLine("------------------------------");
		}
	}

	/// <summary>
	/// The "ConcreteImplementor" class
	/// </summary>
	/// <seealso cref="CustomerDataBridge.DataObject" />
	class CustomersData : DataObject {
		private List<string> _customers = new List<string>();
		private int _current = 0;

		public CustomersData(){
			//Loaded from a database
			_customers.Add("Jim Jones");
			_customers.Add("Samuel Jackson");
			_customers.Add("Allen Good");
			_customers.Add("Ann Stills");
			_customers.Add("List Giolani");
		}

		public override void NextRecord() {
			if (_current <= _customers.Count - 1) _current++;
		}

		public override void PriorRecord() {
			if (_current > 0) _current--;
		}

		public override void AddRecord(string name) {
			_customers.Add(name);
		}

		public override void DeleteRecord(string name) {
			_customers.Remove(name);
		}

		public override void ShowRecord() {
			Console.WriteLine(_customers[_current]);
		}

		public override void ShowAllRecords() {
			foreach (string customer in _customers) {
				Console.WriteLine(" "+customer);
			}
		}
	}

	/// <summary>
	/// The "Implementor" abstract class
	/// </summary>
	abstract class DataObject{
		public abstract void NextRecord();
		public abstract void PriorRecord();
		public abstract void AddRecord(string name);
		public abstract void DeleteRecord(string name);
		public abstract void ShowRecord();
		public abstract void ShowAllRecords();
	}

}