using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

Delivery home = new HomeDelivery("Рогожникова, 42");
Delivery pickpoint = new PickPointDelivery("Набережная, 6");
Delivery shop = new ShopDelivery("Северная, 68");

Person<int> anna = new Person<int>("Aннa", "Иванова", "555-55-55", 24);
Person<string> ivan = new Person<string>("Иван", "Щеглов", "565-595-545", "K234H");
Employee oleg = new Employee("Олег", "Пупкин", "372-846-832", "324", 5);
Item item1 = new Item(400);


Console.WriteLine($"ID Анны {anna.ID}");
Console.WriteLine($"ID Ивана {ivan.ID}");

string id1;
string id2;
Console.WriteLine("Введите ID 1 для обмена");
id1 = Console.ReadLine();
Console.WriteLine("Введите ID 2 для обмена");
id2 = Console.ReadLine();

SwapID<string>(ref id1, ref id2); // вызов метода с обобщением

Console.WriteLine($"Теперь ID 1 {id1}, а ID 2 {id2}");

Console.WriteLine($" Вес посылки {item1.Weight}");

home.Job();         
pickpoint.Job();        
shop.Job();
oleg.Salary(); // считает зарплату олега умнажая статический элемент класса Employee на количестве смен 

Console.ReadKey();



void SwapID<T>( ref T id1,  ref T id2)      // метод с обобщением
{
    T trmpid = id1;
    id1 = id2;
    id2 = trmpid;
}

abstract class Delivery
{
    public string Address { get; }
    // конструктор абстрактного класса Delivery
    public Delivery(string address)
    {
        Address = address;
    }
    public abstract void Job(); // абстрактный метод
}
class HomeDelivery : Delivery // наследование от класса Delivery
{
    public HomeDelivery(string address) : base(address) { }
    public override void Job() => Console.WriteLine($"Посылка доставлена на дом по адресу {Address}"); // переопределение абстрактного метода
}
class PickPointDelivery : Delivery // наследование от класса Delivery
{
    public PickPointDelivery(string address) : base(address) { }
    public override void Job() => Console.WriteLine($"Посылка в пункт выдачи по адресу {Address}");
}
class ShopDelivery : Delivery // наследование от класса Delivery
{
    public ShopDelivery(string address) : base(address) { }
    public override void Job() => Console.WriteLine($"Посылка доставлена в магазин по адресу {Address}");
}

public class Person<T>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public T ID { get; set; }  // обобщенный тип
    public Person(string name, string surname, string phone, T id) 
    {
        Name = name;
        Surname = surname;
        Phone = phone;
        ID = id;
    }
}

public class Employee : Person<string> // явное определение наследуемого типа
{ 
    public int Shifts { get; set; }

    private static int payonshift = 2000; // статический член класса. оплата за 1 смену имеет модификатор private потому что очень секретно
    public Employee(string name, string surename, string phone, string id, int shifts)
       : base(name, surename, phone, id)
    {
        Shifts = shifts;
    }
    public void Salary() => Console.WriteLine($"Зарплата {Name} в этом месяце {Shifts * payonshift}"); 
    
}


class Item
{
    public int Weight;
  
    public Item(int weight)
    { 
        Weight = weight;
    }
}

class Order   //композиция классов. Item не существует отдельно от Order
{
    string type = "Хрупкое";
    Item item1;
    public Order()
    { 
        this.item1 = new Item(200);
    }
}

class Pakege
{
    private int size;
    public int Sizr
    {
        get
        { 
            return size;
        }
        set
        {
            if (value > 0)    // если число отрицательное запись в поле не происходит
            { 
                size = value;
            }
        }
    }
}