dotnetapp/Managers/IPlayerManager.cs
 
using dotnetapp.Models;
namespace dotnetapp.Managers{
    public interface IPlayerManager{
        void AddPlayer(int teamId);
        void ListPlayers();
        void FindPlayer(string playerName);
        void EditPlayer();
        void DeletePlayer();
        Player FindById(int id);
        public void AddPlayerToDB(int playerId);
        public void ListPlayersFromDB();
        public void DeletePlayerFromDB();
        public void EditPlayerInDB();
    }
}
 
dotnetapp/Managers/ITeamManager.cs
 
 
using dotnetapp.Models;
 
 
 
namespace dotnetapp.Managers{
    public interface ITeamManager{
        void AddTeam();
        void ListTeams();
       
        public void AddTeamToDB();
        public void ListTeamsFromDB();
    }
}
 
dotnetapp/Managers/PlayerManager.cs
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using dotnetapp.Models;
using dotnetapp.Services;
// using Internal;
 
 
 
 
namespace dotnetapp.Managers
{
public class PlayerManager:IPlayerManager{
    PlayersService playerService = new PlayersService();
    public void AddPlayer(int teamId){
        Player player = new Player();
        player.Id = PlayersService.Players.Count+1;
        player.TeamId = teamId;
        Console.Write("Enter Player Name: ");
        player.Name= Console.ReadLine();
 
        Console.Write("Enter Player Age: ");
        player.Age = int.Parse(Console.ReadLine());
 
        Console.Write("Enter Player Category: ");
        player.Category = Console.ReadLine();
 
        Console.Write("Enter Bidding Price: ");
        player.BiddingPrice = decimal.Parse(Console.ReadLine());
 
        playerService.AddPlayer(player);
 
        Console.WriteLine("Player added successfully!");
 
    }
    public void ListPlayers(){
        if(PlayersService.Players.Count==0){
            Console.WriteLine("No Player");
            return;
        }
        Console.WriteLine("List of Players");
 
        foreach(var player in PlayersService.Players){
            Console.WriteLine($"Player ID: {player.Id}, Name: {player.Name}, Age: {player.Age}, Category: {player.Category}, Bidding Price: ${player.BiddingPrice:N2}");
        }
    }
    public void FindPlayer(string playerName){
        var player = PlayersService.Players.FirstOrDefault(p=>p.Name.ToLower()==playerName.ToLower());
        if(player==null){
            Console.WriteLine("Player Not Found");
        }else{
            Console.WriteLine($"Player ID: {player.Id}, Name: {player.Name}, Age: {player.Age}, Category: {player.Category}, Bidding Price: ${player.BiddingPrice:N2}");
 
        }
    }
     public Player FindById(int id){
        return PlayersService.Players.FirstOrDefault(p=>p.Id==id);
     }
 
     public void EditPlayer(){
        Console.WriteLine("Enter Player ID to edit: ");
        int id = int.Parse(Console.ReadLine());
 
        var player = PlayersService.Players.FirstOrDefault(p=>p.Id==id);
        if(player==null){
            Console.WriteLine("Player not found!");
            return;
        }
 
 
        Console.Write("Enter new Player Name");
        string name = Console.ReadLine();
        Console.Write("Enter new Player Age");
        string age  = Console.ReadLine();
        Console.Write("Enter new Player Category");
        string category = Console.ReadLine();
        Console.Write("Enter new Player Price");
        string price = Console.ReadLine();
       
        if(!string.IsNullOrWhiteSpace(name)) player.Name = name;
        if(!string.IsNullOrWhiteSpace(age)) player.Age = int.Parse(age);
        if(!string.IsNullOrWhiteSpace(category)) player.Category = category;
        if(!string.IsNullOrWhiteSpace(name)) player.BiddingPrice = decimal.Parse(price);
       
     }
     public void DeletePlayer(){
        Console.WriteLine("Enter the ID to be deleted");
        int id = int.Parse(Console.ReadLine());
        var player = PlayersService.Players.FirstOrDefault(p=>p.Id==id);
        if(player==null){
            return;
        }
        PlayersService.Players.Remove(player);
     }
     public void AddPlayerToDB(int PlayerId){
        playerService.AddPlayerToDB(PlayerId);
     }
     public void ListPlayersFromDB(){
        playerService.ListPlayersFromDB();
     }
     public void DeletePlayerFromDB(){
        playerService.DeletePlayerFromDB();
     }
     public void EditPlayerInDB(){
        playerService.EditPlayerInDB();
     }
 
 
 
}
   
}
 
