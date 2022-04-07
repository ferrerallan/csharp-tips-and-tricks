

using Newtonsoft.Json;

void log(string line){
  Console.ForegroundColor = ConsoleColor.White;
  Console.WriteLine("    "+line);
}

void header(string line){
  Console.ForegroundColor = ConsoleColor.Yellow;
  Console.WriteLine("### "+line+" ###");
}

void h2(string line){
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine("  "+line);
}


var simpleFruitList = new List<string> {"banana", "apple", "pineapple"};
header("Simple list");
h2("listing...");
simpleFruitList.ForEach(item=>log(item));

h2("filtering by initial letter");
simpleFruitList.Where(item => item.ToCharArray()[0]=='b')
        .ToList<string>().ForEach(item => log(item));

h2("filtering by length");
simpleFruitList.Where(item => item.Length >5)
        .ToList<string>()
        .ForEach(item => log(item));

/*Dictionary

Important Points:

The Dictionary class implements the
IDictionary<TKey,TValue> Interface
IReadOnlyCollection<KeyValuePair<TKey,TValue>> Interface
IReadOnlyDictionary<TKey,TValue> Interface
IDictionary Interface
In Dictionary, the key cannot be null, but value can be.
In Dictionary, key must be unique. Duplicate keys are not allowed if you try to use duplicate key then compiler will throw an exception.
In Dictionary, you can only store same types of elements.
The capacity of a Dictionary is the number of elements that Dictionary can hold.
var simpleDictionary = new List<string> {"banana", "apple", "pineapple"};*/

var simpleDictionaryIntString = new Dictionary<int, string>(); 
simpleDictionaryIntString.Add(1,"guitar");
simpleDictionaryIntString.Add(2,"piano");
simpleDictionaryIntString.Add(3,"bass");

header("Dictionaries");
h2("listing...");
foreach (var item in simpleDictionaryIntString){
  log($"key:{item.Value} value:{item.Value}");  
}

h2("filtering and mapping to list...");
simpleDictionaryIntString.Where(item => item.Key>1)
    .ToList()
    .ForEach(item => log(item.Value));


header("List of Records");
var allan = new SoccerPlayer("Allan", "Barcelona", 39);
var listSoccerPleyers = new List<SoccerPlayer>{
  new SoccerPlayer("Allan","Barcelona",39),
  new SoccerPlayer("Romario","Vasco",50),
  new SoccerPlayer("R.Gaucho","Barcelona",45)
};
h2("listing...");
listSoccerPleyers.ForEach(player=>log($"name:{player.name} team:{player.teamName} age:{player.age}"));

h2("grouping by teamName...");
var result = from player in listSoccerPleyers
              group player by player.teamName into groupedPlayers
              select groupedPlayers;
foreach ( var team in result){
  log(team.Key);
  foreach (var player in team) {
    log("-"+player.name);
  }
}

h2("grouping by teamName by another way...");
var query = listSoccerPleyers.GroupBy(player=>player.teamName);
foreach ( var team in query){
  log(team.Key);
  foreach (var player in team) {
    log("-"+player.name);
  }
}

h2("converting into a json...");
var grouped = listSoccerPleyers
    .GroupBy(p =>  p.teamName )
    .Select(g => new {
        team = g.Key,
        count = g.Count(),
        playersList = g.Select(i => new {
              name = i.name,
              age = i.age
          })
    }
    );
foreach (var item in grouped) {
  log($"{item.team} | count: {item.count}");
  foreach(var detail in item.playersList) {
    log($"--{detail.name} | {detail.age.ToString()}");
  }
}
h2("serializing object...");
string resultJSON= JsonConvert.SerializeObject(grouped);
log(resultJSON);

header("Extension methods");
h2("extending strings..");
string otherName = "Allan";
log(otherName.addLastName());


public record SoccerPlayer(string name, string teamName, int age);



