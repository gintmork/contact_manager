using System;
using System.Collections.Generic;
using System.IO;

namespace contact_manager
{
	class Program
	{

		static void Main(string[] args)
		{
			string line = "";
			int counter = 0;
			string firstRow = "";
			string path = "../../contacts.txt";


			Console.WriteLine("Contact Manager\r");
			Console.WriteLine("------------------------\n");

			Console.WriteLine("All Contacts\r");
			counter = List(line, firstRow, path, counter);

			Options(line, firstRow, path, counter);
		}

		public static int List(string line, string firstRow, string path, int counter)
		{
			StreamReader file = new System.IO.StreamReader(path);

			firstRow = String.Format("|{0,20}|{1,20}|{2,20}|{3,20}|", "Name", "Last Name", "Phone Number", "Address");
			Console.WriteLine(firstRow);
			Console.WriteLine("|--------------------|--------------------|--------------------|--------------------|");

			while ((line = file.ReadLine()) != null)
			{
				string[] data = new string[4];
				string row = "";
				data = line.Split(',');
				row = String.Format("|{0,20}|{1,20}|{2,20}|{3,20}|", data[0], data[1], data[2], data[3]);
				Console.WriteLine(row);
				counter++;
			}
			Console.WriteLine("-------------------------------------------------------------------------------------");
			file.Close();

			return counter;
		}

		static void Options(string line, string firstRow, string path, int counter)
		{
			Console.WriteLine("Choose an option from the following list:");
			Console.WriteLine("\ta - Add contact");
			Console.WriteLine("\td - Delete contact");
			Console.WriteLine("\tu - Update contact");
			Console.WriteLine("\tc - Close");
			Console.Write("Your option? ");

			switch (Console.ReadLine())
			{
				case "a":
					Add(line, path, counter);
					counter = List(line, firstRow, path, counter);
					Options(line, firstRow, path, counter);
					break;
				case "d":
					int rowNumber;
					Console.WriteLine("Enter contact row which you want to delete: ");
					rowNumber = Convert.ToInt32(Console.ReadLine());

					Delete(counter, rowNumber, path);
					counter = List(line, firstRow, path, counter);
					Options(line, firstRow, path, counter);
					break;

				case "u":
					int rowNumberUpdate;
					Console.WriteLine("Enter contact row which you want to update: ");
					rowNumberUpdate = Convert.ToInt32(Console.ReadLine());
					Update(counter, rowNumberUpdate, path);
					break;
				case "c":
					break;
			}
		}

		static void Add(string line, string path, int counter)
		{
			string name, lastname, phoneNumber, address;

			Console.WriteLine("Enter name: ");
			name = Console.ReadLine();

			Console.WriteLine("Enter last name: ");
			lastname = Console.ReadLine();

			Console.WriteLine("Enter phone number: ");
			phoneNumber = Console.ReadLine();

			StreamReader fileForAdd = new System.IO.StreamReader(path);
			for (int i = 0; i < counter; i++)
			{
				line = fileForAdd.ReadLine();
				string[] data = new string[4];
				data = line.Split(',');
				if (data[2] == phoneNumber)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Phone number already exists! Please enter different phone number: ");
					Console.ResetColor();
					phoneNumber = Console.ReadLine();
					i = -1;
					fileForAdd = new System.IO.StreamReader(path);
				}
			}
			fileForAdd.Close();

			Console.WriteLine("Enter address: ");
			address = Console.ReadLine();

			var row = name + "," + lastname + "," + phoneNumber + "," + address;

			System.IO.File.AppendAllText(path, string.Format("{0}{1}", row, Environment.NewLine));

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Contact successfully added!");
			Console.ResetColor();
		}

