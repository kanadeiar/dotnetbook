# Константы и поля

## Константы

Константа - это идентификатор, значение которого никогда не меняется. Можно определить только для примитивных типов.

```csharp
public const int Value = 16;
```

Значение константы внедряется в код, в период выполнения память для констант не выделяется. Для констант нельзя использовать слово static, так как они и так статичны.

Нельзя применять константы во время выполнения, если модуль задействует значение из другого модуля.

## Поля

Поля - член данных, хранящий экземпляр значимого типа или ссылку на ссылочный тип.

По отношению к полям можно применить следующие модификаторы:

- static - поле является частью состояния типа, а не объекта

- instance - поле связано с экземпляром типа, а не с самим типом

- readonly - запись в поле разрешается только из кода конструктора

- volatile - код, использующий это поле, не должен оптимизироватся компилятором, используется с целью обеспечения безопасности потоков. (неустойчивый тип)

Поля могут быть как изменяемые, так и предназначенные только для чтения. Данные в неизменяемые поля можно записать только один раз из конструктора.

Пример определения статических и экземплярных полей:

```csharp
public static readonly Random _random = new Random();
private static int _numbers = 0;
public readonly string Sample = "Test";
private FileStream _stream;
```

Неизменность поля ссылочного типа означает неизменность ссылки, которую этот тип содержит, а не объекта, на которую указывает ссылка.


