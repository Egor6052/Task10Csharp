using System;

namespace App9
{
    public class Patient
    {
        private int _id;
        private string _name;
        private string _surname;
        private int _daysBeforeAppointment;

        /// <summary>
        /// Конструктор для полей пациента;
        /// </summary>
        /// <param name="id">Айди пациента</param>
        /// <param name="name">Имя пациента</param>
        /// <param name="surname">Фамилия пациента</param>
        public Patient(int id, string name, string surname, int daysBeforeAppointment)
        {
            Name = name;
            Id = id;
            Surname = surname;
            DaysBeforeAppointment = daysBeforeAppointment;
        }

        /// <summary>
        /// Для заполнения экземпляров класса из сохраненного файла bin;
        /// </summary>
        /// <exception cref="NotImplementedException">В случае если конструктор не риализован</exception>
        public Patient() { }
        
        
        /// <summary>
        /// Проверка на корректность дней до сеанса к врачу;
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">В случае, если отрицателдьное количество.</exception>
        public int DaysBeforeAppointment
        {
            get => _daysBeforeAppointment;
            set
            {
                if (value > -1 && value != null)
                {
                    _daysBeforeAppointment = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Дней до приема не может быть отрицательное количество или null.");
                }
            }
        }

        /// <summary>
        /// Проверка на корректность индификационного номера пациента;
        /// </summary>
        /// <exception cref="ArrayTypeMismatchException">В случае если, номер введен некорректно</exception>
        public int Id
        {
            get => _id;
            set
            {
                if (value > 0 && value != null)
                {
                    _id = value;
                }
                else
                {
                    throw new ArrayTypeMismatchException("Id пациента меньше 0 или null.");
                }
            }
        }

        /// <summary>
        /// Проверка на корректность введенного имени пациента;
        /// </summary>
        /// <exception cref="AggregateException">В случае если имя не введено или пустая строка.</exception>
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) //проверка на null или пустую строку;
                {
                    _name = value.Trim();
                }
                else
                {
                    throw new ArgumentException("Имя пациента не может быть null, пустой строкой или содержать только пробелы.");
                }
            }
        }

        /// <summary>
        /// Проверка на корректность введенной фамилии пациента;
        /// </summary>
        /// <exception cref="ArgumentException">В случае если фамилия не введена или пустая строка.</exception>
        public string Surname
        {
            get => _surname;
            set
            {
                if (value != null && value != "")
                {
                    _surname = value.Trim();
                }
                else
                {
                    throw new ArgumentException("Фамилия пациента null или пустая строка.");
                }
            }
        }

        /// <summary>
        /// Преобразование данных о пациенте в строку;
        /// </summary>
        /// <returns>Данные пациента</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Surname: {Surname}, Days Before Appointment: {DaysBeforeAppointment};";
        }
    }
}