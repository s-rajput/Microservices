using Newtonsoft.Json;
 public class  PetOwner
        {
            [JsonProperty("name")]
            public string Name { get; set; } 

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("age")]
            public int age { get; set; }
 
            [JsonProperty("pets")]
            public Pet[] Pets { get; set; }
        }

 public class  Pet
        {
            [JsonProperty("name")]
            public string Name { get; set; } 

            [JsonProperty("type")]
            public string Type { get; set; } 
        }