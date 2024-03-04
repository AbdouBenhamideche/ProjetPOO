using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TriVins;

// Chemin vers votre fichier CSV
string filePath = @"DONNE\train_reduced.csv";

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
    Console.WriteLine(CalculateEntropy(records)); 
    Console.ReadKey();
    Console.WriteLine(CalculateInformationGain(records, "alcohol"));
    Console.ReadKey();
}
static double CalculateEntropy(List<Vin> records)
{
    int totalSamples = records.Count;
    var qualityCounts = records.GroupBy(s => s.quality)
                               .Select(g => new { Quality = g.Key, Count = g.Count() });

    double entropy = 0.0;

    foreach (var count in qualityCounts)
    {
        
        double proportion = (double)count.Count / totalSamples;
        entropy -= proportion * Math.Log(proportion, 2);
    }

    return entropy;
}
static double CalculateInformationGain(List<Vin> records, string feature)
{
    double entropyS = CalculateEntropy(records);

    var featureValues = records.Select(v => v.GetType().GetProperty(feature).GetValue(v, null)).Distinct();

    double gain = 0.0;

    foreach (var value in featureValues)
    {
        var subset = records.Where(v => v.GetType().GetProperty(feature).GetValue(v, null).Equals(value)).ToList();
        double entropySubset = CalculateEntropy(subset);
        double proportion = (double)subset.Count / records.Count;
        gain += proportion * entropySubset;
    }

    gain = entropyS - gain;
    return gain;
}
