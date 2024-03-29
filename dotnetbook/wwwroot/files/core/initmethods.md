# Методы

Методы могут быть реализованы внутри области видимости классов, структур или интерфейсов.

```csharp
модификаторы   тип вертаемого   имя     параметры
    public static  void        MyFunc  (string s)
    {
        ...         тело метода
    }
```

Пример простого метода, сжатого до выражения:

```csharp
static int Add(int x, int y) => x + y;
```

## Конструкторы

Конструктор — это специальный метод класса или структуры, который неявно вызывается при создании объекта с применением ключевого слова new. Конструкторы позволяют устанавливать состояние объекта в момент его создания.

Один конструктор может вызывать другой конструктор используя ключевое слово this. Так можно строить цепочки конструкторов.

```csharp
internal class Sample
{
    private int _value;
    public Sample()
    {
        _value = 5;
    }
    public Sample(int value) : this()
    {
        _value = value;
    }
}
```

Порядок логики конструкторов:

- Первым делом создается объект путем вызова конструктора, принимающего один аргумент типа int.

- Этот конструктор передает полученные данные главному конструктору и предоставляет любые дополнительные начальные аргументы, не указанные вызывающим кодом.

- Главный конструктор присваивает входные данные полям данных объекта.

- Управление возвращается первоначально вызванному конструктору, который выполняет оставшиеся операторы кода.

Конструктор может быть сжатым до выражения:

```csharp
internal sealed class Sample
{
    private int _value;
    public Sample(int val) => _value = val;
}
```

Может быть определен конструктор с параметрами out:

```csharp
internal sealed class Sample
{
    public Sample(out int val) 
    {
        val = 10;
    }
}
```

### Конструктор ссылочных типов

Конструкторы ссылочного типа - специальные методы, корректно инициализирующие новый экземпляр типа. При создании экземпляра объекта ссылочного типа выделяется память для полей данных в куче, инициализируются служебные поля и затем вызывается конструктор, устанавливающий исходное состояние объекта.

Конструкторы класса не наследуются. Если конструктор явно в классе не задать, то компилятор создаст неявный конструктор без параметров по умолчанию. Если создать конструктор с параметрами, то неявный конструктор без параметров станет недоступен.

Если базовый класс не предлагает конструктор без параметров, производный класс должен выполнить явный вызов базового конструктора с помощью base.

Для абстрактных классов компилятор создает конструктор по умолчанию с модификатором защищенный. Для статических классов конструктор по умолчанию не создается.

Всегда вызывается неявно конструктор базового класса System.Object без параметров. Поля, инициализирующиеся при определении неявно инициализируются в конструкторе без параметров.

```csharp
internal sealed class Sample
{
    private int _value = 5;
}
```

Все подклассы должны явно вызывать подходящий конструктор базового класса. Ключевое слово base можно применять всякий раз, когда подкласс желает обратиться к открытому или защищенному члену, определенному в родительском классе.

```csharp
internal class Sample
{
    private int _value;
    protected string Name { get; set; }
    public Sample()
    {
        _value = 5;
    }
    public Sample(int value) : this()
    {
        _value = value;
    }
}
internal sealed class AdvSample : Sample
{
    public AdvSample(int val) : base(val)
    {
        base.Name = "Advanved";
    }
}
```

### Конструктор значимых типов

CLR всегда разрешает создание экземпляров значимых типов. Конструкоры у значимых типов можно не определять. Компилятор определяет конструктор по умолчанию не имеющий параметров для значимого типа, в нем поля значимого типа инициализируются значением 0 / null. 

CLR позволяет определять конструкторы для значимых типов, но они должны вызыватся только при наличии кода, явно вызывающего их.

CLR еще позволяет определять конструкторы без параметров для значимых типов. Также можно устанавливать значения по умолчанию для полей и свойств структур, но тогда нужно установить значения для всех полей.

```csharp
internal struct Test
{
    public int _value;
    public Test(int value)
    {
        _value = value;
    }
}
// использование
_test = new Test(1);
```

Тогда оператор new вызывает конструктор для инициализации полей значимого типа. 

Компилятор не генерирует автоматически код для вызова конструктора по умолчанию для значимого типа даже при наличии конструктора без параметров, нужно его явно вызвать.

Компилятор C# позволяет определять для значимого типа как конструкторы без параметров, так и конструкторы с параметрами.

В значимых типам можно задавать инициализацию начальных значений свойств и полей.

```csharp
internal struct SampleVal
{
    public int value = 10;
    public int Val2 { get; set; }
    public SampleVal()
    {
        Val2 = 20;
    }
    public SampleVal(int value)
    {
        this = new SampleVal();
        value = 10;
    }
}
// использование
SampleVal v1 = new();
var v2 = new SampleVal(10);
```

