namespace fizzbuzztemp.Models
{
    // https://opencom.no/dataset/58f23dea-ab22-4c68-8c3b-1f602ded6d3e/resource/d9f75b07-ad78-4d86-81a2-4b48db754a06/download/badetemp.csv
    public class TemperatureData
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public double Temperature { get; set; }
        public string ReadingTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}