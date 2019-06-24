using LiteDB;
using RealEstateSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RealEstateSample.Services
{
    public class DatabaseService
    {
        string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RealEstate\\data.db";
        string directoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RealEstate\\";

        public House GetHouse(int propertyId)
        {
            using (var connection = new LiteDatabase(filePath))
            {
                var employees = connection.GetCollection<House>();
                return employees.FindById(propertyId);
            }
        }

        public List<House> GetHouses()
        {
            using (var connection = new LiteDatabase(filePath))
            {
                var houses = connection.GetCollection<House>();
                return houses.FindAll().ToList();
            }
        }

        public List<House> GetExpenses(int houseId)
        {
            using (var connection = new LiteDatabase(filePath))
            {
                var houses = connection.GetCollection<House>();
                return houses.Find(x => x.HouseId == houseId).ToList();
            }
        }

        public void SaveHouse(House house)
        {
            using (var connection = new LiteDatabase(filePath))
            {
                var expenses = connection.GetCollection<House>();
                expenses.Update(house);
            }
        }

        public void InitializeDatabase()
        {
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var connection = new LiteDatabase(filePath))
            {
                var houses = connection.GetCollection<House>();

                //var expenses = connection.GetCollection<House>();

                int result = houses.Count();

                if (result == 0)
                {

                    string r = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    int index = r.LastIndexOf("\\");

                    var dir = $"{r.Substring(0, index)}\\";
                    var file = $"{dir}\\RedmondHouses.xml";

                    using (StreamReader reader = new StreamReader(file))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(AllHouses));
                        AllHouses school = (AllHouses)serializer.Deserialize(reader);

                        foreach (var item in school.Houses.Take(10))
                        {

                            item.Schools.Add(new School
                            {
                                Name = "Seatle High school",
                                Type = "Publich high school",
                                Size = 2941
                            });

                            item.Schools.Add(new School
                            {
                                Name = "Seatle Academy of Arts",
                                Type = "Pricate middle school",
                                Size = 948
                            });

                            item.Schools.Add(new School
                            {
                                Name = "Pike Place Primary School",
                                Type = "Publich primary school",
                                Size = 492
                            });

                            houses.Insert(item);


                        }
                    }
                }
            }
        }
    }
}

