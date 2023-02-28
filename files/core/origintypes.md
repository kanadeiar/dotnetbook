# Основы типов

## Инкапсуляция

Такая характерная черта как инкапсуляция описывает способность языка скрывать излишние детали реализации от пользователя объекта.

Модификаторы доступа по всему C#:

Модификатор доступа | Может быть применен | Практический смысл
-------|------|------
public | Типы или члены типов | Открытые члены доступны из любого объекта, производного класса, другой сборки
private | Члены типов или вложенные типы | Доступны только классу, где определены
protected | Члены типов или вложенные типы | Доступны классу, где определены и любым дочерним классам
internal | Типы и члены типов | Доступны только внутри сборки
protected internal | Члены типов или вложенные типы | Доступен внутри сборки, внутри класса и производным классам внутри или вне сборки
private protected | Члены типов или вложенные типы | Доступен внутри класса и производным классам внутри сборки

## Наследование

Наследование — это аспект ООП, упрощающий повторное использование кода.

Все типы прямо или косвенно являются производными от System.Object. Все значимые типы наследуются от ValueType, а ValueType в свою очередь - от System.Object.

У класса System.Object есть следующие открытые экземплярные методы:

Equals() - сравнение двух объектов на равенство

GetHashCode() - возвращает хеш-код значения данного объекта

ToString() - возвращает полное имя типа, в некоторых типах переопределен

GetType() - невиртуальный метод возвращает экземпляр объекта, производнго от Type

Защищенные методы:

MemberwiseClone() - невиртуальный метод создает новый экземпляр типа и присваивает полям нового объекта соответствующие значения объекта this

Finalize() - Вызывается, когда уборщик мусора определяет, что объект является мусором, но до возвращения занятой объектом памяти в кучу

Любой производный от Object класс наследует сигнатуры методов и реализацию этих методов. Только один класс может быть базовым для другого класса.

```csharp
var h = 12.GetHashCode();
var b = 12.Equals(23);
var s = 12.ToString();
var t = 12.GetType();
```

Определение System.Object:

```csharp
public class Object
{
    // Виртуальные члены.
    public virtual bool Equals(object obj);
    protected virtual void Finalize();
    public virtual int GetHashCode();
    public virtual string ToString();
    // Невиртуальные члены уровня экземпляра.
    public Type GetType();
    protected object MemberwiseClone();
    // Статические члены.
    public static bool Equals(object objA, object objB);
    public static bool ReferenceEquals(object objA, object objB);
}
```

## Создание типа

Все объекты ссылочных типов по требованию CLR должны создаваться оператором new. Допустимо для нового значения применять вместо new литерал default. Можно использовать оператор new при создании примитивных типов. Можно не использовать имя типа после new при указании типа объека.

```csharp
bool bl = true, Ь2 = false, ЬЗ = bl;
Employee e = new Employee();
Employee e = new ();
int myInt = 0;
int i = new int();
int i = new();
```

Оператор new выполняет следующие действия:

1. Вычисление количества байтов, необходимых для хранения всех экземплярных полей типа и всех его базовых типов, в том числе System.Object. В каждый объект кучи дополнительно: указатель на объект-тип и индекс блока синхронизации. 

2. Выделение памяти для объекта с резервированием байтов в управляемой куче, инициализируются нулями (0).

3. Инициализация указателя на объект-тип и индекса блока синхронизации.

4. Вызов конструктора экземпляра типа с параметрами.

После этого он возвращает ссылку на вновь созданный объект. Ставшие ненужными или недоступными объекты сборщик мусора среды CLR автоматически находит и освобождает.

Литерал default позволяет присваивать переменной стандартное значение ее типа данных. Литерал default работает для стандартных типов данных, а также для специальных классов и обобщенных типов.

```csharp
int myInt = default;
var myInt = default(int);
```

Все примитивные типы данных поддерживают стандартный конструктор, можно создать переменную, используя ключевое слово new():

```csharp
bool b = new bool(); // false
b = new(); // false
int i = new int(); // 0
i = new(); // 0
char c = new char(); // ' '
c = new(); // ' '
double d = new double(); // 0.0
d = new(); // 0.0
DateTime dt = new DateTime(); // 1/1/0001 12:00:00 AM
dt = new(); // 1/1/0001 12:00:00 AM
```

Все числовые типы .NET Core поддерживают разнообразные свойства: MaxValue и MinValue и другие полезные члены.

В C# можно вот так объявлять экземпляр используемой структуры с использованием выражения with:

```csharp
var v2 = new SampleVal() with { Val2 = 20 };
```

## Ленивое создание объектов

Когда объект или переменная в программе может и не понадобится вовсе можно применять обобщенный класс Lazy<>. Этот класс позволяет определять данные, которые не будут создаваться до тех пор, пока действительно не будут применяться в коде.

Пример:

```csharp
public class Sample : IEnumerable
{
    private int[] arr;
    public Sample()
    {
        arr = Enumerable.Range(1, 100).Select(x => x).ToArray();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var e in arr)
            yield return e;
    }
}
//использование
var sample = new Lazy<Sample>();
foreach (var e in sample.Value)
{
    Console.Write("{0} ", e);
}
```

Ленивое создание объектов полезно не только для уменьшения количества выделений памяти под ненужные объекты. Этот прием можно также использовать в ситуации, когда для создания члена применяется затратный в плане ресурсов код, такой как вызов удаленного метода, взаимодействие с реляционной базой данных и т.п.

