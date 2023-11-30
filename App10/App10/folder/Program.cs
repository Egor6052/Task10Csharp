using System;

namespace App9
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // LAB 8 ----------------------------------
            Patient patient1 = new Patient(1, "Max", "Ilchishin", 12);
            Patient patient2 = new Patient(2, "Andrei", "Andrich", 5);
            Patient patient3 = new Patient(3, "Petya", "Sergeev", 30);
            Patient patient4 = new Patient(4, "Nikolas", "Tkachuk", 9);
            QueuePatients queuePatients = new QueuePatients();

            // Добавление пациентов в список;
            queuePatients.AddPatient(patient1);
            queuePatients.AddPatient(patient2);
            queuePatients.AddPatient(patient3);
            queuePatients.AddPatient(patient4);
            
            string pathToJsonFile = "patients.json";
            string pathToBinaryFile = "patients.bin";
            
            // Запись и чтение из бинарного файла;
            Console.WriteLine("Сериализация в бинарный файл: ");
            DataSerializer.SaveBinFile(pathToBinaryFile, queuePatients);
            QueuePatients binLoadPatient = DataSerializer.LoadFromBin(pathToBinaryFile);
            Console.WriteLine(binLoadPatient);
            
            // Чтение и запись из JSON файла;
            Console.WriteLine("Сериализация в JSON файл: ");
            DataSerializer.SaveToJson(pathToJsonFile, queuePatients);
            QueuePatients jsonLoadPatient = DataSerializer.LoadFromJson(pathToJsonFile);
            Console.WriteLine(jsonLoadPatient);
            
            // LAB 9 ----------------------------------
            
            //Использование стандартного делигата;
            //Использование стандартного делегата;
            Console.WriteLine("Сортировка по Имени:\n");
            QueueFiltering.Sort(queuePatients, QueueFiltering.DescendingByAscendingName);
            Console.WriteLine(queuePatients);

            Console.WriteLine("Сортировка по Фамилии:\n");
            QueueFiltering.Sort(queuePatients, QueueFiltering.DescendingByAscendingSurname);
            Console.WriteLine(queuePatients);

            Console.WriteLine("Сортировка по Id:\n");
            QueueFiltering.Sort(queuePatients, QueueFiltering.DescendingByAscendingId);
            Console.WriteLine(queuePatients);

            Console.WriteLine("Сортировка по дню регистрации на прием:\n");
            QueueFiltering.Sort(queuePatients, QueueFiltering.DescendingByAscendingDaysBeforeAppointment);
            Console.WriteLine(queuePatients);

            // Использование лямбда-выражений;
            Console.WriteLine("Поиск по имени: ");
            QueuePatients nameFilter = SearchPatients(queuePatients, "Pe", 
                (part, value) => part.Name.Contains(value, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(nameFilter);

            Console.WriteLine("Поиск по дате регистрации на прием: ");
            QueuePatients dateFilter = SearchPatients(queuePatients, "12", 
                (part, value) => part.Cost.ToString().Contains(value, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(dateFilter);

            
            
            //Использование анонимных методов;
            Console.WriteLine("Поиск по id: ");
            QueueFiltering.FilteringDelegate descendingByAscendingId = delegate(Patient left, Patient right)
            {
                return left.Id > right.Id;
            };
            QueueFiltering.Sort(nameFilter, descendingByAscendingId);
            Console.WriteLine(nameFilter);
            
            
            
            
            // Lab 10 ----------------------------------
            string jsonPath = "patientsQueue.json";
            QueuePatients  patients = new QueuePatients();
            TasksPatients.PartsQueue(patients, jsonPath, 9999999, 2);

            Console.WriteLine("Выполнение сортировки по дате регистрации\n");
            TasksPatients.ParallelSort(patients, comparison);
            Console.WriteLine(patients);
        }
    }
}