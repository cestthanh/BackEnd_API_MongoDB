using MongoDB.Driver;
using UserApiDemo.Models;

namespace UserApiDemo.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IConfiguration config)
        {
            var settings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollection); 

        }
        public List<User> GetAll() => _users.Find(u => true).ToList();
        public User GetById(int id) => _users.Find(u => u.Id == id).FirstOrDefault();
        public void Add(User user) => _users.InsertOne(user);
        public void Delete(int id) => _users.DeleteOne(u => u.Id == id);

    }
}