Класс Lazy<>() позволяет указывать в качестве необязательного параметра обобщенный делегат, который задает метод для вызова во время создания находящегося внутри типа. Этот метод должен возвращать новый объект требуемого типа.

```csharp
var sample = new Lazy<Sample>(() => 
{
    return new Sample();
});
```

## Очень большие целые числа

Следует применять для операций с большими целыми числами тип BigInteger.

```csharp
Console.WriteLine("=> Use BigInteger:");
BigInteger biggy = BigInteger.Parse("9999999999999999999999999999999999999999999999");
Console.WriteLine("Value of biggy is {0}", biggy);
```

## Разделители в числовых литералах

Для удобства чтения литералов допускается применять символы:

```csharp
Console.WriteLine(123_456);
Console.WriteLine(123_456_789L);
Console.WriteLine(123_456.1234F);
Console.WriteLine(123_456.12);
Console.WriteLine(123_456.12M);
Console.WriteLine(0x_00_00_FF);
Console.WriteLine("Sixteen: {0}", 0b_0001_0000);
Console.WriteLine("Thirty Two: {0}", 0b_0010_0000);
Console.WriteLine("Sixty Four: {0}", 0b_0100_0000);
```

## Неявно типизированные локальные переменные

C# поддерживает определения типа локальных переменных по типу использованных при их инициализации выражений.

```csharp
var name = "Test";
var x = (String)null;
var nums = new int[] {1,2,3};
var dict = new Dictionary<string, int>() { { "Test", 5 } };
```

В среде разработки Visual Studio при наведении мышки на ключевое слово var появляется предложенный тип.

## Неявное объявление чисел

Пример неявного объявления:

```csharp
var myUInt = 0u;
var mylnt = 0;
var myLong = 0L;
var myDouble = 0.5;
var myFloat = 0.5F;
var myDecimal = 0.5M;
```

Неявно типизированными не могут быть поля, возвращаемые значения и параметры методов.

Неявная типизация переменных в итоге дает строго типизированные данные.

## Пространства имен

Пространство имен — удобный способ логической организации связанных типов, содействующий их пониманию.

Используя директиву using, можно писать более лаконичный код. Директивой using можно определить превдоним для пространства имен.

```csharp
using Sample.Text = Sample.Text;
```

Теперь можно определять используемое пространство имен на уровне всего проекта. Для этого необходимо перед фразой using добавить ключевое слово global.

```csharp
global using System.Text;
using System;
```

Таким образом, можно не дублировать в разных файлах одни и те же пространства имён, используя директиву using. Применять данную конструкцию следует ПЕРЕД включениями using, которые объявлены без использования global.

Есть возможность объявлять использование пространств имен глобально в файле проекта *.csproj. Пример:

```csharp
  <ItemGroup>
    <Using Include="System.Text" />
  </ItemGroup>
```

Есть возможность использования namespace для всего файла. Для этого необходимо написать namespace без использования блочных фигурных скобок:

```csharp
namespace SimpleConsole;

public class Sample
{
...
}
```

В C# в различных видах проектов неявно импортируются определенные простраснова имен.

В приложениях типа Client (Microsoft.NET.Sdk) - System и пр.

В приложениях типа Web (Microsoft.NET.Sdk.Web) - дополнительно к Client: System.Net.Http.Json и пр.

В приложениях типа Worker Service (Microsoft.NET.Sdk.Worker) - дополнительно к Client: Microsoft.Extensions.Configuration и пр.


## Приведение типов

Нет специального синтаксиса для приведения типа объекта к его базовому типу, но для приведения к произвольному от него - нужно ввести операцию явного приведения типов. Язык разрешает неявное приведение типа, если оно "безопасно", не приводит к потере точности данных. Для приведения связанного с потерей точности данных нужно явно указать приведение типа.

```csharp
Object o = new Employee();
Employee e = (Employee) o;
Int64 l = 5;
Byte b = (Byte) 5;
```

Расширяющая операция - приведение типа к типу с больним размером.

Сужающая операция - приведение типа к типу с меньшим размером, может быть с потерей точности.

Если CLR не в состоянии выполнить приведение типов, то генерирует исключение System.InvalidCastException.

Оператор is проверяет совместимость объекта и типа и выдает булевский результат.

```csharp
if (o is Employee)
{
    Employee e = (Employee) o;
}
```

Можно применять оператор as, который проверяет совместимость типов и возвращяет объект или null.

```csharp
var e = o as Employee;
if (e != null)
{

}
```

Ключевое слово is можно определять для проверки совместимости типа, и если типы несовместимы, тогда возвращается false.

```csharp
var e = o is Employee;
```

Можно использовать присваивать объект переменной, если приведение работает:

```csharp
if (o is Employee emp)
{
    // использовать emp
}
```

Есть дополнительные возможности сопоставления с образцами:

```csharp
if (o is not Employee and not Sales)
{
    // использовать emp
}
```

Можно использовать отбрасывание:

```csharp
if (o is var _)
{
}
```

## Проверяемые и непроверяемые операции

В С# программист сам решает, как обрабатывать переполнение при операциях над простыми типами данных. 

Без проверки:

```csharp
UInt32 invalid = unchecked((UInt32) -1); // OK
```

С проверкой:

```csharp
Byte b = 100; b = checked((Byte) (b + 200)); // OverflowException
```

```csharp
checked { 
    Byte b = 100;
    b += 200;
} // OverflowException
```

