using System;

namespace App9
{
    public class QueueFiltering
    {
        public delegate bool FilteringDelegate(Patient left, Patient right);
        
        public static void Sort(QueuePatients queuePatients, FilteringDelegate suitablePatient)
        {
            if (queuePatients != null)
            {
                if (queuePatients.Patients != null)
                {
                    int count = queuePatients.Patients.Count;
                    for (int i = 0; i < count - 1; i++)
                    {
                        Patient currentPatient = queuePatients.Patients.Dequeue();
                        for (int j = 0; j < count - i - 1; j++)
                        {
                            Patient nextPatient = queuePatients.Patients.Dequeue();
                            if (suitablePatient(currentPatient, nextPatient))
                            {
                                // Используем картеж и меняем порядорк в очереди, если нужно;
                                (currentPatient, nextPatient) = (nextPatient, currentPatient);
                            }

                            // Возвращаем элементы в очередь;
                            queuePatients.Patients.Enqueue(nextPatient);
                        }

                        // Отсортированные элементы;
                        queuePatients.Patients.Enqueue(currentPatient);
                    }
                }
            }
        }
        
        
        /// <summary>
        /// Сравнение по убыванию имен;
        /// true, если имя левого пациента следует после имени правого пациента в словарном порядке,  
        /// </summary>
        /// <param name="left">левый полюс</param>
        /// <param name="right">правый полюс</param>
        /// <returns>true or false</returns>
        public static bool DescendingByAscendingName(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return string.Compare(left.Name, right.Name, StringComparison.Ordinal) > 0;
            }
            throw new ArgumentException("left или right == null");
        }
        public static bool DescendingByDescendingName(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return string.Compare(left.Name, right.Name, StringComparison.Ordinal) < 0;
            }
            throw new ArgumentException("left или right == null");
        }
        
        
        /// <summary>
        /// Сравнение по айди;
        /// </summary>
        /// <param name="left">левый полюс</param>
        /// <param name="right">правый полюс</param>
        /// <returns>true or false</returns>
        public static bool DescendingByAscendingId(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return left.Id > right.Id;
            }
            throw new ArgumentException("left или right == null");
        }

        public static bool DescendingByDescendingId(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return left.Id < right.Id;
            }
            throw new ArgumentException("left или right == null");
        }
        
        
        /// <summary>
        /// Сравнеение пациентов по времяни записи на прием;
        /// </summary>
        /// <param name="left">левый полюс</param>
        /// <param name="right">правый полюс</param>
        /// <returns>true or false</returns>
        public static bool DescendingByAscendingDaysBeforeAppointment(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return left.DaysBeforeAppointment < right.DaysBeforeAppointment;
            }
            throw new ArgumentException("left или right == null");

        }
        public static bool DescendingByDescendingDaysBeforeAppointment(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return left.DaysBeforeAppointment > right.DaysBeforeAppointment;
            }
            throw new ArgumentException("left или right == null");
        }
        
        
        /// <summary>
        /// Сравнение фамилий пациентов в очереди по алфавитному порядку;
        /// </summary>
        /// <param name="left">левый полюс</param>
        /// <param name="right">правый полюс</param>
        /// <returns>true or false</returns>
        public static bool DescendingByAscendingSurname(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return string.Compare(left.Surname, right.Surname, StringComparison.Ordinal) > 0;
            }
            throw new ArgumentException("left или right == null");
        }
        public static bool DescendingByDescendingSurname(Patient left, Patient right)
        {
            if (left != null && right != null)
            {
                return string.Compare(left.Surname, right.Surname, StringComparison.Ordinal) < 0;
            }
            throw new ArgumentException("left или right == null");
        }
        
    }
}