		static void Delete(int counter, int rowNumber, string path)
		{

			if (rowNumber > counter || rowNumber < 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("This number contact row does not exists. Enter existing row: ");
				Console.ResetColor();
				rowNumber = Convert.ToInt32(Console.ReadLine());
				Delete(counter, rowNumber, path);
			}
			else
			{
				var file = new List<string>(System.IO.File.ReadAllLines(path));
				file.RemoveAt(rowNumber - 1);
				File.WriteAllLines(path, file.ToArray());


				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Contact successfully deleted!");
				Console.ResetColor();

			}

		}

		static void Update(int counter, int rowNumber, string path)
		{
			if (rowNumber > counter || rowNumber < 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("This number contact row does not exist. Enter existing row: ");
				Console.ResetColor();
				rowNumber = Convert.ToInt32(Console.ReadLine());
				Update(counter, rowNumber, path);
			}
			else
			{
				Console.WriteLine("Choose what you want to update:");
				Console.WriteLine("\tn - Name");
				Console.WriteLine("\tl - Last Name");
				Console.WriteLine("\tp - Phone Number");
				Console.WriteLine("\ta - Address");
				Console.WriteLine("\td - Done");
				Console.Write("Your option? ");

				switch (Console.ReadLine())
				{
					case "n":
						Console.WriteLine("Enter new name: ");
						string newName = Console.ReadLine();
						UpdateName(counter, rowNumber, path, newName);
						string line = "";
						string firstRow = "";
						Options(line, firstRow, path, counter);
						break;

					case "l":
						Console.WriteLine("Enter new last name: ");
						string newLastName = Console.ReadLine();
						UpdateLastName(counter, rowNumber, path, newLastName);
						line = "";
						firstRow = "";
						Options(line, firstRow, path, counter);
						break;

					case "p":
						Console.WriteLine("Enter new phone number: ");
						string phoneNumber = Console.ReadLine();
						UpdatePhoneNumber(counter, rowNumber, path, phoneNumber);
						line = "";
						firstRow = "";
						Options(line, firstRow, path, counter);
						break;

					case "a":
						Console.WriteLine("Enter new address: ");
						string newAddress = Console.ReadLine();
						UpdateAddress(counter, rowNumber, path, newAddress);
						line = "";
						firstRow = "";
						Options(line, firstRow, path, counter);
						break;

					case "d":
						break;
				}
			}
		}

		static void UpdateName(int counter, int rowNumber, string path, string newName)
		{
			string[] arr = File.ReadAllLines(path);

			string line = arr[rowNumber - 1];
			string[] data = new string[4];
			data = line.Split(',');
			line = line.Replace(data[0], newName);
			arr[rowNumber - 1] = line;
			File.WriteAllLines(path, arr);
		}

		static void UpdateLastName(int counter, int rowNumber, string path, string newLastName)
		{
			string[] arr = File.ReadAllLines(path);

			string line = arr[rowNumber - 1];
			string[] data = new string[4];
			data = line.Split(',');
			line = line.Replace(data[1], newLastName);
			arr[rowNumber - 1] = line;
			File.WriteAllLines(path, arr);
		}

		static void UpdatePhoneNumber(int counter, int rowNumber, string path, string phoneNumber)
		{
			if (phoneNumber == null)
			{
				phoneNumber = Console.ReadLine();
			}

			string[] arr = File.ReadAllLines(path);
			string line1 = arr[rowNumber - 1];
			string[] data = new string[4];
			data = line1.Split(',');

			if (data[2] == phoneNumber)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Phone number already exists! Please enter different phone number: ");
				Console.ResetColor();

				UpdatePhoneNumber(counter, rowNumber, path, null);
			}

			line1 = line1.Replace(data[2], phoneNumber);
			arr[rowNumber - 1] = line1;

			File.WriteAllLines(path, arr);
		}

		static void UpdateAddress(int counter, int rowNumber, string path, string newAddress)
		{
			string[] arr = File.ReadAllLines(path);

			string line = arr[rowNumber - 1];
			string[] data = new string[4];
			data = line.Split(',');
			line = line.Replace(data[3], newAddress);
			arr[rowNumber - 1] = line;
			File.WriteAllLines(path, arr);
		}
	}
}
