
class Program
{
    static void Main(string[] args)
    {

        var npz1 = new Factory();
        npz1.description = "Первый нефтеперерабатывающий завод";
        npz1.name = "НПЗ№1";
        npz1.id = 1;

        var npz2 = new Factory();
        npz2.description = "Второй нефтеперерабатывающий завод";
        npz2.name = "НПЗ№2";
        npz2.id = 2;

        var unit1 = new Unit();
        unit1.description = "Газофракционирующая установка";
        unit1.name = "ГФУ-2";
        unit1.id = 1;
        unit1.factoryId = 1;

        var unit2 = new Unit();
        unit2.description = "Атмосферно-вакуумная трубчатка";
        unit2.name = "АВТ-6";
        unit2.id = 2;
        unit2.factoryId = 1;

        var unit3 = new Unit();
        unit3.description = "Атмосферно-вакуумная трубчатка";
        unit3.name = "АВТ-10";
        unit3.id = 3;
        unit3.factoryId = 2;

        var tank1 = new Tank();
        tank1.description = "Надземный - вертикальный";
        tank1.name = "Резервуар 1";
        tank1.maxVolume = 2000;
        tank1.volume = 1500;
        tank1.id = 1;
        tank1.unitId = 1;

        var tank2 = new Tank();
        tank2.description = "Надземный - горизонтальный";
        tank2.name = "Резервуар 2";
        tank2.maxVolume = 3000;
        tank2.volume = 2500;
        tank2.id = 2;
        tank2.unitId = 1;

        var tank3 = new Tank();
        tank3.description = "Надземный - горизонтальный";
        tank3.name = "Дополнительный резервуар 24";
        tank3.maxVolume = 3000;
        tank3.volume = 3000;
        tank3.id = 3;
        tank3.unitId = 2;

        var tank4 = new Tank();
        tank4.description = "Надземный - вертикальный";
        tank4.name = "Резервуар 35";
        tank4.maxVolume = 3000;
        tank4.volume = 3000;
        tank4.id = 4;
        tank4.unitId = 2;

        var tank5 = new Tank();
        tank5.description = "Подземный - двустенный";
        tank5.name = "Резервуар 47";
        tank5.maxVolume = 5000;
        tank5.volume = 4000;
        tank5.id = 5;
        tank5.unitId = 2;

        var tank6 = new Tank();
        tank6.description = "Подводный";
        tank6.name = "Резервуар 256";
        tank6.maxVolume = 500;
        tank6.volume = 500;
        tank6.id = 6;
        tank6.unitId = 3;


        var tanks = GetTanks(tank1, tank2, tank3, tank4, tank5, tank6);

        var units = GetUnits(unit1, unit2, unit3);

        var factories = GetFactories(npz1, npz2);
        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        var foundUnit = FindUnit(units, tanks, "Резервуар 2");

        var factory = FindFactory(factories, foundUnit);

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.name} и заводу {factory.name}");

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");

        var searching = Console.ReadLine();
        FindInfo(factories, units, tanks, searching); //Поиск информации по описанию объекта
    }
    public static void FindInfo(Factory[] factories, Unit[] units, Tank[] tanks, string description)
    {
        for (int i = 0; i < factories.Length; i++)
            if (factories[i].description == description)
            {
                Console.WriteLine($"По вашему запросу найден {factories[i].name}, это {factories[i].description}," +
                    $"\n ID № {factories[i].id}.");
                for (int j = 0; j < units.Length; j++)
                {
                    int Volume = 0;
                    int maxVolume = 0;
                    if (units[j].factoryId == factories[i].id)
                    {

                        Console.WriteLine($"Оснащен установкой {units[j].name} - {units[j].description} cообщающейся с хранилищем: ");
                        for (int k = 0; k < tanks.Length; k++)
                            if (tanks[k].unitId == units[j].id)
                            {
                                maxVolume += tanks[k].maxVolume;
                                Volume += tanks[k].volume;
                                Console.Write($"{tanks[k].name}, типа установки {tanks[k].description}, наполненностью в {tanks[k].volume} кубических метров" +
                                    $"\n и общим объемом в {tanks[k].maxVolume} кубических метров \n");
                            }
                    }
                    if (Volume != 0)
                        Console.WriteLine($"Общая наполненность хранилищ - {Volume} кб.м., максимальный объем - {maxVolume} ");
                }
            }
        for (int i = 0; i < units.Length; i++)
        {
            int Volume = 0;
            int maxVolume = 0;
            if (units[i].description == description)
            {
                Console.WriteLine($"По вашему запросу найдена установка {units[i].name} - {units[i].description} cообщающаяся с хранилищем: ");
                for (int k = 0; k < tanks.Length; k++)
                    if (tanks[k].unitId == units[i].id)
                    {
                        maxVolume += tanks[k].maxVolume;
                        Volume += tanks[k].volume;
                        Console.Write($"{tanks[k].name}, типа установки {tanks[k].description}, наполненностью в {tanks[k].volume} кубических метров" +
                            $"\n и общим объемом в {tanks[k].maxVolume} кубических метров \n");
                    }

            }
            if (Volume != 0)
                Console.WriteLine($"Общая наполненность хранилищ - {Volume} кб.м., максимальный объем - {maxVolume} ");
        }

        for (int i = 0; i < tanks.Length; i++)
        {

            if (tanks[i].description == description)
            {
                Console.WriteLine($"По вашему запросу найден {tanks[i].name} типа установки {tanks[i].description}" +
                    $"\n наполненостью в {tanks[i].volume}кб.м, и общим объемом в {tanks[i].maxVolume} кб.м.");
            }
        }

    } //Поиск информации по описанию объекта
    // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенным таблицам
    // можно использовать создание объектов прямо в C# коде через new, или читать из файла (на своё усмотрение)

    public static Tank[] GetTanks(Tank tank1, Tank tank2, Tank tank3, Tank tank4, Tank tank5, Tank tank6)
    {

        Tank[] tanks = new Tank[] { tank1, tank2, tank3, tank4, tank5, tank6 };

        return tanks;
    }
    // реализуйте этот метод, чтобы он возвращал массив установок, согласно приложенным таблицам
    public static Unit[] GetUnits(Unit unit1, Unit unit2, Unit unit3)
    {
        Unit[] units = new Unit[] { unit1, unit2, unit3 };
        return units;
    }

    // реализуйте этот метод, чтобы он возвращал массив заводов, согласно приложенным таблицам
    public static Factory[] GetFactories(Factory npz1, Factory npz2)
    {
        Factory[] factories = new Factory[] { npz1, npz2 };
        return factories;

    }

    // реализуйте этот метод, чтобы он возвращал установку (Unit), которой
    // принадлежит резервуар (Tank), найденный в массиве резервуаров по имени
    // учтите, что по заданному имени может быть не найден резервуар
    public static Unit FindUnit(Unit[] units, Tank[] tanks, string unitName)
    {
        var unitId = 0;
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].name == unitName)
            {
                unitId = tanks[i].unitId;
                break;
            }
        }
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].id == unitId)
            {
                unitId = i;
                break;
            }
        }
        return units[unitId];
    }

    // реализуйте этот метод, чтобы он возвращал объект завода, соответствующий установке
    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        var factoryId = 0;
        for (int i = 0; i < factories.Length; i++)
        {
            if (factories[i].id == unit.factoryId)
                factoryId = i;
        }
        return factories[factoryId];
    }

    // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
    public static int GetTotalVolume(Tank[] units)
    {
        var totalVolume = 0;
        for (int i = 0; i < units.Length; i++)
            totalVolume += units[i].maxVolume;
        return totalVolume;
    }
}