dotnetapp/Managers/TeamManager.cs
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Managers;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Data;
using System.Data.SqlClient;
// using Internal;
namespace dotnetapp.Managers
{
    public class TeamManager: ITeamManager{
        TeamsService teamService = new TeamsService();
        public void AddTeam(){
            Team team = new Team();
            team.Id =  TeamsService.Teams.Count+1;
            Console.WriteLine("Enter the Team Name: ");
            team.Name = Console.ReadLine();
            Console.WriteLine("Enter the Team's Max Budget: ");
            team.MaximumBudget = decimal.Parse(Console.ReadLine());
            teamService.AddTeam(team);
        }
 
        public void ListTeams(){
            if(TeamsService.Teams.Count==0){
                return;
            }
            Console.WriteLine("List: \n");
            foreach(var team in TeamsService.Teams){
                    Console.WriteLine($"Team ID {team.Id}\nTeam Name {team.Name}\nMax Budget {team.MaximumBudget}");
            }
        }
        public void AddTeamToDB(){
            teamService.AddTeamToDB();
        }
        public void ListTeamsFromDB(){
            teamService.ListTeamsFromDB();
        }
    }
}
dotnetapp/Services/PlayersService.cs
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Models;
using System.Data;
using System.Data.SqlClient;
// using Internal;
 
 
namespace dotnetapp.Services
{
    public class PlayersService{
        private string connectionString  = "User ID=sa;password=examlyMssql@123;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False";
        public static List<Player> Players = new List<Player>();
 
        public void AddPlayer(Player player){
            Players.Add(player);
        }
        public List<Player>ListPlayers(){
            return Players;
        }
        public Player FindPlayer(string playerName){
            return Players.FirstOrDefault(p=>p.Name.Equals(playerName,StringComparison.OrdinalIgnoreCase));
        }
        public Player FindById(int id){
            return Players.FirstOrDefault(p=>p.Id==id);
        }
 
