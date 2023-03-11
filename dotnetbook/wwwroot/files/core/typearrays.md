# Массивы

## Объявление массива

Массив — это набор элементов данных, для доступа к которым применяется числовой индекс.

Среда CLR поддерживает одномерные, многомерные и нерегулярные массивы. Массивы относятся к ссылочному типу и всегда резмещаются в управляемой куче, переменная содержит ссылку на объект.

Примеры объявления массива из значимых типов:

```csharp
int[] myArray = new int[100];
```

Выделяется память под 100 значений типа int, всем им присваивается значение 0. Блок хранения 100 значений выделятся в управляемой куче, в этм же блоке выделяется место для указателя на объект-тип, индекс блока синхронизации и некоторые дополнительные члены.

Примеры объявления массива из ссылочных типов:

```csharp
Sample[] myArray = new Sample[100];
```

Выделяется память под 100 ссылок на Sample и все они инициализируются значением null. Массив формируется путем создания ссылок, а не каких-либо реальных объектов.

Нумерация элементов в массиве должна начинаться с нуля. Одномерные массивы с нулевым начальным элементом оптимизированы, обладают наибольшей производительностью.

Многомерные массивы также можно создавать:

```csharp
double[,] myArray = new double[5, 10];
string[,,] myArray = new string[5, 10, 20];
```

Пример объявления нерегулярных массивов:

```csharp
Sample[][] myArray = new Sample[2][];
myArray[0] = new Sample[10];
myArray[1] = new Sample[20];
```

## Инициализация массива

Синтаксис C# позволяют совместить операции создания массива и присвоения ему начальных значений.

```csharp
string[] names = new string[] { "Test", "Name", null };
var names = new int[] { 1, 2 };
var memes = { "Sato", "Avto" };
```

Можно создавать и использовать массив анонимных типов:

```csharp
var anons = new[] { new { Name = "Test" }, new { Name = "Ano" } };
foreach (var an in anons)
{
    Console.WriteLine(an.Name);
}
```

## Приведение типов в массивах

В массивах с сылочными типами доступно приведение типов. Массивы должны иметь одинаковый размер, должно быть явное или неявное приведение типов. 

При помощи метода Array.Copy можно создать новый массив совместимого типа и заполнить его новыми элементами.

```csharp
FileStream[] fileStreams = new FileStream[10];
object[] objs = fileStreams;
Stream[] streams = (Stream[])objs;
int[] ints = new int[10];
object[] objs2 = new object[ints.Length];
Array.Copy(ints, objs2, ints.Length);
```

Метод Copy выполняет следующие действия:

- упаковка значимых типов в ссылочные

- распаковка ссылочных типов в значимые

- расширение примитивных значимых типов путем копирования

- понижающее приведение типов

## Базовый класс массивов

Объявление типа приводит к автоматическому созданию типа, производному от типа System.Array. Тип будет наследовать от System.Array множество полезных методов и свойств. Также содержит статические методы для работы с массивом, многие методы имеют обобщенные перегруженные версии, повышающие производительность и дающие типобезопасность.

При создании одномерного массива с начинающейся с нуля индексацией CLR автоматически реализует интерфейсы IEnumerable<T>, ICollection<T> и IList<T> (здесь T — тип элементов массива), а также три интерфейса для всех базовых типов массива при условии, что эти типы являются ссылочными.

При создании типа ссылочного типа FileStream[] CLR автоматически реализует в нем интерфейсы IEnumerable<FileStream>, ICollection<FileStream> и IList<FileStream>. Более того, тип FileStream[] будет реализовывать интерфейсы базовых классов IEnumerable<Stream>, IEnumerable<Object>, ICollection<Stream>, ICollection<Object>, IList<Stream> и IList<Object>.

При создании типа значимого типа DateTime[] будет реализовывать только интерфейсы IEnumerable<DateTime>, ICollection<DateTime> и IList<DateTime>, другие интерфейсы не будут реализованы.

## Передача и возврат массивов

Передвая массив в качестве параметра в метод происходит передача ссылки и метод сможет изменить элементы массива.

Методы возвращают ссылку на массив. Метод может передавать как вновь созданный массив, так и внутренний массив. Метод Array.Copy позволяет передать из метода копию массива. Рекомендуется в случае отсутствия элементов возвращать массив с нулевым количеством элементов.