В конструкторе значимого типа this - экземпляр значимого типа, допускает запись, а в конструкторах ссылочного типа указатель this - только для чтения.

### Конструкторы уровня типов

Также называют статическими конструкторами. Такие конструкторы можно применять к интерфейсам, ссылочным и значимым типам. Первоначально у типа не определено конструктора. Не может быть более 1 конструктора, у таких конструкторов нет параметров.

```csharp
internal sealed class Sample
{
    private static int _value;
    static Sample()
    {     
        _value = 5;
    }
}
internal struct SampleVal
{
    static SampleVal()
    {
    }
}
```

Компилятор такие конструкторы делает зарытыми автоматически. 

> Конструкторы в значимом типе не следует определять, так как CLR не всегда вызывает статический конструктор значимого типа.

CLR старается гарантировать, чтобы конструктор типа выполнялся только раз в каждом домене приложений. Для этого при вызове конструктора типа вызывающий поток в рамках синхронизации потоков получает исключающую блокировку. Только первый поток выполнит код статического конструктора.

> Конструктор типов лучше всего подходит для инициализации всех объектов-одиночек.

Если в конструкторе типа генерируется необработанное исключение, то такой тип становится непригодным. Код из такого конструктора может использовать только статические поля класса.

В значимых типах допускается использовать конструкторы статические.

```csharp
internal struct SampleVal
{
    public static int _value;
    static SampleVal()
    {
        _value = 10;
    }
}
```

## Статические методы

Методы, декорированные атрибутом static находятся на уровне типа, могут манипулировать только статическим данными класса. Поле статических данных разделяется между всеми объектами конкретного класса.

Внутри статического метода нельзя использовать ключевое слово this.

## Локальные функции

Можно создавать методы (проще назвать локальную функцию) внутри методов.

Пример:

```csharp
PrintBox();
void PrintBox() => WriteLine("Box");
```

Модификатор static. Можно добавлять ключевое слово statiс для гарании что локальная функция не захватывает и не ссылается на переменные в области ее видимости.

```csharp
int var = 25;
PrintBox(45);
static void PrintBox(int var) => WriteLine($"Box {var}"); // Box 45
```

## Перегрузка методов

В C# разрешена перегрузка методов. Когда определяется набор идентично именованных методов, которые отличаются друг от друга количеством (или типами) параметров, то говорят, что такой метод был перегружен.

```csharp
public class Sample
{
    static int Add(in int x, in int y)
    {
        var ans = x + y;
        return ans;
    }
    static double Add(in double x, in double y)
    {
        var ans = x + y;
        return ans;
    }
}
```

Модификаторы in, ref и out не считаются частью сигнатуры при перегрузке методов, когда используется более одного модификатора.

## Перегрузка операторов

CLR ничего не известно о перегрузке операторов, для CLR это просто методы, указывается, как языки программирования должны предоставлят доступ к перегруженным операторам. Перегрузка операторов определяется языком программирования.

Перегруженные операторы должны быть открытыми и статическими. 

Пример перегруженного оператора:

```csharp
public static Sample operator+(Sample s1, Sample s2) { ... }
public static Sample operator-(Sample s1, Sample s2) { ... }
public static Sample operator++(Sample s) { ... }
public static Sample operator--(Sample s) { ... }
```

Когда компилятор в тексте видит оператор +, он исследует типы его операндов, выясняет существование определения для одного из них специального метода op_XXX со специальным флагом specialname. При нахождении такого метода, компилятор генерирует код вызова этого метода.

Унарные операторы C#:

Оператор C# | Имя специального метода | Рекомендуемое CLS-совместимое имя
----|-----|-----
\+ | op_UnaryPlus | Plus
\- | op_UnaryNegation | Negate
\! | op_LogicalNot | Not
\- | op_OnesComplement | OnesComplement
\+\+ | op_Increment | Increment
\-\- | op_Decrement | Decrement
Нет | op_True | IsTrue { get; }
Нет | op_False | IsFalse { get; }

Бинарные операторы C#:

Оператор C# | Имя специального метода | Рекомендуемое CLS-совместимое имя
----|-----|-----
\+ | op_Addition | Add
\- | op_Subtraction | Subtract
\* | op_Multiply | Multiply
\/ | op_Division | Divide
\% | op_Modulus | Modulus
\& | op_BitwiseAnd | BitwiseAnd
\|\| | op_BitwiseOr | BitwiseOr
\^ | op_ExclusiveOr | Xor
\<\< | op_LeftShift | LeftShift
\>\> | op_RightShift | RightShift
\=\= | op_Equality | Equality
\!\= | op_Inequality | Inequality
\< | op_LessThan | Compare
\> | op_GreatherThan | Compare
\<\= | op_LessThanOrEqual | Compare
\>\= | op_GreatherThanOrEqual | Compare