        public void EditPlayer(Player updatedPlayer){
            Player existingPlayer = Players.FirstOrDefault(p=>p.Id==updatedPlayer.Id);
            if(existingPlayer!=null){
                existingPlayer.Name = updatedPlayer.Name;
                existingPlayer.Age = updatedPlayer.Age;
                existingPlayer.Category = updatedPlayer.Category;
                existingPlayer.BiddingPrice = updatedPlayer.BiddingPrice;
                existingPlayer.TeamId = updatedPlayer.TeamId;
            }
        }
        public void DeletePlayer(int playerId){
            Player player = Players.FirstOrDefault(p=>p.Id==playerId);
            if(player!=null){
                Players.Remove(player);
            }
        }
        public void AddPlayerToDB(int teamId){
            Console.WriteLine("Enter Name: ");
            string name  = Console.ReadLine();
            Console.WriteLine("Enter Age: ");
            int age =  int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();
            Console.WriteLine("Bidding Price: ");
            decimal biddngPrice = decimal.Parse(Console.ReadLine());
            try{
                SqlConnection con =  new SqlConnection(connectionString);
                con.Open();
                string q = "INSERT INTO Players VALUES(@Name,@Age,@Category,@BiddingPrice,@TeamId)";
                SqlCommand com = new SqlCommand(q,con);
                com.Parameters.AddWithValue("@Name",name);
                com.Parameters.AddWithValue("@Age",age);
                com.Parameters.AddWithValue("@Category",category);
                com.Parameters.AddWithValue("@BiddingPrice",biddngPrice);
                com.Parameters.AddWithValue("@TeamId",teamId);
                int row = com.ExecuteNonQuery();
                if(row>0){
                    Console.WriteLine("Player Added Successfully.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
        public void ListPlayersFromDB(){
              SqlConnection con =  new SqlConnection(connectionString);
              con.Open();
                string q = "SELECT * FROM Players";
                SqlCommand com = new SqlCommand(q,con);
                SqlDataReader reader =  com.ExecuteReader();
                while(reader.Read()){
                    Console.WriteLine($"Player ID: {reader["Id"]}\nName: {reader["Name"]}\nAge: {reader["Age"]}\nCategory: {reader["Category"]}\nBidding Price: {reader["BiddingPrice"]}\nTeam: {reader["TeamId"]}\n");
                }
        }
        public void EditPlayerInDB(){
            Console.WriteLine("Enter ID of player to Edit.");
            int id = int.Parse(Console.ReadLine());
 
            Console.WriteLine("Enter Name of player to Edit.");
            string name  = Console.ReadLine();
 
            Console.WriteLine("Enter Age of player to Edit.");
            int age = int.Parse(Console.ReadLine());
 
            Console.WriteLine("Enter Category of player to Edit.");
            string category   =Console.ReadLine();
 
            Console.WriteLine("Enter Bidding price of player to Edit.");
            decimal biddngPrice = decimal.Parse(Console.ReadLine());
            try{
                SqlConnection con =  new SqlConnection(connectionString);
                con.Open();
                string q = "UPDATE Players SET Name =@Name,Age = @Age,Category= @Category,BiddingPrice = @BiddingPrice WHERE @Id=Id";
                SqlCommand com = new SqlCommand(q,con);
                com.Parameters.AddWithValue("@Name",name);
                com.Parameters.AddWithValue("@Age",age);
                com.Parameters.AddWithValue("@Category",category);
                com.Parameters.AddWithValue("@BiddingPrice",biddngPrice);
                com.Parameters.AddWithValue("@Id",id);
                int rowEffect = com.ExecuteNonQuery();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
       
        }
        public void DeletePlayerFromDB(){
            SqlConnection con =  new SqlConnection(connectionString);
            con.Open();
            Console.WriteLine("Enter Id of Player to be deleted");
            int id = int.Parse(Console.ReadLine());
            string q = "DELETE FROM Players WHERE Id= @Id";
            SqlCommand com = new SqlCommand(q,con);
            com.Parameters.AddWithValue("@Id",id);
 
            int rowsAffected = com.ExecuteNonQuery();
            if(rowsAffected>0){
                Console.WriteLine("Player Delete successfully");
            }else{
                 Console.WriteLine("Player not found.");
            }
           
 
        }
    }
}
 
dotnetapp/Services/TeamsService.cs
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Models;
using System.Data;
using System.Data.SqlClient;
// using Internal;
 
namespace dotnetapp.Services
{
    public class TeamsService{
        private string connectionString  = "User ID=sa;password=examlyMssql@123;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False;";
   
        public static List<Team>Teams = new List<Team>();
 
        public void AddTeam(Team team){
            Teams.Add(team);
        }
        public List<Team>ListTeams(){
            return Teams;
        }
        public void AddTeamToDB(){
            Console.WriteLine("Enter the name of team.");
            string name=Console.ReadLine();
            Console.WriteLine("Enter the Maximum Budget of team.");
            decimal maximumBudget = decimal.Parse(Console.ReadLine());
            try{
                SqlConnection con =  new SqlConnection(connectionString);
                con.Open();
                string q = "INSERT INTO Teams VALUES(@Name,@MaximumBudget)";
                SqlCommand com = new SqlCommand(q,con);
                com.Parameters.AddWithValue("@Name",name);
                com.Parameters.AddWithValue("@MaximumBudget",maximumBudget);
                int x = com.ExecuteNonQuery();
            }catch(Exception ex){
                Console.WriteLine("Error: "+ex.Message);
            }
           
        }
        public void ListTeamsFromDB(){
             SqlConnection con =  new SqlConnection(connectionString);
             con.Open();
                string q = "SELECT * FROM Teams";
                SqlCommand com = new SqlCommand(q,con);
                SqlDataReader reader = com.ExecuteReader();
 
                while(reader.Read()){
                Console.WriteLine($"Team ID: {reader["Id"]}\nName: {reader["Name"]}\nMaximumBudget: {reader["MaximumBudget"]}\n");
                }
        }
    }
}