## Использование индексов и диапазонов

Для упрощения работы с последовательностями, можно использовать следущие типы и операции:

```csharp
System.Index предоставляет индекс в последовательности
System.Range предоставляет поддиапазон в последовательности
Операция конца (^) указывает, что индекс отсчитывается относительно конца диапазона
Операция диапазона ( ... ) устанавливает в своих операндах начало и конец диапазона.
```

> Индексы и диапазоны можно использовать с массивами, строками, Span<T> и ReadOnlySpan<T>.

Пример:

```csharp
string[] strs = { "1", "2", "3", "4", "5" };
for (var i = 1; i <= strs.Length; i++)
{
    Index ind = ^i; //обратный порядок
    Console.WriteLine(strs[ind]);
}
```

Примеры использования диапазонов и индексов:

```csharp
string[] strs = { "1", "2", "3", "4", "5" };
Index o1 = 0;
Index o2 = 2;
Range r = o1..o2;
foreach (var e in strs[r])
{
    Console.WriteLine(e);
}
Range r2 = ..;
Range r3 = 0..^0;
var elem = strs[^2];
```

## Внутренняя реализация массивов

В CLR поддерживаются массивы двух типов:

- одномерные массивы с нулевым начальным элементом;

- одномерные и многомерные массивы с неизвестным начальным индексом.

Доступ к элементам одномерного массива с нулевой нижней границей осуществляется немного быстрее, чем доступ к элементам многомерного массива или массива с ненулевой нижней границей.

Компилятор генерирует оптимизированный код при доступе к элементам одномерного массива с нулевым начальным элементом. Проверка индекса массива при этом выносится за цикл и оптимизируется.

## Необобщенные коллекции

Для работы с этими коллекциями требуется подключить пространство имен System.Collections.

Некоторые полезные классы из этого пространства имен System.Collections.

Класс     |  Описание, некоторые реализуемые интерфейсы
------|-----------
ArrayList |  Коллекция с динамически изменяемым размером, выдающая объекты в последовательном порядке       Интерфейсы - IList, ICollection, IEnumerable, ICloneable
BitArray  |  Управляет компактным массивом битовых значений, которые булевские (1 и 0)    Интерфейсы - ICollection, IEnumerable, ICloneable
Hashtable  | Коллекция пар "Ключ - значение", основанная на основе хеш-кода ключа    Интерфейсы - IDictionary, ICollection, IEnumerable, ICloneable
Queue      | Стандартная очередь объектов, работающая по принципу FIFO (первый вошел - первый вышел)   Интерфейсы - ICollection, IEnumerable, ICloneable
SortedList | Коллекция пар "ключ - значение", отсортированная по ключу и доступных по индексу    Интерфейсы - IDictionary, ICollection, IEnumerable, ICloneable
Stack      | Представляет собой стек из объектов по принципу LIFO (последний вошел - первый вышел), поддерживается функциональноть заталкивания и выталкивания, а также считывания Интерфейсы - ICollection, IEnumerable, ICloneable

Пример:

```csharp
var list = new ArrayList();
list.AddRange(new string[] { "1", "bool", "affff", "one" });
Console.WriteLine(list.Count);
list.Add("third");
list.Sort();
list.Reverse();
foreach (var v in list)
    Console.WriteLine(v);
```

## Необобщенные специальные коллекции System.Collection.Specialized

В .NET есть специальные коллекции:

Класс             |  Описание
---------|------------
HybridDictionary   | Реализует интерфейс IDictionary за счет применения ListDictionary, пока коллекция мала и переключается на Hashtable, когда коллекция вырастет
ListDictionary     | Удобен, когда необходимо управлять небольшим количеством элементов, которые могут изменяться со времнем
StringCollection   | Оптимальный способ для управления крупными коллекциями строковых данных

Применение необобщенных коллекций может привести к ряду проблем:

- код может быть с низкой производительностью, особенно на числовых данных. Причина - применение простого механизма упаковки/распаковки для простых типов.

- большинство классов не являются безопасными в отношении типов, т.к. они были созданы для работы с System.Object, и могут содержать рядом все что угодно. Из этого возникает необходимость создания специальных классов - надстроек над стандартной коллекцией.