## Методы операторов преобразования

Когда целевой и требуемый тип являются примитивными, компилятор справляется сам, но если один тип не является примитивным - нужно создать код, заставляющий CLR выполнить приведение типов.

Для этого в типе должны определяться открытые конструкторв, принимающие в качестве параметра экземпляр преобразуемого типа. Язык C# поддерживает перегрузку операторов преобразования. Они определяются с помощью специального синтаксиса.

```csharp
public sealed class Sample
{
    public Sample(int value) { ... }
    public int ToInt() { ... }
    //Неявное приведение типа
    public static implicit operator Sample(Int32 value) => new Sample(value);
    //Явное приведение типа
    public static explicit operator Int32(Sample sample) => sample.ToInt32();
}
```

Компилятор C# при обнаружении внутренних механизмов генерирует IL-код, вызывающий методы операторов преобразования, определенные в этом типе. Компилятор генерирует по одному методу для каждого оператора преобразования типа.

Если точность при приведении типов не теряется - то следует определять оператор неявного преобразования, иначе - явного. Если попытка явного приведения завешнится неудачей, следует выбрасывать исключение OverflowException или InvalidOperationException. 

> Операторы неявного преобразования никода не вызываются при использовании операторов is или as.

## Методы расширения

Методы расширения позволяют определить статический метод, который вызывается посредством синтаксиса экземплярного метода.

```csharp
public static class IntExtensions
{
    public static int SumOf(this int one, int two)
    {
        return one + two;
    }
}
// варианты использования
var result = IntExtensions.SumOf(value, 32);
var result = value.SumOf(32);
```

Методы расширения должны быть объявлены в статическом необобщенном классе. Метод должен иметь хотябы один параметр, первый параметр должен быть помечен ключевым словом this. Компилятор ищет C# методы расширения только в статических классах. Если в нескольких статических классах определены несколько одинаковых методов расширения, то тогда выдается сообщение о ошибке (для устранения ошибки нужно применять синтаксис статического метода с указанием имени статического класса).

При расширении одного типа методом расширения все унаследованные типы также будут расширены этим методом. Экземплярный метод с таким-же названием как и метод расширения, имеет приоритет над методом расширения.

Среда CLR при вызове метода расширения не производит проверку объекта на null.

```csharp
String value = null;
var result = value.MyIfIsNull(5); //NullReferenceException не будет
```

Можно определять методы расширения для интерфейсных типов.

```csharp
public static void MyTestItems<T>(this IEnumerable<T> collection)
{
    ...
}
//использование:
"Grant".MyTestItems();
new[] {"One", "Two"}.MyTestItems();
new List<Int32>() {1, 2, 3}.MyTestItems();
```

Можно определять методы расширения для типов делегатов и для перечислимых типов.

В компиляторе C#, при пометке первого параметра статического метода ключевым словом this, компилятор устанавливает специальный атрибут к методу, и к статическому классу. Это позволяет компилятору быстро просканировать все ссылающиеся сборки, чтоб определить, какая из них содержит методы расширения и в дальнейшем быстро находить все расширения.

## Расширяющй метод GetEnumerator

Оператор foreach языка C# исследует расширяющие методы класса и в случае, если обнаруживает метод GetEnumerator(), то использует его для получения реализации IEnumerator, относящейся к данному классу.

```csharp
internal static class Extensions
{
    public static IEnumerator GetEnumerator(this Sample s)
        => s.Array.GetEnumerator();
}
public class Sample
{
    public int[] Array { get; set; }
}
// использование
var sample = new Sample();
foreach (var e in sample)
{
    Console.WriteLine(e);
}
```

## Частичные методы

Для решения проблемы переопределения можно применять частичные методы языка C#. Класс может быть запечатанным, статическим или значимым. Код определения двух частичных классов компилируется в один тип. Код базового метода - объявление частичного метода с пометкой partial и не имеет тела. Код обычного метода реализует объявление частиного метода с пометкой partial и имеет тело.

```csharp
internal sealed partial class Base
{
    partial void OnTest();
}
internal sealed partial class Base
{
    partial void OnTest()
    {
        System.Console.WriteLine("OnTest");
    }
}
```

Преимущество такого решения - при отсутствии объявления выполняемого частичного метода компилятор не будет генерировать метаданные частичного метода. Частичные методы могут объявлятся только внутри частичного класса или структуры. Частичные методы должны иметь возвращаемый тип void и не могут иметь параметров out. Частичные метод не существует во время выполнения программы.
