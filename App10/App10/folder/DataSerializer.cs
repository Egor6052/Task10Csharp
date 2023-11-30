using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Text;

namespace App9
{
    public class DataSerializer
    {


        /// <summary>
        /// Сохранение всех данных из очереди пациентов в бинарный файл;
        /// </summary>
        /// <param name="pathToBinaryFile">Путь к файлу для сохранения</param>
        /// <param name="patients">Все пациенты в очереди</param>
        public static void SaveBinFile(string pathToBinaryFile, QueuePatients patients)
        {
            using (FileStream binFile = new FileStream(pathToBinaryFile, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(binFile, Encoding.Default))
                {
                    foreach (Patient patient in patients.PatientsInQueue())
                    {
                        writer.Write(patient.Name);
                        writer.Write(patient.Surname);
                        writer.Write(patient.Id);
                        writer.Write(patient.DaysBeforeAppointment);
                    }
                }
            }
        }


        /// <summary>
        /// Сохранение данных очереди в бинарный файл;
        /// </summary>
        /// <param name="pathToBinaryFile">Путь к бинарному файлу</param>
        /// <returns>Очередь</returns>
        public static QueuePatients LoadFromBin(string pathToBinaryFile)
        {
            if (!File.Exists(pathToBinaryFile))
            {
                // Обработка случая, когда файл не существует
                throw new FileNotFoundException($"Файл не найден: {pathToBinaryFile}");
            }

            using (FileStream binFile = new FileStream(pathToBinaryFile, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(binFile, Encoding.Default))
                {
                    QueuePatients queuePatients = new QueuePatients();
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        // Создаем экземпляр класса с данными из файла;
                        Patient patient = new Patient
                        {
                            Name = reader.ReadString(),
                            Surname = reader.ReadInt32().ToString(),
                            Id = reader.ReadInt32(),
                            DaysBeforeAppointment = reader.ReadInt32()
                        };
                        queuePatients.PatientsInQueue().Enqueue(patient);
                    }
                    return queuePatients;
                }
            }
        }



        /// <summary>
        /// ВЫполняем сериализацию в json файл;
        /// </summary>
        /// <param name="pathToJsonFile">Путь к файлу для записи</param>
        /// <param name="patients">Пациенты из очереди</param>
        public static void SaveToJson(string pathToJsonFile, QueuePatients patients)
        {
            string jsonString = JsonSerializer.Serialize(patients);
            using (FileStream outputFile = new FileStream(pathToJsonFile, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    writer.Write(jsonString);
                }
            }
        }


        /// <summary>
        /// Десериализация из json файла;
        /// </summary>
        /// <param name="pathToJsonFile">Путь к файлу для чтения</param>
        /// <returns></returns>
        public static QueuePatients LoadFromJson(string pathToJsonFile)
        {
            if (!File.Exists(pathToJsonFile))
            {
                // Обработка случая, когда файл не существует
                throw new FileNotFoundException($"Файл не найден: {pathToJsonFile}");
            }

            using (FileStream inputFile = new FileStream(pathToJsonFile, FileMode.Open))
            {
                using (StreamReader jsonReader = new StreamReader(inputFile))
                {
                    string jsonString = jsonReader.ReadToEnd();
                    return JsonSerializer.Deserialize<QueuePatients>(jsonString);
                }
            }
        }
    }
}