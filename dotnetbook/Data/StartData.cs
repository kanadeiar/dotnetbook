namespace dotnetbook.Data;

public static class StartData
{
    public static ICollection<Book> GetBooks()
    {
        var partId = 1;
        var itemId = 1;
        var books = new Book[]
        {
            new Book
            { 
                Id = 1,
                Name = "Руководство .NET",
                Parts = new Part[]
                { 
                    new Part
                    {
                        Id = partId++,
                        Name = "Теория",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "ООП", Path = "theoryoop.md" },
                            new Item{ Id = itemId++, Name = "SOLID", Path = "theorysolid.md" },
                        }
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Основы",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Введение в .NET", Path = "originbegin.md" },
                            new Item{ Id = itemId++, Name = "Среда исполнения", Path = "originruntime.md" },
                            new Item{ Id = itemId++, Name = "Консоль", Path = "originconsole.md" },
                            new Item{ Id = itemId++, Name = "Команды", Path = "origincommands.md" },
                        },
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Создание типов",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Основы типов", Path = "origintypes.md" },
                            new Item{ Id = itemId++, Name = "Разновидности типов", Path = "typetypes.md" },
                            new Item{ Id = itemId++, Name = "Поведение типов", Path = "methodtypes.md" },
                            new Item{ Id = itemId++, Name = "Члены типов", Path = "fieldstypes.md" },
                            new Item{ Id = itemId++, Name = "Константы и поля", Path = "fields.md" },
                            new Item{ Id = itemId++, Name = "Методы", Path = "methods.md" },
                            new Item{ Id = itemId++, Name = "Параметры", Path = "parameters.md" },
                            new Item{ Id = itemId++, Name = "Свойства", Path = "properties.md" },
                            new Item{ Id = itemId++, Name = "События", Path = "events.md" },
                            new Item{ Id = itemId++, Name = "Обобщения", Path = "generics.md" },
                            new Item{ Id = itemId++, Name = "Абстракции", Path = "abstractions.md" },
                        },
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Типы данных",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Символы", Path = "symbols.md" },
                            new Item{ Id = itemId++, Name = "Строки", Path = "strings.md" },
                            new Item{ Id = itemId++, Name = "Перечислимые типы", Path = "enums.md" },
                            new Item{ Id = itemId++, Name = "Массивы", Path = "arrays.md" },
                            new Item{ Id = itemId++, Name = "Делегаты", Path = "delegates.md" },
                            new Item{ Id = itemId++, Name = "Null-типы", Path = "nullable.md" },
                            new Item{ Id = itemId++, Name = "Анонимные", Path = "anonymous.md" },
                        },
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Код",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Ветвления", Path = "ifs.md" },
                            new Item{ Id = itemId++, Name = "Итерации", Path = "whiles.md" },
                            new Item{ Id = itemId++, Name = "Стандартные интерфейсы", Path = "stdinterfaces.md" },
                            new Item{ Id = itemId++, Name = "LINQ", Path = "linq.md" },
                        },
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Многопоточность",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Потоки", Path = "threads.md" },
                            new Item{ Id = itemId++, Name = "Асинхронные вычисления", Path = "tasks.md" },
                            new Item{ Id = itemId++, Name = "Асинхронный код", Path = "asyncawait.md" },
                            new Item{ Id = itemId++, Name = "Примитивная синхронизация", Path = "syncsimple.md" },
                            new Item{ Id = itemId++, Name = "Гибридная синхронизация", Path = "synchybrid.md" },
                            new Item{ Id = itemId++, Name = "Асинхронная синхронизация", Path = "syncasync.md" },
                        },
                    },
                    new Part
                    { 
                        Id = partId++,
                        Name = "Механизмы",
                        Items = new Item[]
                        {
                            new Item{ Id = itemId++, Name = "Механизмы CLR", Path = "mechclr.md" },
                            new Item{ Id = itemId++, Name = "Исключения", Path = "mechexceptions.md" },
                            new Item{ Id = itemId++, Name = "Сборщик мусора", Path = "mechgarbage.md" },
                            new Item{ Id = itemId++, Name = "Пул потоков", Path = "mechthreadpool.md" },
                            new Item{ Id = itemId++, Name = "Асинхронный автомат", Path = "mechasync.md" },
                        },
                    },
                },
            }
        };
        return books;
    }
}