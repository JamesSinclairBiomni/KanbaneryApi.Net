using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KanbaneryApi.Net.Tests
{
    [TestFixture]
    public class KanbaneryApiClientTests
    {
        private MessageHandlerFake messageHandlerFake;
        private HttpClient httpClient;
        private KanbaneryApiClient kanbaneryApiClient;

        [OneTimeSetUp]
        public void Initialise()
        {
            messageHandlerFake = new MessageHandlerFake();
            httpClient = new HttpClient(messageHandlerFake);
            httpClient.BaseAddress = new Uri(MessageHandlerFake.Host);
            kanbaneryApiClient = new KanbaneryApiClient(httpClient, "workspace", "api-token");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            kanbaneryApiClient?.Dispose();
            httpClient?.Dispose();
            messageHandlerFake?.Dispose();
        }

        #region SetApiTokenHeader

        [Test]
        public void SetApiTokenHeader_AddsHeader_WhenDoesNotExist()
        {
            // Arrange & Act
            kanbaneryApiClient.SetApiTokenHeader("myToken");

            // Assert
            var headers = httpClient.DefaultRequestHeaders.GetValues("X-Kanbanery-ApiToken").ToList();
            Assert.That(headers.Count, Is.EqualTo(1));
            Assert.That(headers[0], Is.EqualTo("myToken"));
        }

        [Test]
        public void SetApiTokenHeader_ReplacesHeader_WhenDoesExist()
        {
            // Arrange
            kanbaneryApiClient.SetApiTokenHeader("myOldToken");

            // Act
            kanbaneryApiClient.SetApiTokenHeader("myNewToken");

            // Assert
            var headers = httpClient.DefaultRequestHeaders.GetValues("X-Kanbanery-ApiToken").ToList();
            Assert.That(headers.Count, Is.EqualTo(1));
            Assert.That(headers[0], Is.EqualTo("myNewToken"));
        }

        #endregion

        [Test]
        public async Task GetUser_ReturnsUser()
        {
            // Arrange
            kanbaneryApiClient.SetApiTokenHeader("token");

            // Act
            var result = await kanbaneryApiClient.GetUser();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(123456));
            Assert.That(result.CreatedAt.ToUniversalTime, Is.EqualTo(new DateTime(2014, 05, 27, 8, 16, 29, DateTimeKind.Utc)));
            Assert.That(result.UpdatedAt.ToUniversalTime, Is.EqualTo(new DateTime(2018, 06, 21, 17, 23, 05, DateTimeKind.Utc)));
            Assert.That(result.Email, Is.EqualTo("dave@example.com"));
            Assert.That(result.FirstName, Is.EqualTo("Dave"));
            Assert.That(result.LastName, Is.EqualTo("Example"));
            Assert.That(result.ApiToken, Is.EqualTo("abcdefghijklmnopqrstuvwxyz1234567890"));
            Assert.That(result.LastLoginAt.ToUniversalTime, Is.EqualTo(new DateTime(2018, 06, 21, 17, 22, 40, DateTimeKind.Utc)));
            Assert.That(result.Name, Is.EqualTo("Dave Example"));
            Assert.That(result.AvatarUrl, Is.EqualTo("https://gravatar.com/my_gravatar.png"));            
            Assert.That(result.Type, Is.EqualTo("User"));
        }

        [Test]
        public async Task GetProjectUsers_ReturnsCollectionOfUser()
        {
            // Arrange
            kanbaneryApiClient.SetApiTokenHeader("token");

            // Act
            var result = await kanbaneryApiClient.GetProjectUsers(123);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetWorkspaces_ReturnsCollectionOfWorkspace()
        {
            // Arrange
            kanbaneryApiClient.SetApiTokenHeader("token");

            // Act
            var result = await kanbaneryApiClient.GetWorkspaces();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

        }
    }
}
