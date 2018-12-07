using System;
using System.Collections;
using System.Collections.Generic;

namespace Adresar
{
    class Program
    {
        static void Main(string[] args)
        {
            var adressDictionary = new Dictionary<string, Tuple<string, string>>();
            var number = "234";
            var adressName = new Tuple<string, string>("Laura Bevc", "adresa");
            adressDictionary[number] = adressName;
            var number2 = "2222";
            var adressName2 = new Tuple<string, string>("Mate Matic","adresa2");
            adressDictionary[number2] = adressName2;

            //Console.WriteLine(str.Replace(" ",""));
            var exit = false;
            do
            {
                int option = -1;
                do
                {
                    Console.WriteLine("Odaberi jednu od 6 opcija:" +
                        "\n1 Dodaj novi upis" +
                        "\n2 Promjeni ime, adresu, broj" +
                        "\n3 Brisi upis" +
                        "\n4 Pretraga po broju" +
                        "\n5 Pretraga po imenu" +
                        "\n6 Izadi iz programa");
                    option = int.Parse(Console.ReadLine());
                } while (!(option == 1 || option == 2 || option == 3 || option == 4 || option == 5 || option == 6));
                if (option == 1)
                {
                    var newPerson = NewElement(adressDictionary);
                    adressDictionary[newPerson.Item1] = newPerson.Item2;
                    showElements(adressDictionary);
                }
                else if (option == 2) {
                    showElements(adressDictionary);
                    adressDictionary = ChangeNameAdressNumber(adressDictionary);
                    showElements(adressDictionary);
                }
                else if (option == 3) {
                    adressDictionary = DeleteElement(adressDictionary);
                    showElements(adressDictionary);
                }
                else if (option == 4) {
                    SortElements(adressDictionary);
                    var person = FindByNumber(adressDictionary);
                    Console.WriteLine($"{person.Item1}-{person.Item2.Item1}-{person.Item2.Item2}");
                    
                }
                else if (option == 5) {
                    SortElements(adressDictionary);
                    var listOfChosen = FindByName(adressDictionary);
                    foreach (var person in listOfChosen)
                    {
                        if (person.Item1 != "")
                        {
                            Console.WriteLine($"Odabrao si {person.Item1}-{person.Item2.Item1}-{person.Item2.Item2}");
                        }
                        else
                        {
                            Console.WriteLine("Takva osoba ne postoji");
                        }
                    }

                }
                else {
                    exit = true;
                }
            }while (!exit);



        }

