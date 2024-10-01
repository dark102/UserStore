using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UserStore.Models;


namespace TestUserStore
{
    public class UsersControllerTests
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:8080/api/Users"; // Заменить путь к контейнеру;

        public UsersControllerTests()
        {
            _client = new HttpClient();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUserById()
        {
            var userId = Guid.Parse("B776ECB3-A316-49A8-2210-08DCE22AA2C0");  // Заменить на валидный гуид;
            var response = await _client.GetAsync($"{_baseUrl}/GetUserById?id={userId}");
            string responseText = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseText);
            Assert.Equal(userId, user.Id);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUserWithMailDevice()
        {
            var user = new User { Name = "Artem", Email = "artem@example.com"};
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/CreateUser")
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            request.Headers.Add("x-Device", "mail");
            var response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseText);
            Assert.Equal(user.Name, createdUser.Name);
            Assert.Equal(user.Email, createdUser.Email);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUserWithMobileDevice()
        {
            var user = new User { Phone = "71112223344" };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/CreateUser")
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            request.Headers.Add("x-Device", "mobile");
            var response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseText);
            Assert.Equal(user.Phone, createdUser.Phone);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUserWithWebDevice()
        {
            var user = new User
            {
                Surname = "Nikolaevich",
                Name = "Artem",
                Patronymic = "Pechenko",
                DateOfBirth = "1990-01-01",
                PassportNumber = "1234 567809",
                PlaceOfBirth = "City",
                Phone = "71234567890",
                RegistrationAddress = "123 Street"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/CreateUser")
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            request.Headers.Add("x-Device", "web");
            var response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseText);
            Assert.Equal(user.Surname, createdUser.Surname);
            Assert.Equal(user.Name, createdUser.Name);
            Assert.Equal(user.Patronymic, createdUser.Patronymic);
        }

        [Fact]
        public async Task Search_ShouldReturnUsersBasedOnQueryParameters()
        {
            var queryParams = "surname=Nikolaevich&name=Artem";
            var response = await _client.GetAsync($"{_baseUrl}/Search?{queryParams}");
            string responseText = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(responseText);
            Assert.NotEmpty(users);
            Assert.Equal("Nikolaevich", users[0].Surname);
            Assert.Equal("Artem", users[0].Name);
        }
    }
}