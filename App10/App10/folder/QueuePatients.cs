using System;
using System.Collections.Generic;
using System.Linq;

namespace App9
{
    public class QueuePatients
    {
        // Использование Queue по условию задачи;
        private Queue<Patient> patients = new Queue<Patient>();
        public Queue<Patient> Patients
        {
            get => patients;
            set => patients = value;
        }

        /// <summary>
        /// Добавление нового пациента в очередь.
        /// </summary>
        /// <param name="newPatient">Новая запись</param>
        /// <exception cref="ArgumentException">В случае, если пациент с таким Id уже есть в очереди.</exception>
        public void AddPatient(Patient newPatient)
        {
            if (newPatient is null)
            {
                throw new ArgumentNullException(nameof(newPatient), "Пациент не может быть null.");
            }

            if (!patients.Contains(newPatient))
            {
                patients.Enqueue(newPatient);
            }
            else
            {
                throw new ArgumentException("Пациент с таким Id уже есть в очереди.");
            }
        }


        /// <summary>
        /// Извлечение первого пациента из очереди.
        /// </summary>
        /// <returns>Первый пациент в очереди</returns>
        /// <exception cref="InvalidOperationException">В случае, если очередь пуста.</exception>
        public void RemovePatient()
        {
            if (patients.Count > 0 && patients != null)
            {
                patients.Dequeue(); //удаление первого в очереди;
            }
            else
            {
                throw new InvalidOperationException("Очередь пуста.");
            }
        }

        /// <summary>
        /// Поиск пациента по имени в очереди.
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns>Пациент из очереди</returns>
        /// <exception cref="ArgumentException">В случае, если такой записи нет в очереди.</exception>
        
        // public int FindPatientByName(string searchName)
        // {
        //     // Проходим по элементам очереди;
        //     for (int i = 0; i < patients.Count; i++)
        //     {
        //         if (patients.ElementAt(i).Name == searchName)
        //         {
        //             return i; // Порядковый номер пациента в очереди;
        //         }
        //     }
        //     throw new ArgumentException("Пациент не найден.");
        // }


        /// <summary>
        /// Вывод всех пациентов в очереди.
        /// </summary>
        /// <returns>Все элементы очереди.</returns>
        public Queue<Patient> PatientsInQueue()
        {
            return patients;
        }
        
    }
}