        static Tuple<string, Tuple<string, string>> FindByNumber(Dictionary<string, Tuple<string, string>> dic) {
            Console.WriteLine("Unesi broj osobe koju trazis: ");
            var number=Console.ReadLine().Replace(" ", "");
            while (!dic.ContainsKey(number)) {

                Console.WriteLine("Unesi broj osobe koju trazis: ");
                number = Console.ReadLine().Replace(" ", "");
            }
            var chosenPerson = dic[number];
            return new Tuple<string, Tuple<string, string>>(number,chosenPerson);
        }
        static List<Tuple<string, Tuple<string, string>>> FindByName(Dictionary<string, Tuple<string, string>> dic) {
            Console.WriteLine("Unesi ime/prezime osobe koju trazis: ");
            var list = new List<Tuple<string, Tuple<string, string>>>();
            var chosenName = Console.ReadLine();
            var listOfNamesSurnames = new List<string>();
            var tupleNameAdress = new Tuple<string, string>(" "," ");
            var tuplePerson = new Tuple<string, Tuple<string, string>>("",tupleNameAdress);
            foreach (var pair in dic)
            {
                string nameAndSurname = pair.Value.Item1;
                listOfNamesSurnames.Add(nameAndSurname);
            }
            foreach (var pair in dic) {
                var name = pair.Value.Item1.Split(" ")[0];
                var surname = " ";
                try
                {
                    surname = pair.Value.Item1.Split(" ")[1];
                }
                catch { }
                if (name.ToLower() == chosenName.ToLower() || surname.ToLower() == chosenName.ToLower()) {
                    tupleNameAdress = new Tuple<string, string>(pair.Value.Item1,pair.Value.Item2);
                    tuplePerson = new Tuple<string, Tuple<string, string>>(pair.Key.Replace(" ", ""), tupleNameAdress);
                    list.Add(tuplePerson);
                }
            }
            return list;
        }
        static void SortElements(Dictionary<string, Tuple<string, string>> dic) {
            Console.WriteLine("Sortirani adresar:");
            var listOfnames = new List<string>();
            foreach (var pair in dic) {
                string nameAndSurname = pair.Value.Item1;
                listOfnames.Add(nameAndSurname);
            }
            listOfnames.Sort();
            foreach (var name in listOfnames) {
                foreach (var pair in dic) {
                    if (name == pair.Value.Item1) {
                        Console.WriteLine($"{pair.Key.Replace(" ", "")}-{pair.Value.Item1}-{pair.Value.Item2}");
                    }
                        }
            }
            
        }
        static Dictionary<string, Tuple<string, string>> DeleteElement(Dictionary<string, Tuple<string, string>> dic)
        {
            var index = dic.Values.Count;
            var chosenElementIndex = -1;
            while (chosenElementIndex <= 0 || chosenElementIndex > index)
            {
                Console.WriteLine($"Odaberi osobu od 1 do {index} koju zelis brisati:");
                chosenElementIndex = int.Parse(Console.ReadLine());

            }
            var i = 1;
            var chosenPersonNumber = "";
            foreach (var pair in dic)
            {
                if (index == i)
                    chosenPersonNumber = pair.Key.Replace(" ", "");
                
                i++;
            }
            Console.WriteLine("Unesi njen broj za potvrdu: ");
            if (Console.ReadLine().Replace(" ", "") == chosenPersonNumber.Replace(" ", ""))
            {
                Console.WriteLine("Unesi njen broj za potvrdu: ");
                if (Console.ReadLine().Replace(" ", "") == chosenPersonNumber.Replace(" ", ""))
                {
                    dic.Remove(chosenPersonNumber.Replace(" ", ""));
                }
                else
                {
                    Console.WriteLine("Krivo ponovljen broj. Promjene nisu spremljene");
                }
            }
            else
            {
                Console.WriteLine("Krivo ponovljen broj. Promjene nisu spremljene");
            }
            return dic;
            }
        static Tuple<string, Tuple<string, string>> NewElement(Dictionary<string, Tuple<string, string>> dic) {
            Console.WriteLine("Unesi ime: ");
            var name = Console.ReadLine();
            Console.WriteLine("Unesi adresu: ");
            var adress = Console.ReadLine();
            Console.WriteLine("Unesi broj: ");
            var number = Console.ReadLine().Replace(" ", "");
            Console.WriteLine("Unesi broj ponovo: ");
            var numberAgain = Console.ReadLine().Replace(" ", "");
            Console.WriteLine("Unesi broj ponovo: ");
            var numberAgain2 = Console.ReadLine().Replace(" ", "");
            bool uniqueNumber = true;
            foreach (var pair in dic)
            {
                if (pair.Key.Replace(" ", "") == number)
                {
                    uniqueNumber = false;
                    break;
                }
                else {
                    uniqueNumber = true;
                }
                
            }
            while (number != numberAgain || number!=numberAgain2 || uniqueNumber==false) {
                foreach (var pair in dic)
                {
                    if (pair.Key == number)
                        uniqueNumber = false;
                    else
                        uniqueNumber = true;
                }
                Console.WriteLine("Moras unijeti tri puta isti broj koji je jedinstven!");
                Console.WriteLine("Unesi broj: ");
                number =Console.ReadLine().Replace(" ", "");
                Console.WriteLine("Unesi broj ponovo: ");
                numberAgain = Console.ReadLine().Replace(" ", "");
                Console.WriteLine("Unesi broj ponovo: ");
                numberAgain2 = Console.ReadLine().Replace(" ", "");
            }
            var newNameAdress = new Tuple<string, string>(name, adress);
            return new Tuple<string, Tuple<string, string>>(number, newNameAdress);
        }
        static Dictionary<string,Tuple<string,string>> ChangeNameAdressNumber(Dictionary<string, Tuple<string, string>> dic)
        {
            var index = dic.Values.Count ;
            var chosenElementIndex = -1;
            while(chosenElementIndex<=0 || chosenElementIndex>index)
            {
                Console.WriteLine($"Odaberi osobu od 1 do {index} kojoj zelis mijenjati podatke:");
                 chosenElementIndex = int.Parse(Console.ReadLine());

            }
            var i = 1;
            var chosenPersonNumber = "";
            var chosenPerson = new Tuple<string, string>("","");
            foreach (var pair in dic) {
                if (index == i)
                {
                    chosenPersonNumber = pair.Key.Replace(" ", "");
                    chosenPerson = new Tuple<string, string>(pair.Value.Item1,pair.Value.Item2);
                }
                i++;
            }

            
            Console.WriteLine("Sto zelis mijenjati: 1-ime 2-adresu 3-broj");
            var change = int.Parse(Console.ReadLine());
            while (change != 1 && change != 2 && change != 3) {
                Console.WriteLine("Sto zelis mijenjati: 1-ime 2-adresu 3-broj");
                change = int.Parse(Console.ReadLine());
            }
            if (change == 1) {
                Console.WriteLine("Novo ime je: ");
                var name = Console.ReadLine();
                Console.WriteLine("Potvrdi ponavljanjem broja: ");
                if (Console.ReadLine().Replace(" ", "") == chosenPersonNumber)
                {
                    chosenPerson = new Tuple<string, string>(name, chosenPerson.Item2);
                    dic[chosenPersonNumber] = chosenPerson;
                }
                else {
                    Console.WriteLine("Nisi dobro potvrdio. Promjena ponistena!");
                }

            }
            else if (change == 2) {
                Console.WriteLine("Nova adresa je: ");
                var adress = Console.ReadLine();
                Console.WriteLine("Potvrdi ponavljanjem broja: ");
                if (Console.ReadLine().Replace(" ", "") == chosenPersonNumber)
                {
                    chosenPerson = new Tuple<string, string>( chosenPerson.Item1,adress);
                    dic[chosenPersonNumber] = chosenPerson;
                }
                else
                {
                    Console.WriteLine("Nisi dobro potvrdio. Promjena ponistena!");
                }
            }
            else {
                Console.WriteLine("Novi broj je: ");
                var newNumber = Console.ReadLine().Replace(" ", "");
                Console.WriteLine("Potvrdi ponavljanjem broja: ");
                if (Console.ReadLine().Replace(" ", "") == newNumber && dic.ContainsKey(newNumber)==false)
                {
                    Console.WriteLine("Potvrdi ponavljanjem broja: ");
                    if (Console.ReadLine().Replace(" ", "") == newNumber)
                    {
                        dic.Remove(chosenPersonNumber);
                        dic.Add(newNumber,chosenPerson);
           

                    }
                    else
                    {
                        Console.WriteLine("Nisi dobro potvrdio. Promjena ponistena!");
                    }
                }
                else
                {
                    Console.WriteLine("Nisi dobro potvrdio ili broj vec postoji. Promjena ponistena!");
                }

            }
            return dic;

        }
        static void showElements(Dictionary<string,Tuple<string,string>>dic) {
            foreach (var pair in dic) {
                Console.WriteLine($"{pair.Key.Replace(" ", "")} {pair.Value.Item1} {pair.Value.Item2}");
            }
        }
    }
}
