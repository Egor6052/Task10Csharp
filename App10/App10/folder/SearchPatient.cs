using System;
using System.Linq;

namespace App9
{
    public class SearchPatient
    {
        public delegate bool SearchDelegate(Patient patient, string searchPatient);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queuePatients">Пациенты в списке</param>
        /// <param name="searchPatient">Найденый пауиент</param>
        /// <param name="searchDelegate">Найденый делигат</param>
        /// <returns>temporaryQueue</returns>
        /// <exception cref="ArgumentException">В случае если список пуст или Patient == null</exception>
        public QueuePatients SearchPatients(QueuePatients queuePatients, string searchPatient, SearchDelegate searchDelegate)
        {
            QueuePatients temporaryQueue = new QueuePatients();
            if (queuePatients != null)
            {
                if (queuePatients.Patients != null)
                {
                    for (int i = 0; i < queuePatients.Patients.Count(); i++)
                    {
                        if (searchDelegate(queuePatients.Patients.ElementAt(i), searchPatient))
                        {
                            temporaryQueue.AddPatient(queuePatients.Patients.ElementAt(i));
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Список пациентов пуст!");
                }
            }
            else
            {
                throw new ArgumentException("Пациентов не может быть null!");
            }

            return temporaryQueue;
        }
    }
}