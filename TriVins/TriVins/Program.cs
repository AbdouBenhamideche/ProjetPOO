using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using TriVins;

// Chemin vers votre fichier CSV
string filePath = @"DONNE\samples_reduced\sample_01.csv";

Console.WriteLine(File.Exists(filePath));
Console.ReadKey();
var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = ";",
    HasHeaderRecord = true
};


using (var reader = new StreamReader(filePath)) 
using (var csv = new CsvReader(reader, config))

{
    var records = csv.GetRecords<Vin>().ToList();

    foreach (var record in records)
    {
        Console.WriteLine($"Alcohol: {record.alcohol}, Sulphates: {record.sulphates}, Citric Acid: {record.citricacid}, Volatile Acidity: {record.volatileacidity}, Quality: {record.quality}");
    }
    Console.ReadKey();

}
