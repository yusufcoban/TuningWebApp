using BaseBackend.Models;

namespace YourNamespace.Controllers
{
    public class TuningDatabaseHandler
    {

        private static List<CarBrand> carBrands = new List<CarBrand>
        {
            new CarBrand
            {
                Name = "Abarth",
                Icon = "../src/assets/brandlogos/abarth.png",
                Slug = "abarth",
                Models = new List<CarModel>
                {
                    new CarModel { Id = "1_1", Name = "Abarth 595", Icon = "/assets/modelslogo/car.png" },
                    new CarModel { Id = "1_2", Name = "Abarth 124 SpIder", Icon = "/assets/modelslogo/car.png" },
                    new CarModel { Id = "1_3", Name = "Abarth 695", Icon = "/assets/modelslogo/car.png" },
                    new CarModel { Id = "1_4", Name = "Abarth 1000", Icon = "/assets/modelslogo/car.png" }
                }
            },
            new CarBrand
            {
                Name = "Alfa Romeo",
                Icon = "../src/assets/brandlogos/alfa-romeo.png",
                Slug = "alfa-romeo",
                Models = new List<CarModel>
                {
                    new CarModel { Id = "2_1",Name= "Alfa Romeo Giulia", Icon= "../src/assets/modelslogo/car.png" },
                    new CarModel { Id = "2_2",Name= "Alfa Romeo Stelvio", Icon= "../src/assets/modelslogo/car.png" },
                    new CarModel { Id = "2_3",Name= "Alfa Romeo 4C", Icon= "../src/assets/modelslogo/car.png" },
                    new CarModel { Id = "2_4",Name= "Alfa Romeo Tonale", Icon= "../src/assets/modelslogo/car.png" },
                    new CarModel { Id = "2_5",Name= "Alfa Romeo Guilia Quadrifoglio", Icon= "../src/assets/modelslogo/car.png" },
                    new CarModel { Id = "2_6",Name= "Alfa Romeo Stelvio Quadrifoglio", Icon= "../src/assets/modelslogo/car.png" }
                }
            },
            new CarBrand {  Name= "Audi",
        Icon= "../src/assets/brandlogos/audi.png",
        Slug= "audi",
        Models = new List<CarModel>{
       new CarModel { Id= "3_1", Name= "Audi A3", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_2", Name= "Audi A4", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_3", Name= "Audi A6", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_4", Name= "Audi Q3", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_5", Name= "Audi Q5", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_6", Name= "Audi Q7", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_7", Name= "Audi Q8", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_8", Name= "Audi R8" , Icon= "../src/assets/Modelslogo/car.png"},
       new CarModel { Id= "3_9", Name= "Audi S3", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_10", Name= "Audi S4", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_11", Name= "Audi S5", Icon= "../src/assets/Modelslogo/car.png" },
       new CarModel { Id= "3_12", Name= "Audi SQ5", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Bentley",
        Icon= "../src/assets/brandlogos/bentley.png",
            Slug= "bentley",
        Models = new List<CarModel>{
     new CarModel  { Id= "4_1", Name= "Bentley Continental GT", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel  { Id= "4_2", Name= "Bentley Bentayga" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "4_3", Name= "Bentley Flying Spur" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "4_4", Name= "Bentley Mulsanne", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "BMW",
        Icon= "../src/assets/brandlogos/bmw.png",
            Slug= "bmw",
        Models = new List<CarModel>{
    new CarModel  { Id= "5_1", Name= "BMW 1 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_2", Name= "BMW 2 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_3", Name= "BMW 3 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_4", Name= "BMW 4 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_5", Name= "BMW 5 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_6", Name= "BMW 7 Series", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_7", Name= "BMW 8 Series" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_8", Name= "BMW X1", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_9", Name= "BMW X2" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_10", Name= "BMW X3" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_11", Name= "BMW X4" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_12", Name= "BMW X5", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_13", Name= "BMW X6", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_14", Name= "BMW X7" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "5_15", Name= "BMW Z4", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_16", Name= "BMW i3", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "5_17", Name= "BMW i8", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Chevrolet",
        Icon= "../src/assets/brandlogos/chevrolet.png",
            Slug= "chevrolet",
        Models = new List<CarModel>{
  new CarModel  { Id= "6_1", Name= "Chevrolet Malibu", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_2", Name= "Chevrolet Camaro", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_3", Name= "Chevrolet Corvette" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel  { Id= "6_4", Name= "Chevrolet Tahoe", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_5", Name= "Chevrolet Silverado", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_6", Name= "Chevrolet Equinox", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_7", Name= "Chevrolet Traverse", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "6_8", Name= "Chevrolet Suburban" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel  { Id= "6_9", Name= "Chevrolet Blazer", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Dodge",
        Icon= "../src/assets/brandlogos/dodge.png",
            Slug= "dodge",
        Models = new List<CarModel>{
    new CarModel  { Id= "7_1", Name= "Dodge Charger", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "7_2", Name= "Dodge Challenger", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "7_3", Name= "Dodge Durango", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel  { Id= "7_4", Name= "Dodge Journey" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel  { Id= "7_5", Name= "Dodge Grand Caravan", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Ferrari",
        Icon= "../src/assets/brandlogos/ferrari.png",
        Slug= "ferrari",
        Models = new List<CarModel>{
  new CarModel   { Id= "8_1", Name= "Ferrari 488", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel   { Id= "8_2", Name= "Ferrari F8", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel   { Id= "8_3", Name= "Ferrari Roma", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel   { Id= "8_4", Name= "Ferrari SF90", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel   { Id= "8_5", Name= "Ferrari Portofino" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel   { Id= "8_6", Name= "Ferrari LaFerrari" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Ford",
        Icon= "../src/assets/brandlogos/ford.png",
        Slug= "ford",
        Models = new List<CarModel>{
  new CarModel  { Id= "9_1", Name= "Ford F-150", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_2", Name= "Ford Mustang", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_3", Name= "Ford Explorer", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_4", Name= "Ford Escape" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel  { Id= "9_5", Name= "Ford Bronco" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel  { Id= "9_6", Name= "Ford Edge", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_7", Name= "Ford Expedition", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_8", Name= "Ford Ranger", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel  { Id= "9_9", Name= "Ford Super Duty", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Honda",
        Icon= "../src/assets/brandlogos/honda.png",
        Slug= "honda",
        Models = new List<CarModel>{
 new CarModel  { Id= "10_1", Name= "Honda Civic", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel  { Id= "10_2", Name= "Honda Accord", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel  { Id= "10_3", Name= "Honda CR-V" , Icon= "../src/assets/Modelslogo/car.png"},
 new CarModel  { Id= "10_4", Name= "Honda HR-V" , Icon= "../src/assets/Modelslogo/car.png"},
 new CarModel  { Id= "10_5", Name= "Honda Odyssey" , Icon= "../src/assets/Modelslogo/car.png"},
 new CarModel  { Id= "10_6", Name= "Honda Pilot" , Icon= "../src/assets/Modelslogo/car.png"},
 new CarModel  { Id= "10_7", Name= "Honda RIdgeline", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel  { Id= "10_8", Name= "Honda Insight" , Icon= "../src/assets/Modelslogo/car.png"},
 new CarModel  { Id= "10_9", Name= "Honda Fit", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Hyundai",
        Icon= "../src/assets/brandlogos/hyundai.png",
        Slug= "hyundai",
        Models = new List<CarModel>{
     new CarModel  { Id= "11_1", Name= "Hyundai Elantra" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "11_2", Name= "Hyundai Sonata" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "11_3", Name= "Hyundai Tucson" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "11_4", Name= "Hyundai Santa Fe" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "11_5", Name= "Hyundai Palisade" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel  { Id= "11_6", Name= "Hyundai Kona", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel  { Id= "11_7", Name= "Hyundai Ioniq", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel  { Id= "11_8", Name= "Hyundai Venue", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Mercedes-Benz",
        Icon= "../src/assets/brandlogos/mercedes-benz.png",
        Slug= "mercedes-benz",
        Models = new List<CarModel>{
    new CarModel   { Id= "12_1", Name= "Mercedes-Benz C-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_2", Name= "Mercedes-Benz E-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_3", Name= "Mercedes-Benz S-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_4", Name= "Mercedes-Benz GLA", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_5", Name= "Mercedes-Benz GLC", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_6", Name= "Mercedes-Benz GLE", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_7", Name= "Mercedes-Benz G-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_8", Name= "Mercedes-Benz CLA" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "12_9", Name= "Mercedes-Benz GLS", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "12_10", Name= "Mercedes-Benz A-Class" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Nissan",
        Icon= "../src/assets/brandlogos/nissan.png",
        Slug= "nissan",
        Models = new List<CarModel>{
    new CarModel    { Id= "13_1", Name= "Nissan Altima", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "13_2", Name= "Nissan Maxima", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "13_3", Name= "Nissan Sentra", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "13_4", Name= "Nissan Rogue" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel    { Id= "13_5", Name= "Nissan Murano", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "13_6", Name= "Nissan Pathfinder", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "13_7", Name= "Nissan Leaf" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel    { Id= "13_8", Name= "Nissan Armada" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel    { Id= "13_9", Name= "Nissan Kicks" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Porsche",
        Icon= "../src/assets/brandlogos/porsche.png",
        Slug= "porsche",
        Models = new List<CarModel>{
 new CarModel   { Id= "14_1", Name= "Porsche 911", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_2", Name= "Porsche Cayenne", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_3", Name= "Porsche Macan", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_4", Name= "Porsche Taycan", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_5", Name= "Porsche PaNamera", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_6", Name= "Porsche 718 Cayman", Icon= "../src/assets/Modelslogo/car.png" },
 new CarModel   { Id= "14_7", Name= "Porsche 718 Boxster" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Toyota",
        Icon= "../src/assets/brandlogos/toyota.png",
        Slug= "toyota",
        Models = new List<CarModel>{
new CarModel   { Id= "15_1", Name= "Toyota Camry", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel   { Id= "15_2", Name= "Toyota Corolla" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel   { Id= "15_3", Name= "Toyota RAV4" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel   { Id= "15_4", Name= "Toyota Highlander", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel   { Id= "15_5", Name= "Toyota Tacoma", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel   { Id= "15_6", Name= "Toyota Tundra", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel   { Id= "15_7", Name= "Toyota Prius", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel   { Id= "15_8", Name= "Toyota C-HR" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel   { Id= "15_9", Name= "Toyota 4Runner" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel   { Id= "15_10", Name= "Toyota Avalon" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Volkswagen",
        Icon= "../src/assets/brandlogos/volkswagen.png",
        Slug= "volkswagen",
        Models = new List<CarModel>{
new CarModel  { Id= "16_1", Name= "Volkswagen Golf", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "16_2", Name= "Volkswagen Jetta" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "16_3", Name= "Volkswagen Passat" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "16_4", Name= "Volkswagen Tiguan" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "16_5", Name= "Volkswagen Atlas", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "16_6", Name= "Volkswagen Id.4" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "16_7", Name= "Volkswagen Arteon" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "16_8", Name= "Volkswagen Touareg", Icon= "../src/assets/Modelslogo/car.png" }
       }},
            new CarBrand {
        Name= "Audi",
        Icon= "../src/assets/brandlogos/audi.png",
        Slug= "audi",
        Models = new List<CarModel>{
new CarModel  { Id= "3_1", Name= "Audi A3", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_2", Name= "Audi A4", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_3", Name= "Audi A6", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_4", Name= "Audi Q3", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_5", Name= "Audi Q5", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_6", Name= "Audi Q7", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_7", Name= "Audi Q8", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_8", Name= "Audi R8" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel  { Id= "3_9", Name= "Audi S3", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_10", Name= "Audi S4", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_11", Name= "Audi S5", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel  { Id= "3_12", Name= "Audi SQ5", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Bentley",
        Icon= "../src/assets/brandlogos/bentley.png",
        Slug= "bentley",
        Models = new List<CarModel>{
  new CarModel   { Id= "4_1", Name= "Bentley Continental GT", Icon= "../src/assets/Modelslogo/car.png" },
  new CarModel   { Id= "4_2", Name= "Bentley Bentayga" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel   { Id= "4_3", Name= "Bentley Flying Spur" , Icon= "../src/assets/Modelslogo/car.png"},
  new CarModel   { Id= "4_4", Name= "Bentley Mulsanne", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "BMW",
        Icon= "../src/assets/brandlogos/bmw.png",
        Slug= "bmw",
        Models = new List<CarModel>{
new CarModel     { Id= "5_1", Name= "BMW 1 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_2", Name= "BMW 2 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_3", Name= "BMW 3 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_4", Name= "BMW 4 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_5", Name= "BMW 5 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_6", Name= "BMW 7 Series", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_7", Name= "BMW 8 Series" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_8", Name= "BMW X1", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_9", Name= "BMW X2" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_10", Name= "BMW X3" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_11", Name= "BMW X4" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_12", Name= "BMW X5", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_13", Name= "BMW X6", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_14", Name= "BMW X7" , Icon= "../src/assets/Modelslogo/car.png"},
new CarModel     { Id= "5_15", Name= "BMW Z4", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_16", Name= "BMW i3", Icon= "../src/assets/Modelslogo/car.png" },
new CarModel     { Id= "5_17", Name= "BMW i8", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Chevrolet",
        Icon= "../src/assets/brandlogos/chevrolet.png",
        Slug= "chevrolet",
        Models = new List<CarModel>{
        new CarModel   { Id= "6_1", Name= "Chevrolet Malibu", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_2", Name= "Chevrolet Camaro", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_3", Name= "Chevrolet Corvette" , Icon= "../src/assets/Modelslogo/car.png"},
        new CarModel   { Id= "6_4", Name= "Chevrolet Tahoe", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_5", Name= "Chevrolet Silverado", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_6", Name= "Chevrolet Equinox", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_7", Name= "Chevrolet Traverse", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel   { Id= "6_8", Name= "Chevrolet Suburban" , Icon= "../src/assets/Modelslogo/car.png"},
        new CarModel   { Id= "6_9", Name= "Chevrolet Blazer", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Dodge",
        Icon= "../src/assets/brandlogos/dodge.png",
        Slug= "dodge",
        Models = new List<CarModel>{
      new CarModel  { Id= "7_1", Name= "Dodge Charger", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel  { Id= "7_2", Name= "Dodge Challenger", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel  { Id= "7_3", Name= "Dodge Durango", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel  { Id= "7_4", Name= "Dodge Journey" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel  { Id= "7_5", Name= "Dodge Grand Caravan", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Ferrari",
        Icon= "../src/assets/brandlogos/ferrari.png",
        Slug= "ferrari",
        Models = new List<CarModel>{
     new CarModel   { Id= "8_1", Name= "Ferrari 488", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel   { Id= "8_2", Name= "Ferrari F8", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel   { Id= "8_3", Name= "Ferrari Roma", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel   { Id= "8_4", Name= "Ferrari SF90", Icon= "../src/assets/Modelslogo/car.png" },
     new CarModel   { Id= "8_5", Name= "Ferrari Portofino" , Icon= "../src/assets/Modelslogo/car.png"},
     new CarModel   { Id= "8_6", Name= "Ferrari LaFerrari" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Ford",
        Icon= "../src/assets/brandlogos/ford.png",
        Slug= "ford",
        Models = new List<CarModel>{
      new CarModel   { Id= "9_1", Name= "Ford F-150", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_2", Name= "Ford Mustang", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_3", Name= "Ford Explorer", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_4", Name= "Ford Escape" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "9_5", Name= "Ford Bronco" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "9_6", Name= "Ford Edge", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_7", Name= "Ford Expedition", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_8", Name= "Ford Ranger", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "9_9", Name= "Ford Super Duty", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Honda",
        Icon= "../src/assets/brandlogos/honda.png",
        Slug= "honda",
        Models = new List<CarModel>{
   new CarModel  { Id= "10_1", Name= "Honda Civic", Icon= "../src/assets/Modelslogo/car.png" },
   new CarModel  { Id= "10_2", Name= "Honda Accord", Icon= "../src/assets/Modelslogo/car.png" },
   new CarModel  { Id= "10_3", Name= "Honda CR-V" , Icon= "../src/assets/Modelslogo/car.png"},
   new CarModel  { Id= "10_4", Name= "Honda HR-V" , Icon= "../src/assets/Modelslogo/car.png"},
   new CarModel  { Id= "10_5", Name= "Honda Odyssey" , Icon= "../src/assets/Modelslogo/car.png"},
   new CarModel  { Id= "10_6", Name= "Honda Pilot" , Icon= "../src/assets/Modelslogo/car.png"},
   new CarModel  { Id= "10_7", Name= "Honda RIdgeline", Icon= "../src/assets/Modelslogo/car.png" },
   new CarModel  { Id= "10_8", Name= "Honda Insight" , Icon= "../src/assets/Modelslogo/car.png"},
   new CarModel  { Id= "10_9", Name= "Honda Fit", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Hyundai",
        Icon= "../src/assets/brandlogos/hyundai.png",
        Slug= "hyundai",
        Models = new List<CarModel>{
    new CarModel   { Id= "11_1", Name= "Hyundai Elantra" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "11_2", Name= "Hyundai Sonata" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "11_3", Name= "Hyundai Tucson" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "11_4", Name= "Hyundai Santa Fe" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "11_5", Name= "Hyundai Palisade" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel   { Id= "11_6", Name= "Hyundai Kona", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "11_7", Name= "Hyundai Ioniq", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel   { Id= "11_8", Name= "Hyundai Venue", Icon= "../src/assets/Modelslogo/car.png" }
        }},
            new CarBrand {
        Name= "Mercedes-Benz",
        Icon= "../src/assets/brandlogos/mercedes-benz.png",
        Slug= "mercedes-benz",
        Models = new List<CarModel>{
    new CarModel    { Id= "12_1", Name= "Mercedes-Benz C-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_2", Name= "Mercedes-Benz E-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_3", Name= "Mercedes-Benz S-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_4", Name= "Mercedes-Benz GLA", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_5", Name= "Mercedes-Benz GLC", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_6", Name= "Mercedes-Benz GLE", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_7", Name= "Mercedes-Benz G-Class", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_8", Name= "Mercedes-Benz CLA" , Icon= "../src/assets/Modelslogo/car.png"},
    new CarModel    { Id= "12_9", Name= "Mercedes-Benz GLS", Icon= "../src/assets/Modelslogo/car.png" },
    new CarModel    { Id= "12_10", Name= "Mercedes-Benz A-Class" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Nissan",
        Icon= "../src/assets/brandlogos/nissan.png",
        Slug= "nissan",
        Models = new List<CarModel>{
        new CarModel     { Id= "13_1", Name= "Nissan Altima", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel     { Id= "13_2", Name= "Nissan Maxima", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel     { Id= "13_3", Name= "Nissan Sentra", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel     { Id= "13_4", Name= "Nissan Rogue" , Icon= "../src/assets/Modelslogo/car.png"},
        new CarModel     { Id= "13_5", Name= "Nissan Murano", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel     { Id= "13_6", Name= "Nissan Pathfinder", Icon= "../src/assets/Modelslogo/car.png" },
        new CarModel     { Id= "13_7", Name= "Nissan Leaf" , Icon= "../src/assets/Modelslogo/car.png"},
        new CarModel     { Id= "13_8", Name= "Nissan Armada" , Icon= "../src/assets/Modelslogo/car.png"},
        new CarModel     { Id= "13_9", Name= "Nissan Kicks" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Porsche",
        Icon= "../src/assets/brandlogos/porsche.png",
        Slug= "porsche",
        Models = new List<CarModel>{
      new CarModel { Id= "14_1", Name= "Porsche 911", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_2", Name= "Porsche Cayenne", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_3", Name= "Porsche Macan", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_4", Name= "Porsche Taycan", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_5", Name= "Porsche PaNamera", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_6", Name= "Porsche 718 Cayman", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel { Id= "14_7", Name= "Porsche 718 Boxster" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
        Name= "Toyota",
        Icon= "../src/assets/brandlogos/toyota.png",
        Slug= "toyota",
        Models = new List<CarModel>{
      new CarModel   { Id= "15_1", Name= "Toyota Camry", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "15_2", Name= "Toyota Corolla" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "15_3", Name= "Toyota RAV4" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "15_4", Name= "Toyota Highlander", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "15_5", Name= "Toyota Tacoma", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "15_6", Name= "Toyota Tundra", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "15_7", Name= "Toyota Prius", Icon= "../src/assets/Modelslogo/car.png" },
      new CarModel   { Id= "15_8", Name= "Toyota C-HR" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "15_9", Name= "Toyota 4Runner" , Icon= "../src/assets/Modelslogo/car.png"},
      new CarModel   { Id= "15_10", Name= "Toyota Avalon" , Icon= "../src/assets/Modelslogo/car.png"}
        }},
            new CarBrand {
                  Name= "Volkswagen",
                  Icon= "../src/assets/brandlogos/volkswagen.png",
                  Slug= "volkswagen",
                  Models = new List<CarModel>{
                new CarModel  { Id= "16_1", Name= "Volkswagen Golf", Icon= "../src/assets/Modelslogo/car.png" },
                new CarModel  { Id= "16_2", Name= "Volkswagen Jetta" , Icon= "../src/assets/Modelslogo/car.png"},
                new CarModel  { Id= "16_3", Name= "Volkswagen Passat" , Icon= "../src/assets/Modelslogo/car.png"},
                new CarModel  { Id= "16_4", Name= "Volkswagen Tiguan" , Icon= "../src/assets/Modelslogo/car.png"},
                new CarModel  { Id= "16_5", Name= "Volkswagen Atlas", Icon= "../src/assets/Modelslogo/car.png" },
                new CarModel  { Id= "16_6", Name= "Volkswagen Id.4" , Icon= "../src/assets/Modelslogo/car.png"},
                new CarModel  { Id= "16_7", Name= "Volkswagen Arteon" , Icon= "../src/assets/Modelslogo/car.png"},
                new CarModel  { Id= "16_8", Name= "Volkswagen Touareg", Icon= "../src/assets/Modelslogo/car.png" }
        }
        }
        };

        // Generate fake tuning database information
        private static List<TuningDatabaseInfo> tuningDatabase = new List<TuningDatabaseInfo>
        {
             // Volkswagen Models
                new TuningDatabaseInfo
                {
                    Id = "16_1",
                    Brand = "Volkswagen",
                    Information = "Golf tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "Golf4", Year = "1999-2004", Engine = "1.8T", Horsepower = "150PS", Variant = "petrol", EcuType = "ME7.5", TuningId = "16_1_1" },
                        new TuningVariant { TypeName = "Golf4", Year = "2000-2004", Engine = "2.0", Horsepower = "115PS", Variant = "petrol", EcuType = "ME7.5", TuningId = "16_1_2" },
                        new TuningVariant { TypeName = "Golf4", Year = "2001-2004", Engine = "1.9TDI", Horsepower = "130PS", Variant = "diesel", EcuType = "EDC15", TuningId = "16_1_3" },
                        new TuningVariant { TypeName = "Golf5", Year = "2005-2010", Engine = "1.4TSI", Horsepower = "170PS", Variant = "petrol", EcuType = "MED17", TuningId = "16_1_4" },
                        new TuningVariant { TypeName = "Golf5", Year = "2006-2010", Engine = "2.0TDI", Horsepower = "140PS", Variant = "diesel", EcuType = "EDC16U34", TuningId = "16_1_5" },
                        new TuningVariant { TypeName = "Golf5", Year = "2008-2010", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED9.1", TuningId = "16_1_6" },
                        new TuningVariant { TypeName = "Golf6", Year = "2008-2013", Engine = "1.4TSI", Horsepower = "160PS", Variant = "petrol", EcuType = "MED17", TuningId = "16_1_7" },
                        new TuningVariant { TypeName = "Golf6", Year = "2008-2013", Engine = "2.0TDI", Horsepower = "140PS", Variant = "diesel", EcuType = "EDC17CP14", TuningId = "16_1_8" },
                        new TuningVariant { TypeName = "Golf6", Year = "2008-2013", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED17.5", TuningId = "16_1_9" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "16_2",
                    Brand = "Volkswagen",
                    Information = "Jetta tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "Jetta B1", Year = "2005-2010", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED9.1", TuningId = "16_2_1" },
                        new TuningVariant { TypeName = "Jetta B1", Year = "2006-2010", Engine = "1.9TDI", Horsepower = "105PS", Variant = "diesel", EcuType = "EDC16U1", TuningId = "16_2_2" },
                        new TuningVariant { TypeName = "Jetta B2", Year = "2007-2010", Engine = "2.0TDI", Horsepower = "140PS", Variant = "diesel", EcuType = "EDC17CP14", TuningId = "16_2_3" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "16_3",
                    Brand = "Volkswagen",
                    Information = "Passat tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "Passat B6", Year = "2005-2010", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED9.1", TuningId = "16_3_1" },
                        new TuningVariant { TypeName = "Passat B6", Year = "2006-2010", Engine = "2.0TDI", Horsepower = "170PS", Variant = "diesel", EcuType = "EDC16CP34", TuningId = "16_3_2" },
                        new TuningVariant { TypeName = "Passat B6", Year = "2005-2010", Engine = "3.2V6", Horsepower = "250PS", Variant = "petrol", EcuType = "ME7.1.1", TuningId = "16_3_3" },
                        new TuningVariant { TypeName = "Passat B7", Year = "2010-2015", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED17.5", TuningId = "16_3_4" },
                        new TuningVariant { TypeName = "Passat B7", Year = "2010-2015", Engine = "2.0TDI", Horsepower = "170PS", Variant = "diesel", EcuType = "EDC17CP14", TuningId = "16_3_5" },
                        new TuningVariant { TypeName = "Passat B7", Year = "2010-2015", Engine = "3.2V6", Horsepower = "250PS", Variant = "petrol", EcuType = "ME7.1.1", TuningId = "16_3_6" }
                    }
                },

                // BMW Models
                new TuningDatabaseInfo
                {
                    Id = "5_1",
                    Brand = "BMW",
                    Information = "1 Series tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "1 Series E81", Year = "2007-2012", Engine = "2.0", Horsepower = "143PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_1_1" },
                        new TuningVariant { TypeName = "1 Series E81", Year = "2007-2012", Engine = "2.0d", Horsepower = "177PS", Variant = "diesel", EcuType = "EDC17C41", TuningId = "5_1_2" },
                        new TuningVariant { TypeName = "1 Series E81", Year = "2007-2012", Engine = "1.6", Horsepower = "116PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_1_3" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "5_3",
                    Brand = "BMW",
                    Information = "3 Series tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "3 Series E90", Year = "2005-2012", Engine = "2.0", Horsepower = "150PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_3_1" },
                        new TuningVariant { TypeName = "3 Series E90", Year = "2005-2012", Engine = "2.0d", Horsepower = "163PS", Variant = "diesel", EcuType = "EDC17C06", TuningId = "5_3_2" },
                        new TuningVariant { TypeName = "3 Series E90", Year = "2005-2012", Engine = "3.0", Horsepower = "231PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_3_3" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "5_4",
                    Brand = "BMW",
                    Information = "5 Series tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "5 Series E60", Year = "2005-2010", Engine = "2.0", Horsepower = "143PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_4_1" },
                        new TuningVariant { TypeName = "5 Series E60", Year = "2005-2010", Engine = "3.0d", Horsepower = "204PS", Variant = "diesel", EcuType = "EDC17C06", TuningId = "5_4_2" },
                        new TuningVariant { TypeName = "5 Series E60", Year = "2005-2010", Engine = "3.0", Horsepower = "258PS", Variant = "petrol", EcuType = "MEVD17", TuningId = "5_4_3" }
                    }
                },

                // Audi Models
                new TuningDatabaseInfo
                {
                    Id = "7_1",
                    Brand = "Audi",
                    Information = "A3 tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "A3 8P", Year = "2003-2013", Engine = "1.6", Horsepower = "102PS", Variant = "petrol", EcuType = "MED9", TuningId = "7_1_1" },
                        new TuningVariant { TypeName = "A3 8P", Year = "2003-2013", Engine = "2.0T", Horsepower = "200PS", Variant = "petrol", EcuType = "MED9", TuningId = "7_1_2" },
                        new TuningVariant { TypeName = "A3 8P", Year = "2003-2013", Engine = "2.0TDI", Horsepower = "140PS", Variant = "diesel", EcuType = "EDC16", TuningId = "7_1_3" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "7_2",
                    Brand = "Audi",
                    Information = "A4 tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "A4 B7", Year = "2005-2008", Engine = "1.8T", Horsepower = "160PS", Variant = "petrol", EcuType = "MED9", TuningId = "7_2_1" },
                        new TuningVariant { TypeName = "A4 B7", Year = "2005-2008", Engine = "2.0TDI", Horsepower = "140PS", Variant = "diesel", EcuType = "EDC16", TuningId = "7_2_2" },
                        new TuningVariant { TypeName = "A4 B7", Year = "2005-2008", Engine = "3.2", Horsepower = "250PS", Variant = "petrol", EcuType = "ME7.5", TuningId = "7_2_3" }
                    }
                },
                new TuningDatabaseInfo
                {
                    Id = "7_3",
                    Brand = "Audi",
                    Information = "A6 tuning information...",
                    Variants = new List<TuningVariant>
                    {
                        new TuningVariant { TypeName = "A6 C6", Year = "2005-2011", Engine = "2.0T", Horsepower = "180PS", Variant = "petrol", EcuType = "MED9", TuningId = "7_3_1" },
                        new TuningVariant { TypeName = "A6 C6", Year = "2005-2011", Engine = "2.7TDI", Horsepower = "190PS", Variant = "diesel", EcuType = "EDC16", TuningId = "7_3_2" },
                        new TuningVariant { TypeName = "A6 C6", Year = "2005-2011", Engine = "3.0T", Horsepower = "300PS", Variant = "petrol", EcuType = "MED9", TuningId = "7_3_3" }
                    }
                }
        };

        // Generate fake tuning special info data
        private static List<TuningSpecialInfo> tuningSpecialInfos = new List<TuningSpecialInfo>
        {
          new TuningSpecialInfo
            {
                Id = "16_1_1",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_2",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+20ps/+45nm", Value1 = 20, Value2 = 45, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+30ps/+65nm", Value1 = 30, Value2 = 65, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_3",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+35nm", Value1 = 15, Value2 = 35, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_4",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_5",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_6",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_7",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_8",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "16_1_9",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            }

        };

        public List<CarBrand> getCarBrands()
        {
            return carBrands;
        }

        public List<TuningDatabaseInfo> getTuningDatabaseInfoById(string Id = "")
        {
            return tuningDatabase.Where(x=>x.Id.Equals(Id)).ToList();
        }

        public List<TuningSpecialInfo> getTuningSpecialInfoByTuningId(string Id = "")
        {
            return tuningSpecialInfos.Where(x => x.Id.Equals(Id)).ToList();
        }
    }

}