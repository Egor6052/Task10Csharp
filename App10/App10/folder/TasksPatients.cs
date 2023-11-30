using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App9
{
    public class TasksPatients
    {
        
        /// <summary>
        /// Обработка очереди пациентов в нескольких потоках и сохранение результата в формате JSON;
        /// </summary>
        public static void PartsQueue(QueuePatients patients, string jsonPath, int arraySize, int threadCount)
        {
            if (patients is null)
            {
                throw new ArgumentNullException("Пациент не должен быть null!");
            }
            else
            {
                if(!string.IsNullOrEmpty(jsonPath) && arraySize != 0 && threadCount != 0)
                {
                    int rate = arraySize / threadCount;
                    Task[] tasks = new Task[threadCount];
                    for (int i = 0; i < threadCount; i++)
                    {
                        tasks[i] = PutPartsInStorage(patients, rate);
                    }
                    Task.WaitAll(tasks);
                    DataSerializer.SaveToJson(jsonPath, patients);
               
                }
            }
        }


        /// <summary>
        /// Процес генерації та береження елементів у список
        /// </summary>
        private static Task PutPartsInStorage(QueuePatients patients, int rate)
        {
            if (patients is null)
            {
                throw new ArgumentNullException("patients is null!");
            }
            if (rate > 0)
            {
                const int maxDaysBeforeAppointment = 365;
                for (int i = 0; i < rate; i++)
                {
                    patients.AddPatient(new Patient(i++, "RandomName", "RandomSurname", maxDaysBeforeAppointment));
                }
                return Task.CompletedTask;
            }
            throw new ArgumentNullException("rate is < 0 !");
        }



        /// <summary>
        /// Параллельная сортировка пациентов в очереди;
        /// </summary>
        public static void ParallelSort(QueuePatients patients, QueueFiltering.FilteringDelegate compareQueue)
        {
            if (compareQueue is null && patients is null)
            {
                throw new ArgumentNullException("compareQueue or patients is null!");
            }
            else
            {
                const int threadCount = 2;
                Task[] tasks = new Task[threadCount];
                QueuePatients units = SplitQueue(patients, threadCount);

                for (int i = 0; i < threadCount; i++)
                {
                    tasks[i] = Task.Run(() => Sort(units.ElementAt(i), compareQueue));
                }

                Task.WaitAll(tasks);
                patients.Patients = SortedRates(units, compareQueue);
            }
        }

        /// <summary>
        /// Метод сортировки пациентов в очереди по времяни на регистрацию;
        /// </summary>
        /// <param name="unit">элемент в очереди</param>
        /// <param name="compareQueue">итоговая очередь</param>
        private static void Sort(Queue<Patient> unit, IComparer<int> compareQueue)
        {
            var sortedUnit = unit.OrderBy(patient => patient.DaysBeforeAppointment, compareQueue).ToList();
            unit.Clear();
            foreach (var patient in sortedUnit)
            {
                unit.Enqueue(patient);
            }
        }


        /// <summary>
        /// разделение пациентов из очереди на группы с определенным размером;
        /// </summary>
        public static List<QueuePatients> SplitQueue(QueuePatients patients, int units)
        {
            if (units > 0 || patients is null)
            {
                throw new ArgumentNullException("Element can not be null");
            }

            List<QueuePatients> result = new List<QueuePatients>();

            int partsPerGroup = patients.PatientsInQueue().Count / units;
            int remainingParts = patients.PatientsInQueue().Count % units;

            int indexPatient = 0;

            for (int i = 0; i < units; i++)
            {
                int currentGroupSize = partsPerGroup + (i < remainingParts ? 1 : 0);

                QueuePatients patient = new QueuePatients();
                for (int j = 0; j < currentGroupSize; j++)
                {
                    patient.AddPatient(patients.PatientsInQueue().Dequeue());
                }
                result.Add(patient);
            }
            return result;
        }


        /// <summary>
        /// Об'єднання пациентов из  группы в очередь;
        /// </summary>
        private static Queue<Patient> SortedRates(QueuePatients rates, QueueFiltering.FilteringDelegate compareParts)
        {
            var result = new Queue<Patient>();
            while (rates.PatientsInQueue(chunk => chunk.Count > 0))
            {
                var nonEmptyChunks = rates.PatientsInQueue(chunk => chunk.Count > 0).ToList();
                if (nonEmptyChunks.Count > 0)
                {
                    var minItems = nonEmptyChunks.Select(chunk => chunk.Peek()).ToList();
                    var minItem = minItems.Min();
                    foreach (var chunk in nonEmptyChunks)
                    {
                        if (compareParts(chunk.Peek(), minItem))
                        {
                            result.Enqueue(chunk.Dequeue());
                            break;
                        }
                    }
                }
            }
            return result;
        }


    }
}